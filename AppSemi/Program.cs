using AppSemi.Application.Interfaces;
using AppSemi.Application.Services;
using AppSemi.Domain.Interfaces;
using AppSemi.Infrastructure.Data;
using AppSemi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Connections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DbConnectionFactory>();
builder.Services.AddScoped<ISemiService,SemiService>();
builder.Services.AddScoped<ISemiRepository, SemiRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(',');
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        politica.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
