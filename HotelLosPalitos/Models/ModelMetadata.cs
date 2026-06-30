using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPalitos.Models;

[ModelMetadataType(typeof(HabitacionMetadata))]
public partial class Habitacion
{
}

[ModelMetadataType(typeof(ReservacionMetadata))]
public partial class Reservacion
{
}
public class HabitacionMetadata
{
    [Display(Name = "Codigo de habitacion")]
    [Required(ErrorMessage = "El codigo de habitacion es obligatorio.")]
    [StringLength(7, ErrorMessage = "El codigo de habitacion admite maximo 7 caracteres.")]
    public string CodigoDeHabitacion { get; set; } = string.Empty;

    [Display(Name = "Nombre de habitacion")]
    [Required(ErrorMessage = "El nombre de la habitacion es obligatorio.")]
    [StringLength(30, ErrorMessage = "El nombre admite maximo 30 caracteres.")]
    public string NombreDeHabitacion { get; set; } = string.Empty;

    [Display(Name = "Cantidad de huespedes permitidos")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de huespedes permitidos debe ser mayor a 0.")]
    public int CantidadDeHuespedesPermitidos { get; set; }

    [Display(Name = "Cantidad de camas")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de camas debe ser mayor a 0.")]
    public int CantidadDeCamas { get; set; }

    [Display(Name = "Cantidad de banos")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de banos debe ser mayor a 0.")]
    public int CantidadDeBanos { get; set; }

    [Display(Name = "Ubicacion")]
    [Required(ErrorMessage = "La ubicacion es obligatoria.")]
    [StringLength(10, ErrorMessage = "La ubicacion admite maximo 10 caracteres.")]
    public string Ubicacion { get; set; } = string.Empty;

    [Display(Name = "Encargado de limpieza")]
    [Required(ErrorMessage = "El encargado de limpieza es obligatorio.")]
    [StringLength(100, ErrorMessage = "El encargado de limpieza admite maximo 100 caracteres.")]
    public string EncargadoDeLimpieza { get; set; } = string.Empty;

    [Display(Name = "Tipo de habitacion")]
    [Range(1, 3, ErrorMessage = "Selecciona un tipo de habitacion valido.")]
    public int TipoDeHabitacion { get; set; }

    [Display(Name = "Costo de limpieza")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo de limpieza debe ser mayor a 0.")]
    public decimal CostoDeLimpieza { get; set; }

    [Display(Name = "Costo de reserva")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo de reserva debe ser mayor a 0.")]
    public decimal CostoDeReserva { get; set; }

    [Display(Name = "Fecha de registro")]
    [DataType(DataType.DateTime)]
    public DateTime FechaDeRegistro { get; set; }

    [Display(Name = "Fecha de modificacion")]
    [DataType(DataType.DateTime)]
    public DateTime? FechaDeModificacion { get; set; }

    [Display(Name = "Activo")]
    public bool Estado { get; set; }
}
public class ReservacionMetadata
{
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

    [Display(Name = "Monto total")]
    public decimal MontoTotal { get; set; }

    [Display(Name = "Fecha de inicio de reserva")]
    [Required(ErrorMessage = "La fecha de inicio de la reserva es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaInicioReserva { get; set; }

    [Display(Name = "Fecha de fin de reserva")]
    [Required(ErrorMessage = "La fecha de fin de la reserva es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaFinReserva { get; set; }

    [Display(Name = "Fecha de reservacion")]
    [DataType(DataType.DateTime)]
    public DateTime FechaDeRegistro { get; set; }

    [Display(Name = "Habitacion")]
    public int IdHabitacion { get; set; }
}