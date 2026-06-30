namespace HotelLosPalitos.ViewModels;

public class ReservaListItemViewModel
{
    public int IdHabitacion { get; set; }

    public string NombreDeHabitacion { get; set; } = string.Empty;

    public int CantidadDeHuespedes { get; set; }

    public int CantidadDeCamas { get; set; }

    public int CantidadDeBanos { get; set; }

    public string Ubicacion { get; set; } = string.Empty;

    public decimal CostoPorNoche { get; set; }

    public string TipoDeHabitacionTexto { get; set; } = string.Empty;
}