using HotelLosPalitos.LogicaDeNegocio;
using HotelLosPalitos.Models;
using HotelLosPalitos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelLosPalitos.Controllers;

[Authorize(Roles = "Cliente")]
public class ReservasController : Controller
{
    private readonly HabitacionServicio _habitacionServicio;
    private readonly ReservacionServicio _reservacionServicio;

    public ReservasController(
        HabitacionServicio habitacionServicio,
        ReservacionServicio reservacionServicio)
    {
        _habitacionServicio = habitacionServicio;
        _reservacionServicio = reservacionServicio;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var habitaciones = await _habitacionServicio.ObtenerDisponiblesParaReservarAsync();

            var listado = habitaciones.Select(h => new ReservaListItemViewModel
            {
                IdHabitacion = h.Id,
                NombreDeHabitacion = h.NombreDeHabitacion,
                CantidadDeHuespedes = h.CantidadDeHuespedesPermitidos,
                CantidadDeCamas = h.CantidadDeCamas,
                CantidadDeBanos = h.CantidadDeBanos,
                Ubicacion = h.Ubicacion,
                CostoPorNoche = h.CostoDeReserva,
                TipoDeHabitacionTexto = TipoDeHabitacionTexto.ObtenerTexto(h.TipoDeHabitacion)
            }).ToList();

            return View(listado);
        }
        catch (Exception)
        {
            TempData["MensajeError"] = "Ocurrio un error al cargar las habitaciones. Intente de nuevo.";
            return View(new List<ReservaListItemViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarParcial(int idReservacion)
    {
        try
        {
            var reservacion = await _reservacionServicio.BuscarPorIdAsync(idReservacion);

            if (reservacion is null)
            {
                return Json(new { encontrado = false });
            }

            var modelo = new ReservaDetailsViewModel
            {
                Id = reservacion.Id,
                NombreDeLaPersona = reservacion.NombreDeLaPersona,
                Telefono = reservacion.Telefono,
                Correo = reservacion.Correo,
                Identificacion = reservacion.Identificacion,
                FechaNacimiento = reservacion.FechaNacimiento,
                Direccion = reservacion.Direccion,
                CodigoDeHabitacion = reservacion.Habitacion?.CodigoDeHabitacion ?? string.Empty,
                TipoDeHabitacionTexto = reservacion.Habitacion is not null
                    ? TipoDeHabitacionTexto.ObtenerTexto(reservacion.Habitacion.TipoDeHabitacion)
                    : string.Empty,
                MontoTotal = reservacion.MontoTotal,
                FechaInicioReserva = reservacion.FechaInicioReserva,
                FechaFinReserva = reservacion.FechaFinReserva,
                FechaDeRegistro = reservacion.FechaDeRegistro
            };

            return PartialView("DetallesReserva", modelo);
        }
        catch (Exception)
        {
            return Json(new { encontrado = false, error = true });
        }
    }

    public async Task<IActionResult> Reservar(int id)
    {
        try
        {
            var habitacion = await _habitacionServicio.ObtenerPorIdAsync(id);
            if (habitacion is null || !habitacion.Estado)
            {
                return NotFound();
            }

            var modelo = new ReservaFormViewModel
            {
                IdHabitacion = habitacion.Id,
                CodigoDeHabitacion = habitacion.CodigoDeHabitacion,
                NombreDeHabitacion = habitacion.NombreDeHabitacion,
                TipoDeHabitacionTexto = TipoDeHabitacionTexto.ObtenerTexto(habitacion.TipoDeHabitacion),
                CostoDeReserva = habitacion.CostoDeReserva,
                CostoDeLimpieza = habitacion.CostoDeLimpieza
            };

            return View(modelo);
        }
        catch (Exception)
        {
            TempData["MensajeError"] = "Ocurrio un error al cargar la habitacion. Intente de nuevo.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reservar(ReservaFormViewModel modelo)
    {
        try
        {
            var habitacion = await _habitacionServicio.ObtenerPorIdAsync(modelo.IdHabitacion);
            if (habitacion is null)
            {
                return NotFound();
            }

            if (modelo.CantidadDePersonas > habitacion.CantidadDeHuespedesPermitidos)
            {
                ModelState.AddModelError(
                    nameof(modelo.CantidadDePersonas),
                    $"La habitacion permite maximo {habitacion.CantidadDeHuespedesPermitidos} huespedes.");
            }

            if (!ModelState.IsValid)
            {
                modelo.CodigoDeHabitacion = habitacion.CodigoDeHabitacion;
                modelo.NombreDeHabitacion = habitacion.NombreDeHabitacion;
                modelo.TipoDeHabitacionTexto = TipoDeHabitacionTexto.ObtenerTexto(habitacion.TipoDeHabitacion);
                modelo.CostoDeReserva = habitacion.CostoDeReserva;
                modelo.CostoDeLimpieza = habitacion.CostoDeLimpieza;
                return View(modelo);
            }

            var reservacion = new Reservacion
            {
                NombreDeLaPersona = modelo.NombreDeLaPersona,
                Identificacion = modelo.Identificacion,
                Telefono = modelo.Telefono,
                Correo = modelo.Correo,
                FechaNacimiento = modelo.FechaNacimiento,
                Direccion = modelo.Direccion,
                FechaInicioReserva = modelo.FechaInicioReserva,
                FechaFinReserva = modelo.FechaFinReserva,
                IdHabitacion = modelo.IdHabitacion
            };

            var (exitoso, mensaje, reservacionCreada) = await _reservacionServicio.ReservarAsync(reservacion);

            if (!exitoso)
            {
                ModelState.AddModelError(string.Empty, mensaje);
                modelo.CodigoDeHabitacion = habitacion.CodigoDeHabitacion;
                modelo.NombreDeHabitacion = habitacion.NombreDeHabitacion;
                modelo.TipoDeHabitacionTexto = TipoDeHabitacionTexto.ObtenerTexto(habitacion.TipoDeHabitacion);
                modelo.CostoDeReserva = habitacion.CostoDeReserva;
                modelo.CostoDeLimpieza = habitacion.CostoDeLimpieza;
                return View(modelo);
            }

            return RedirectToAction(nameof(Detalles), new { idReservacion = reservacionCreada!.Id });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Ocurrio un error al procesar la reserva. Intente de nuevo.");
            return View(modelo);
        }
    }

    public async Task<IActionResult> Detalles(int idReservacion)
    {
        try
        {
            var reservacion = await _reservacionServicio.BuscarPorIdAsync(idReservacion);

            if (reservacion is null)
            {
                TempData["MensajeNoEncontrada"] =
                    "Estimado usuario, no se ha encontrado la reserva, favor realice una";
                return RedirectToAction(nameof(Index));
            }

            var modelo = new ReservaDetailsViewModel
            {
                Id = reservacion.Id,
                NombreDeLaPersona = reservacion.NombreDeLaPersona,
                Telefono = reservacion.Telefono,
                Correo = reservacion.Correo,
                Identificacion = reservacion.Identificacion,
                FechaNacimiento = reservacion.FechaNacimiento,
                Direccion = reservacion.Direccion,
                CodigoDeHabitacion = reservacion.Habitacion?.CodigoDeHabitacion ?? string.Empty,
                TipoDeHabitacionTexto = reservacion.Habitacion is not null
                    ? TipoDeHabitacionTexto.ObtenerTexto(reservacion.Habitacion.TipoDeHabitacion)
                    : string.Empty,
                MontoTotal = reservacion.MontoTotal,
                FechaInicioReserva = reservacion.FechaInicioReserva,
                FechaFinReserva = reservacion.FechaFinReserva,
                FechaDeRegistro = reservacion.FechaDeRegistro
            };

            return View("Detalles", modelo);
        }
        catch (Exception)
        {
            TempData["MensajeNoEncontrada"] =
                "Ocurrio un error al buscar la reservacion. Intente de nuevo.";
            return RedirectToAction(nameof(Index));
        }
    }
}