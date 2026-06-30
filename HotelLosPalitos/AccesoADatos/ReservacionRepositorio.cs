using HotelLosPalitos.Abstracciones;
using HotelLosPalitos.Data;
using HotelLosPalitos.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPalitos.AccesoADatos;

public class ReservacionRepositorio : IReservacionRepositorio
{
    private readonly HotelContext _context;

    public ReservacionRepositorio(HotelContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservacion>> ObtenerTodasAsync()
    {
        return await _context.Reservaciones
            .Include(r => r.Habitacion)
            .OrderByDescending(r => r.FechaDeRegistro)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservacion>> ObtenerPorHabitacionAsync(int idHabitacion)
    {
        return await _context.Reservaciones
            .Include(r => r.Habitacion)
            .Where(r => r.IdHabitacion == idHabitacion)
            .OrderByDescending(r => r.FechaDeRegistro)
            .ToListAsync();
    }

    public async Task<Reservacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.Reservaciones
            .Include(r => r.Habitacion)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AgregarAsync(Reservacion reservacion)
    {
        _context.Reservaciones.Add(reservacion);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> TieneReservasActivasAsync(int idHabitacion)
    {
        return await _context.Reservaciones
            .AnyAsync(r => r.IdHabitacion == idHabitacion);
    }
}