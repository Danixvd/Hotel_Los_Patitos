using System;

namespace HotelLosPalitos.Models;
public partial class Reservacion
{
    public int Id { get; set; }

    public string NombreDeLaPersona { get; set; } = string.Empty;

    public string Identificacion { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string Direccion { get; set; } = string.Empty;

    public decimal MontoTotal { get; set; }

    public DateTime FechaInicioReserva { get; set; }

    public DateTime FechaFinReserva { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public int IdHabitacion { get; set; }
    public virtual Habitacion? Habitacion { get; set; }
}