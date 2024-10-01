using reservasAPI.Models;

namespace reservasAPI.DTOs
{
    public class ReservaResponse
    {
        public int IdReserva { get; set; }

        public int? IdCliente { get; set; }

        public int? IdMesa { get; set; }

        public int? UserId { get; set; }

        public DateTime FechaReserva { get; set; }

        public TimeSpan HoraReserva { get; set; }

        public int? NumPersonas { get; set; }

        public string? Estado { get; set; }

        public ClienteResponse IdClienteNavigation { get; set; }

        public virtual MesaResponse IdMesaNavigation { get; set; }

        public virtual UserResponse UserIdNavigation { get; set; }
    }
    public class ReservaRequest
    {
        //public int IdReserva { get; set; }

        public int? IdCliente { get; set; }

        public int? IdMesa { get; set; }

        public int? UserId { get; set; }

        public DateTime FechaReserva { get; set; }

        public TimeSpan HoraReserva { get; set; }

        public int? NumPersonas { get; set; }

        public string? Estado { get; set; }

        
    }
}
