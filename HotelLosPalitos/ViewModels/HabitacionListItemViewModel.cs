namespace HotelLosPalitos.ViewModels;

public class HabitacionListItemViewModel
{
    public int Id { get; set; }

    public string CodigoDeHabitacion { get; set; } = string.Empty;

    public string NombreDeHabitacion { get; set; } = string.Empty;

    public string Ubicacion { get; set; } = string.Empty;

    public int CantidadDeHuespedesPermitidos { get; set; }

    public int CantidadDeCamas { get; set; }

    public int CantidadDeBanos { get; set; }

    public string EncargadoDeLimpieza { get; set; } = string.Empty;

    public decimal CostoDeLimpieza { get; set; }

    public decimal CostoDeReserva { get; set; }

    public string TipoDeHabitacionTexto { get; set; } = string.Empty;

    public bool Estado { get; set; }
}