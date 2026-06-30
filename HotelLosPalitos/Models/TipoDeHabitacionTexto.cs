namespace HotelLosPalitos.Models;

public static class TipoDeHabitacionTexto
{
    public static string ObtenerTexto(int tipoDeHabitacion)
    {
        return ((TipoDeHabitacion)tipoDeHabitacion) switch
        {
            TipoDeHabitacion.Junior => "Junior",
            TipoDeHabitacion.Superior => "Superior",
            TipoDeHabitacion.Suite => "Suite",
            _ => "Desconocido"
        };
    }
}