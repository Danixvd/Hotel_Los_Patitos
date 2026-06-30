namespace HotelLosPalitos.ViewModels;

public class ReservaHistoricoItemViewModel
{
    public int Id { get; set; }

    public string NombreDePersona { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Identificacion { get; set; } = string.Empty;

    public decimal MontoTotal { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaInicioReservacion { get; set; }

    public DateTime FechaFinReservacion { get; set; }

    public DateTime FechaDeRegistro { get; set; }
}

public class ReservaHistoricoViewModel
{
    public int? IdHabitacionFiltro { get; set; }

    public List<ReservaHistoricoItemViewModel> Reservas { get; set; } = new();
}