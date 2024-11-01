using Microsoft.OpenApi.Models;
using reservasAPI.DTOs;
using reservasAPI.Services.Cliente;

namespace reservasAPI.Endpoints
{
    public static class ClienteEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/clientes").WithTags("Clientes");

            // Endpoint para obtener la lista de clientes
            group.MapGet("/", async (IClienteService clienteService) =>
            {
                var clientes = await clienteService.GetClientes();
                return Results.Ok(clientes); // 200 Ok: Devuelve la lista de clientes
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Clientes",
                Description = "Muestra una lista de todos los clientes."
            }).RequireAuthorization();

            // Endpoint para obtener un cliente por su id
            group.MapGet("/{id}", async (int id, IClienteService clienteService) =>
            {
                var cliente = await clienteService.GetCliente(id);
                if (cliente == null)
                    return Results.NotFound(); // 404 Not Found: Cliente no encontrado
                else
                    return Results.Ok(cliente); // 200 Ok: Devuelve el cliente encontrado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Cliente",
                Description = "Busca un cliente por id."
            }).RequireAuthorization();

            // Endpoint para crear un nuevo cliente
            group.MapPost("/", async (ClienteRequest cliente, IClienteService clienteServices) =>
            {
                if (cliente == null)
                    return Results.BadRequest();

                try
                {
                    var id = await clienteServices.PostCliente(cliente);
                    return Results.Created($"api/clientes/{id}", cliente);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(ex.Message); // Devuelve 409 Conflict si el cliente ya existe
                }
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Cliente",
                Description = "Crear un nuevo cliente."
            }).RequireAuthorization();


            // Endpoint para actualizar un cliente existente
            group.MapPut("/{id}", async (int id, ClienteRequest cliente, IClienteService clienteService) =>
            {
                var result = await clienteService.PutCliente(id, cliente);
                if (result == -1)
                    return Results.NotFound(); // 404 Not Found: Cliente no encontrado para actualizar
                else
                    return Results.Ok(result); // 200 Ok: Cliente actualizado correctamente
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar Cliente",
                Description = "Actualiza un cliente existente."
            }).RequireAuthorization();

            // Endpoint para eliminar un cliente
            group.MapDelete("/{id}", async (int id, IClienteService clienteService) =>
            {
                var result = await clienteService.DeleteCliente(id);
                if (result == -1)
                    return Results.NotFound(); // 404 Not Found: Cliente no encontrado para eliminar
                else
                    return Results.NoContent(); // 204 No Content: Cliente eliminado correctamente
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Cliente",
                Description = "Elimina un cliente existente."
            }).RequireAuthorization();
        }
    }
}

