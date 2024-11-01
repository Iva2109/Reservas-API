using Microsoft.OpenApi.Models;
using reservasAPI.DTOs;
using reservasAPI.Services.Mesa;
using reservasAPI.Services.Mesas.reservasAPI.Services.Mesa;

namespace reservasAPI.Endpoints
{
    public static class MesaEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/mesas").WithTags("Mesas");

            group.MapGet("/", async (IMesaServices mesaServices) =>
            {
                var mesas = await mesaServices.GetMesas();
                return Results.Ok(mesas); // 200 Ok: Devuelve la lista de mesas
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Mesas",
                Description = "Muestra una lista de todas las mesas."
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IMesaServices mesaServices) =>
            {
                var mesa = await mesaServices.GetMesa(id);
                if (mesa == null)
                    return Results.NotFound(); // 404 Not Found: Mesa no encontrada
                else
                    return Results.Ok(mesa); // 200 Ok: Devuelve la mesa encontrada
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Mesa",
                Description = "Busca una mesa por id."
            }).RequireAuthorization();

            group.MapPost("/", async (MesaRequest mesa, IMesaServices mesaServices) =>
            {
                if (mesa == null)
                    return Results.BadRequest();

                try
                {
                    var id = await mesaServices.PostMesa(mesa);
                    return Results.Created($"api/mesas/{id}", mesa);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(ex.Message); // Devuelve 409 Conflict si la mesa ya existe
                }
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Mesa",
                Description = "Crear una nueva mesa."
            }).RequireAuthorization();


            group.MapPut("/{id}", async (int id, MesaRequest mesa, IMesaServices mesaServices) =>
            {
                var result = await mesaServices.PutMesa(id, mesa);
                if (result == -1)
                    return Results.NotFound(); // 404 Not Found: Mesa no encontrada para actualizar
                else
                    return Results.Ok(result); // 200 Ok: Mesa actualizada correctamente
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar Mesa",
                Description = "Actualiza una mesa existente."
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IMesaServices mesaServices) =>
            {
                var result = await mesaServices.DeleteMesa(id);
                if (result == -1)
                    return Results.NotFound(); // 404 Not Found: Mesa no encontrada para eliminar
                else
                    return Results.NoContent(); // 204 No Content: Mesa eliminada correctamente
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Mesa",
                Description = "Eliminar una mesa existente."
            }).RequireAuthorization();
        }
    }
}
