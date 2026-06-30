using HotelLosPalitos.Abstracciones;
using HotelLosPalitos.Models;

namespace HotelLosPalitos.LogicaDeNegocio;
public class ReservacionServicio
{
    private readonly IReservacionRepositorio _reservacionRepositorio;
    private readonly IHabitacionRepositorio _habitacionRepositorio;

    public ReservacionServicio(
        IReservacionRepositorio reservacionRepositorio,
        IHabitacionRepositorio habitacionRepositorio)
    {
        _reservacionRepositorio = reservacionRepositorio;
        _habitacionRepositorio = habitacionRepositorio;
    }

    public async Task<IEnumerable<Reservacion>> ObtenerTodasAsync()
    {
        return await _reservacionRepositorio.ObtenerTodasAsync();
    }

    public async Task<IEnumerable<Reservacion>> ObtenerHistoricoAsync(int? idHabitacion)
    {
        if (idHabitacion.HasValue && idHabitacion.Value > 0)
        {
            return await _reservacionRepositorio.ObtenerPorHabitacionAsync(idHabitacion.Value);
        }

        return await _reservacionRepositorio.ObtenerTodasAsync();
    }

    public async Task<Reservacion?> BuscarPorIdAsync(int idReservacion)
    {
        return await _reservacionRepositorio.ObtenerPorIdAsync(idReservacion);
    }

    public async Task<(bool Exitoso, string Mensaje, Reservacion? Reservacion)> ReservarAsync(
        Reservacion reservacion)
    {
        var habitacion = await _habitacionRepositorio.ObtenerPorIdAsync(reservacion.IdHabitacion);
        if (habitacion is null)
        {
            return (false, "La habitacion seleccionada no existe.", null);
        }

        if (!habitacion.Estado)
        {
            return (false, "La habitacion seleccionada no esta activa para reservar.", null);
        }

        if (reservacion.FechaFinReserva <= reservacion.FechaInicioReserva)
        {
            return (false, "La fecha de fin de la reserva debe ser posterior a la fecha de inicio.", null);
        }

        int cantidadDiasReserva = (reservacion.FechaFinReserva.Date - reservacion.FechaInicioReserva.Date).Days;

        reservacion.MontoTotal = (cantidadDiasReserva * habitacion.CostoDeReserva) + habitacion.CostoDeLimpieza;
        reservacion.FechaDeRegistro = DateTime.Now;

        await _reservacionRepositorio.AgregarAsync(reservacion);

        return (true, "Reserva registrada correctamente.", reservacion);
    }
}