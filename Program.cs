using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reservasAPI.DTOs;
using reservasAPI.Endpoints;
using reservasAPI.Models;
using reservasAPI.Services.Reserva;
using reservasAPI.Services.Users;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.OpenApi.Models;
using reservasAPI.Services.Mesas.reservasAPI.Services.Mesa;
using reservasAPI.Services.Mesa;
using reservasAPI.Services.Cliente;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el siguiente formato: Bearer {token}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

builder.Services.AddDbContext<ReservasdbContext>(
    o=>o.UseSqlServer(builder.Configuration.GetConnectionString("ReservasdbConnection"))
    );

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IReservaServices, ReservaServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMesaServices, MesaServices>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var jwtSettings = builder.Configuration.GetSection("JwtSetting");
var secretkey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    // Configura el esquema de autenticación por defecto
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    //Permite usar HTTP en lugar de HTTPS
    options.RequireHttpsMetadata = false;
    //Guardar el toke en el contexto de autenticación
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
      ValidAudience = jwtSettings.GetValue<string>("Audience"),
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints();

app.Run();

public partial class Program { }