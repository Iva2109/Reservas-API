using System;
using System.Collections.Generic;

namespace reservasAPI.Models;

public partial class Mesa
{
    public int IdMesa { get; set; }

    public int Capacidad { get; set; }

    public string? Ubicacion { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
