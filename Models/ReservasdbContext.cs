using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace reservasAPI.Models;

public partial class ReservasdbContext : DbContext
{
    public ReservasdbContext()
    {
    }

    public ReservasdbContext(DbContextOptions<ReservasdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F5D731876C");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_cliente_Users");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__mesas__68A1E1592FD968E9");

            entity.ToTable("mesas");

            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("disponible")
                .HasColumnName("estado");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .HasColumnName("ubicacion");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Mesas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_mesas_Users");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__reserva__423CBE5D12EDDEA5");

            entity.ToTable("reserva");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaReserva).HasColumnName("fecha_reserva");
            entity.Property(e => e.HoraReserva).HasColumnName("hora_reserva");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.NumPersonas).HasColumnName("num_personas");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__reserva__id_clie__403A8C7D");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK__reserva__id_mesa__412EB0B6");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_reserva_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users");

            entity.ToTable("Users");

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired();
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.UserRole)
                .HasMaxLength(100)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
