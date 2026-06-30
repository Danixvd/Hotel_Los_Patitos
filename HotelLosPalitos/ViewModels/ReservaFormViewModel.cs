using System.ComponentModel.DataAnnotations;

namespace HotelLosPalitos.ViewModels;

public class ReservaFormViewModel
{
    [Required]
    public int IdHabitacion { get; set; }

    public string CodigoDeHabitacion { get; set; } = string.Empty;

    public string NombreDeHabitacion { get; set; } = string.Empty;

    public string TipoDeHabitacionTexto { get; set; } = string.Empty;

    public decimal CostoDeReserva { get; set; }

    public decimal CostoDeLimpieza { get; set; }

    [Display(Name = "Nombre de la persona")]
    [Required(ErrorMessage = "El nombre de la persona es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre admite maximo 150 caracteres.")]
    public string NombreDeLaPersona { get; set; } = string.Empty;

    [Display(Name = "Identificacion")]
    [Required(ErrorMessage = "La identificacion es obligatoria.")]
    [StringLength(30, ErrorMessage = "La identificacion admite maximo 30 caracteres.")]
    public string Identificacion { get; set; } = string.Empty;

    [Display(Name = "Telefono")]
    [Required(ErrorMessage = "El telefono es obligatorio.")]
    [StringLength(10, ErrorMessage = "El telefono admite maximo 10 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Display(Name = "Correo")]
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato de correo no es valido.")]
    [StringLength(50, ErrorMessage = "El correo admite maximo 50 caracteres.")]
    public string Correo { get; set; } = string.Empty;

    [Display(Name = "Fecha de nacimiento")]
    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    [Display(Name = "Direccion")]
    [Required(ErrorMessage = "La direccion es obligatoria.")]
    [StringLength(200, ErrorMessage = "La direccion admite maximo 200 caracteres.")]
    public string Direccion { get; set; } = string.Empty;

    [Display(Name = "Cantidad de personas")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de personas debe ser mayor a 0.")]
    public int CantidadDePersonas { get; set; }

    [Display(Name = "Fecha de inicio de reserva")]
    [Required(ErrorMessage = "La fecha de inicio de la reserva es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaInicioReserva { get; set; } = DateTime.Today;

    [Display(Name = "Fecha de fin de reserva")]
    [Required(ErrorMessage = "La fecha de fin de la reserva es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaFinReserva { get; set; } = DateTime.Today.AddDays(1);
}