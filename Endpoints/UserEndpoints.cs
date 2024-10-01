using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using reservasAPI.DTOs;
using reservasAPI.Services.Reserva;
using reservasAPI.Services.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace reservasAPI.Endpoints
{
    public static  class UserEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes) {
            var group = routes.MapGroup("/api/users").WithTags("Users");

            group.MapGet("/", async (IUserServices userServices) =>
            {
                var users = await userServices.GetUsers();
                //200 OK: la solicitud se realizo correctamente
                //y devuelve la lista de reservas
                return Results.Ok(users);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Usuarios",
                Description = "Muestra una lista de todos los Usuarioss."

            });

            group.MapGet("/{id}", async (int id, IUserServices userServices) => {
                var user = await userServices.GetUser(id);
                if (user == null)
                    return Results.NotFound(); //404 Not found: El recurso solicitado no existe
                else
                    return Results.Ok(user); //200 OK: la solicitud se realizo correctamente y devuelve la reserva
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Usuario",
                Description = "Buscar un Usuario por id. "

            });

            group.MapPost("/", async (UserRequest user, IUserServices userServices) => {
                if (user == null)
                    return Results.BadRequest(); //400 BadRequest: La solicitud no se pudo procesar, error de formato

                var id = await userServices.PostUser(user);
                //201 Created: El recurso se creo con exito, se devuelve la ubicacíon del recurso creado
                return Results.Created($"api/users/{id}", user);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Usuario",
                Description = "Crear un nuevo usuario."

            });

            group.MapPut("/{id}", async (int id, UserRequest user, IUserServices userServices) => {


                var result = await userServices.PutUser(id, user);
                if (result == -1)
                    return Results.NotFound(); //404 Not found: El recurso solicitado no existe
                else
                    return Results.Ok(result); //200 OK: la solicitud se realizo correctamente y devuelve la reserva
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar usuario",
                Description = "Actualizar un usuario  existente."

            });

            group.MapDelete("/{id}", async (int id, IUserServices userServices) => {


                var result = await userServices.DeleteUser(id);
                if (result == -1)
                    return Results.NotFound(); //404 Not found: El recurso solicitado no existe
                else
                    return Results.NoContent(); //204 NoContent: Recurso eliminado.
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar usuario",
                Description = "Eliminar un usuario existente."

            });

            group.MapPost("/login", async (UserRequest user, IUserServices userServices, IConfiguration config) => {

            var login = await userServices.Login(user);

            if (login is null)
                return Results.Unauthorized(); // Retorna el estado 401: Unautorized
            else
            {
                var jwtSettings = config.GetSection("JwtSetting");
                var secretkey = jwtSettings.GetValue<string>("SecretKey");
                var issuer = jwtSettings.GetValue<string>("Issuer");
                var audience = jwtSettings.GetValue<string>("Audience");


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secretkey);

                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new[] { 
                      new Claim(ClaimTypes.Name, login.Username),
                       new Claim(ClaimTypes.Role,login.UserRole)

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                   SecurityAlgorithms.HmacSha256Signature )
                  };
                    //Crear token, usando parametros definidos
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    //Convertir el token a una cadena
                    var jwt = tokenHandler.WriteToken(token);

                    return Results.Ok(jwt);
                }

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Login usuario",
                Description = "Generar token para inicio de sesión."

            });
        
        }
    }
}
