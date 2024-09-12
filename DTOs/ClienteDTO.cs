﻿namespace reservasAPI.DTOs
{
    public class ClienteResponse
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public DateOnly? FechaRegistro { get; set; }
    }

    public class ClienteResquet
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public DateOnly? FechaRegistro { get; set; }
    }
}