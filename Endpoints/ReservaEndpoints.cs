using Microsoft.OpenApi.Models;
using reservasAPI.DTOs;
using reservasAPI.Models;
using reservasAPI.Services.Reserva;

namespace reservasAPI.Endpoints
{
    public static class ReservaEdpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/reservas").WithTags("Reservas");

            group.MapGet("/", async (IReservaServices reservasServices) =>
            {
                var reservas = await reservasServices.GetReservas();
                //200 Ok: La solicitud se realizó correctamente
                //y devuelve la lista de reservas
                return Results.Ok(reservas);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Reservas",
                Description = "Muestra una lista de todas las reservas."
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IReservaServices reservaServices) => {
                var reserva = await reservaServices.GetReserva(id);
                if (reserva == null)
                    return Results.NotFound(); // 404 Not Found: El recurso solicitado no existe
                else
                    return Results.Ok(reserva); //200 Ok: La solicitud se realizó correctamente y devuelve la Reserva
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Reserva",
                Description = "Busca una reserva por id."
            }).RequireAuthorization();

            group.MapPost("/", async (ReservaRequest reserva, IReservaServices reservaServices) =>
            {
                if (reserva == null)
                    return Results.BadRequest(); //400 Bad Request: La solicitud no se pudo procesar, error de formato

                var id = await reservaServices.PostReserva(reserva);
                //201 Created: El recurso de creó con éxito, se devuelve la úbicación del recurso creado
                return Results.Created($"api/reservas/{id}", reserva);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Reserva",
                Description = "Crear una nueva Reserva ."
            }).RequireAuthorization();


            group.MapPut("/{id}", async (int id, ReservaRequest reserva, IReservaServices reservaServices) => {

                var result = await reservaServices.PutReserva(id, reserva);
                if (result == -1)
                    return Results.NotFound();// 404 Not Found: se devuelve la úbicación del recurso creado
                else
                    return Results.Ok(result); //200 Ok: La solicitud se realizó correctamente y devuelve el cliente
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar Reserva",
                Description = "Actualiza una reserva existente."
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IReservaServices reservasServices) => {

                var result = await reservasServices.DeleteReserva(id);
                if (result == -1)
                    return Results.NotFound();// 404 Not Found: se devuelve la úbicación del recurso creado
                else
                    return Results.NoContent(); //204 No content: Recurso eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Reserva",
                Description = "Eliminar una reserva existente."
            }).RequireAuthorization();


        }
    }
}