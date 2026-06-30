using HotelLosPalitos.LogicaDeNegocio;
using HotelLosPalitos.Models;
using HotelLosPalitos.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPalitos.Controllers;

public class HabitacionesController : Controller
{
    private readonly HabitacionServicio _habitacionServicio;

    public HabitacionesController(HabitacionServicio habitacionServicio)
    {
        _habitacionServicio = habitacionServicio;
    }

    public async Task<IActionResult> Index()
    {
        var habitaciones = await _habitacionServicio.ObtenerTodasAsync();

        var listado = habitaciones.Select(h => new HabitacionListItemViewModel
        {
            Id = h.Id,
            CodigoDeHabitacion = h.CodigoDeHabitacion,
            NombreDeHabitacion = h.NombreDeHabitacion,
            Ubicacion = h.Ubicacion,
            CantidadDeHuespedesPermitidos = h.CantidadDeHuespedesPermitidos,
            CantidadDeCamas = h.CantidadDeCamas,
            CantidadDeBanos = h.CantidadDeBanos,
            EncargadoDeLimpieza = h.EncargadoDeLimpieza,
            CostoDeLimpieza = h.CostoDeLimpieza,
            CostoDeReserva = h.CostoDeReserva,
            TipoDeHabitacionTexto = TipoDeHabitacionTexto.ObtenerTexto(h.TipoDeHabitacion),
            Estado = h.Estado
        }).ToList();

        return View(listado);
    }

    public IActionResult Registrar()
    {
        var modelo = new HabitacionFormViewModel { EsEdicion = false, Estado = true };
        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrar(HabitacionFormViewModel modelo)
    {
        modelo.EsEdicion = false;

        if (!ModelState.IsValid)
        {
            return View(modelo);
        }

        var habitacion = new Habitacion
        {
            CodigoDeHabitacion = modelo.CodigoDeHabitacion,
            NombreDeHabitacion = modelo.NombreDeHabitacion,
            CantidadDeHuespedesPermitidos = modelo.CantidadDeHuespedesPermitidos,
            CantidadDeCamas = modelo.CantidadDeCamas,
            CantidadDeBanos = modelo.CantidadDeBanos,
            Ubicacion = modelo.Ubicacion,
            EncargadoDeLimpieza = modelo.EncargadoDeLimpieza,
            TipoDeHabitacion = modelo.TipoDeHabitacion,
            CostoDeLimpieza = modelo.CostoDeLimpieza,
            CostoDeReserva = modelo.CostoDeReserva,
            Estado = modelo.Estado
        };

        var (exitoso, mensaje) = await _habitacionServicio.RegistrarAsync(habitacion);

        if (!exitoso)
        {
            ModelState.AddModelError(string.Empty, mensaje);
            return View(modelo);
        }

        TempData["MensajeExito"] = mensaje;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var habitacion = await _habitacionServicio.ObtenerPorIdAsync(id);
        if (habitacion is null)
        {
            return NotFound();
        }

        var modelo = new HabitacionFormViewModel
        {
            Id = habitacion.Id,
            CodigoDeHabitacion = habitacion.CodigoDeHabitacion,
            NombreDeHabitacion = habitacion.NombreDeHabitacion,
            CantidadDeHuespedesPermitidos = habitacion.CantidadDeHuespedesPermitidos,
            CantidadDeCamas = habitacion.CantidadDeCamas,
            CantidadDeBanos = habitacion.CantidadDeBanos,
            Ubicacion = habitacion.Ubicacion,
            EncargadoDeLimpieza = habitacion.EncargadoDeLimpieza,
            TipoDeHabitacion = habitacion.TipoDeHabitacion,
            CostoDeLimpieza = habitacion.CostoDeLimpieza,
            CostoDeReserva = habitacion.CostoDeReserva,
            Estado = habitacion.Estado,
            EsEdicion = true
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, HabitacionFormViewModel modelo)
    {
        modelo.EsEdicion = true;
        modelo.Id = id;

        if (!ModelState.IsValid)
        {
            return View(modelo);
        }

        var habitacion = new Habitacion
        {   
            Id = id,
            CantidadDeHuespedesPermitidos = modelo.CantidadDeHuespedesPermitidos,
            CantidadDeCamas = modelo.CantidadDeCamas,
            EncargadoDeLimpieza = modelo.EncargadoDeLimpieza,
            TipoDeHabitacion = modelo.TipoDeHabitacion,
            CostoDeLimpieza = modelo.CostoDeLimpieza,
            CostoDeReserva = modelo.CostoDeReserva,
            Estado = modelo.Estado
        };

        var (exitoso, mensaje) = await _habitacionServicio.EditarAsync(habitacion);

        if (!exitoso)
        {
            ModelState.AddModelError(string.Empty, mensaje);
            return View(modelo);
        }

        TempData["MensajeExito"] = mensaje;
        return RedirectToAction(nameof(Index));
    }
}