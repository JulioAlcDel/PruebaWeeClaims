using Core.Application.UseCases.CreateRegister;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRegisterRepository,RegisterRepository>();
builder.Services.AddScoped<ApplicationDBContext>();
builder.Services.AddScoped<CreateRegisterHandler>();
// Configurar Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console()
           .WriteTo.File($"Logs/logs_{DateTime.Now:yyyy-MM-dd}.txt", rollingInterval: RollingInterval.Day)
           .Enrich.FromLogContext();
});
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
var key = Encoding.ASCII.GetBytes(jwtSecretKey);
// Configuración de autenticación con JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingrese 'Bearer' [espacio] y luego su token JWT en este campo",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7257")
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Registro");
        c.RoutePrefix = string.Empty;

    });

}
app.UseCors("AllowSpecificOrigin");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
