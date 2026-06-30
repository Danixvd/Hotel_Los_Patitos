using HotelLosPalitos.Abstracciones;
using HotelLosPalitos.Data;
using HotelLosPalitos.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPalitos.AccesoADatos;

public class HabitacionRepositorio : IHabitacionRepositorio
{
    private readonly HotelContext _context;

    public HabitacionRepositorio(HotelContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Habitacion>> ObtenerTodasAsync()
    {
        return await _context.Habitaciones
            .OrderBy(h => h.CodigoDeHabitacion)
            .ToListAsync();
    }

    public async Task<IEnumerable<Habitacion>> ObtenerDisponiblesAsync()
    {
        return await _context.Habitaciones
            .Where(h => h.Estado)
            .OrderBy(h => h.CodigoDeHabitacion)
            .ToListAsync();
    }

    public async Task<Habitacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.Habitaciones
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<bool> ExisteCodigoAsync(string codigoDeHabitacion)
    {
        return await _context.Habitaciones
            .AnyAsync(h => h.CodigoDeHabitacion == codigoDeHabitacion);
    }

    public async Task AgregarAsync(Habitacion habitacion)
    {
        _context.Habitaciones.Add(habitacion);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Habitacion habitacion)
    {
        _context.Habitaciones.Update(habitacion);
        await _context.SaveChangesAsync();
    }
}