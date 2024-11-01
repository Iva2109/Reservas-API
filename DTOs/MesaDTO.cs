namespace reservasAPI.DTOs
{
    public class MesaResponse
    {
        public int IdMesa { get; set; }

        public int? UserId { get; set; }
        public int Capacidad { get; set; }

        public string? Ubicacion { get; set; }

        public string? Estado { get; set; }

        public virtual UserResponse UserIdNavigation { get; set; }
    }
    public class MesaRequest
    {
        public int IdMesa { get; set; }

        public int? UserId { get; set; }

        public int Capacidad { get; set; }

        public string? Ubicacion { get; set; }

        public string? Estado { get; set; }
    }
}
