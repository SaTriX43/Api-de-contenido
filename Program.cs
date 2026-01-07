using API.Middleware;
using API_de_Contenido.DALs;
using API_de_Contenido.DALs.ComentarioRepositoryCarpeta;
using API_de_Contenido.DALs.LikeRepositoryCarpeta;
using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.Models;
using API_de_Contenido.Repositories;
using API_de_Contenido.Services;
using API_de_Contenido.Services.ComentarioServiceCarpeta;
using API_de_Contenido.Services.LikeServiceCarpeta;
using API_de_Contenido.Services.PublicacionServiceCarpeta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =======================
// SERILOG
// =======================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// =======================
// DATABASE (EF CORE)
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// =======================
// JWT AUTHENTICATION
// =======================
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

// =======================
// AUTHORIZATION
// =======================
builder.Services.AddAuthorization();

// =======================
// CONTROLLERS + SWAGGER
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();

builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();
builder.Services.AddScoped<IPublicacionService, PublicacionService>();

builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();

builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

var app = builder.Build();

// =======================
// MIDDLEWARE PIPELINE
// =======================

app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
