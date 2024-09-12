using reservasAPI.Models;

namespace reservasAPI.DTOs
{
    public class ReservaResponse
    {
        public int IdReserva { get; set; }

        public int? IdCliente { get; set; }

        public int? IdMesa { get; set; }

        public DateOnly FechaReserva { get; set; }

        public TimeOnly HoraReserva { get; set; }

        public int? NumPersonas { get; set; }

        public string? Estado { get; set; }

        public ClienteResponse IdClienteNavigation { get; set; }

        public virtual MesaResponse IdMesaNavigation { get; set; }
    }
    public class ReservaResquet
    {
        public int IdReserva { get; set; }

        public int? IdCliente { get; set; }

        public int? IdMesa { get; set; }

        public DateOnly FechaReserva { get; set; }

        public TimeOnly HoraReserva { get; set; }

        public int? NumPersonas { get; set; }

        public string? Estado { get; set; }

        
    }
}
