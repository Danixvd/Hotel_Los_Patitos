using HotelLosPalitos.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelLosPalitos.Data;

public partial class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Habitacion> Habitaciones { get; set; }

    public virtual DbSet<Reservacion> Reservaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.ToTable("HABITACIONES");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CodigoDeHabitacion).HasMaxLength(7).IsRequired();
            entity.Property(e => e.NombreDeHabitacion).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Ubicacion).HasMaxLength(10).IsRequired();
            entity.Property(e => e.EncargadoDeLimpieza).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CostoDeLimpieza).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CostoDeReserva).HasColumnType("decimal(18,2)");
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
            entity.Property(e => e.FechaDeModificacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.ToTable("RESERVACIONES");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.NombreDeLaPersona).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Identificacion).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Telefono).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Correo).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Direccion).HasMaxLength(200).IsRequired();
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18,2)");
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.FechaInicioReserva).HasColumnType("datetime");
            entity.Property(e => e.FechaFinReserva).HasColumnType("datetime");
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");

            entity.HasOne(r => r.Habitacion)
                  .WithMany()
                  .HasForeignKey(r => r.IdHabitacion)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}