using HotelLosPalitos.Models;
using HotelLosPalitos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPalitos.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel modelo, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(modelo);
        }

        try
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                modelo.Email,
                modelo.Password,
                modelo.RememberMe,
                lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Correo o contrasena incorrectos.");
            return View(modelo);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Ocurrio un error al iniciar sesion. Intente de nuevo.");
            return View(modelo);
        }
    }

    public IActionResult Registro()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registro(RegistroViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            return View(modelo);
        }

        if (modelo.Rol != "Administrador" && modelo.Rol != "Cliente")
        {
            ModelState.AddModelError(nameof(modelo.Rol), "Selecciona un rol valido.");
            return View(modelo);
        }

        try
        {
            var usuario = new ApplicationUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Password);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, modelo.Rol);
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                TempData["MensajeExito"] = "Registro exitoso. Bienvenido al sistema.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(modelo);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Ocurrio un error al registrar el usuario. Intente de nuevo.");
            return View(modelo);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult AccesoDenegado()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        if (User.IsInRole("Administrador"))
        {
            return RedirectToAction("Index", "Habitaciones");
        }

        return RedirectToAction("Index", "Reservas");
    }
}