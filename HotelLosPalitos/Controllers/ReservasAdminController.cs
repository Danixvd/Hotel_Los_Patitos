using HotelLosPalitos.LogicaDeNegocio;
using HotelLosPalitos.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPalitos.Controllers;

public class ReservasAdminController : Controller
{
    private readonly ReservacionServicio _reservacionServicio;

    public ReservasAdminController(ReservacionServicio reservacionServicio)
    {
        _reservacionServicio = reservacionServicio;
    }

    public async Task<IActionResult> Index(int? idHabitacion)
    {
        var reservas = await _reservacionServicio.ObtenerHistoricoAsync(idHabitacion);

        var modelo = new ReservaHistoricoViewModel
        {
            IdHabitacionFiltro = idHabitacion,
            Reservas = reservas.Select(r => new ReservaHistoricoItemViewModel
            {
                Id = r.Id,
                NombreDePersona = r.NombreDeLaPersona,
                Telefono = r.Telefono,
                Correo = r.Correo,
                Identificacion = r.Identificacion,
                MontoTotal = r.MontoTotal,
                FechaNacimiento = r.FechaNacimiento,
                FechaInicioReservacion = r.FechaInicioReserva,
                FechaFinReservacion = r.FechaFinReserva,
                FechaDeRegistro = r.FechaDeRegistro
            }).ToList()
        };

        return View(modelo);
    }
}