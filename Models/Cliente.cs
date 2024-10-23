using System;
using System.Collections.Generic;

namespace reservasAPI.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    // Relación con la tabla Users
    public int? UserId { get; set; }

    // Información del cliente
    public string Nombre { get; set; } = null!;
    public string? Correo { get; set; }
    public string? Telefono { get; set; }

    // Fecha de registro con valor predeterminado si es nula
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Propiedad de navegación para la relación con User
    public virtual User? User { get; set; }

    // Propiedad de navegación para las reservas del cliente
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
