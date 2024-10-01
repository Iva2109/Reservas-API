using reservasAPI.Models;
using System;
using System.Collections.Generic;

namespace reservasAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
