using System.ComponentModel.DataAnnotations;

namespace HotelLosPalitos.ViewModels;

public class HabitacionFormViewModel
{
    public int Id { get; set; }

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

    [Display(Name = "Activo")]
    public bool Estado { get; set; } = true;

    public bool EsEdicion { get; set; }
}