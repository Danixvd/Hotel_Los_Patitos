using System.ComponentModel.DataAnnotations;

namespace HotelLosPalitos.ViewModels;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo invalido.")]
    [Display(Name = "Correo electronico")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contrasena es obligatoria.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "La contrasena debe tener al menos 6 caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contrasena")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debes confirmar la contrasena.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar contrasena")]
    [Compare("Password", ErrorMessage = "Las contrasenas no coinciden.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debes seleccionar un rol.")]
    [Display(Name = "Rol")]
    public string Rol { get; set; } = string.Empty;
}