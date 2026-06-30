using HotelLosPalitos.Abstracciones;
using HotelLosPalitos.Models;

namespace HotelLosPalitos.LogicaDeNegocio;

public class HabitacionServicio
{
    private readonly IHabitacionRepositorio _habitacionRepositorio;

    public HabitacionServicio(IHabitacionRepositorio habitacionRepositorio)
    {
        _habitacionRepositorio = habitacionRepositorio;
    }

    public async Task<IEnumerable<Habitacion>> ObtenerTodasAsync()
    {
        return await _habitacionRepositorio.ObtenerTodasAsync();
    }

    public async Task<IEnumerable<Habitacion>> ObtenerDisponiblesParaReservarAsync()
    {
        return await _habitacionRepositorio.ObtenerDisponiblesAsync();
    }

    public async Task<Habitacion?> ObtenerPorIdAsync(int id)
    {
        return await _habitacionRepositorio.ObtenerPorIdAsync(id);
    }

    public async Task<(bool Exitoso, string Mensaje)> RegistrarAsync(Habitacion habitacion)
    {
        bool existeCodigo = await _habitacionRepositorio.ExisteCodigoAsync(habitacion.CodigoDeHabitacion);
        if (existeCodigo)
        {
            return (false, $"Ya existe una habitacion registrada con el codigo '{habitacion.CodigoDeHabitacion}'.");
        }

        habitacion.FechaDeRegistro = DateTime.Now;
        habitacion.FechaDeModificacion = null;

        await _habitacionRepositorio.AgregarAsync(habitacion);
        return (true, "Habitacion registrada correctamente.");
    }

    public async Task<(bool Exitoso, string Mensaje)> EditarAsync(Habitacion datosEditados)
    {
        var habitacionExistente = await _habitacionRepositorio.ObtenerPorIdAsync(datosEditados.Id);
        if (habitacionExistente is null)
        {
            return (false, "No se encontro la habitacion a editar.");
        } 

        habitacionExistente.CantidadDeHuespedesPermitidos = datosEditados.CantidadDeHuespedesPermitidos;
        habitacionExistente.CantidadDeCamas = datosEditados.CantidadDeCamas;
        habitacionExistente.EncargadoDeLimpieza = datosEditados.EncargadoDeLimpieza;
        habitacionExistente.TipoDeHabitacion = datosEditados.TipoDeHabitacion;
        habitacionExistente.CostoDeLimpieza = datosEditados.CostoDeLimpieza;
        habitacionExistente.CostoDeReserva = datosEditados.CostoDeReserva;
        habitacionExistente.Estado = datosEditados.Estado;
        habitacionExistente.FechaDeModificacion = DateTime.Now;

        await _habitacionRepositorio.ActualizarAsync(habitacionExistente);
        return (true, "Habitacion editada correctamente.");
    }
}