using System;
using System.Collections.Generic;

namespace reservasAPI.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMesa { get; set; }

    public int? UserId { get; set; }

    public DateTime FechaReserva { get; set; }

    public TimeSpan HoraReserva { get; set; }

    public int? NumPersonas { get; set; }

    public string? Estado { get; set; }

    public virtual User? User { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Mesa? IdMesaNavigation { get; set; }

        
}
