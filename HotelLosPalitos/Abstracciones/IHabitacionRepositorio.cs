using HotelLosPalitos.Models;

namespace HotelLosPalitos.Abstracciones;

public interface IHabitacionRepositorio
{
    Task<IEnumerable<Habitacion>> ObtenerTodasAsync();

    Task<IEnumerable<Habitacion>> ObtenerDisponiblesAsync();

    Task<Habitacion?> ObtenerPorIdAsync(int id);

    Task<bool> ExisteCodigoAsync(string codigoDeHabitacion);

    Task AgregarAsync(Habitacion habitacion);

    Task ActualizarAsync(Habitacion habitacion);
}