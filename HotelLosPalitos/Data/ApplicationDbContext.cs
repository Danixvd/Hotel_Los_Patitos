using HotelLosPalitos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPalitos.Data;

/// <summary>
/// Contexto de Identity. Separado del HotelContext para no mezclar
/// las tablas del negocio con las tablas de Identity.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}