using HotelLosPalitos.Models;

namespace HotelLosPalitos.Abstracciones;

public interface IReservacionRepositorio
{
    Task<IEnumerable<Reservacion>> ObtenerTodasAsync();

    Task<IEnumerable<Reservacion>> ObtenerPorHabitacionAsync(int idHabitacion);

    Task<Reservacion?> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Reservacion reservacion);

    Task<bool> TieneReservasActivasAsync(int idHabitacion);
}