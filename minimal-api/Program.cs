using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinamalApi.Dominio.Entidades;
using MinamalApi.Dominio.ModelViews;
using MinamalApi.Dominio.Servicos;
using MinamalApi.Infraestrutura.DB;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbContexto>(
  options =>
  {
    options.UseMySql(
      builder.Configuration.GetConnectionString("mysql"),
      ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
  }
);

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion 

#region Administradores
app.MapPost("/Administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
  if (administradorServico.Login(loginDTO) != null)
  {
    return Results.Ok("Login com sucesso");
  }
  else
  {
    return Results.Unauthorized();
  }
}).WithTags("Administrador");
#endregion

#region Veiculos
app.MapPost("/Veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
  var veiculo = new Veiculo
  {
    Nome = veiculoDTO.Nome,
    Marca = veiculoDTO.Marca,
    Ano = veiculoDTO.Ano
  };
  veiculoServico.Incluir(veiculo);

  return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculo");


app.MapGet("/Veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{

  var veiculos = veiculoServico.Todos(pagina);

  return Results.Ok(veiculos);
}).WithTags("Veiculo");
#endregion

#region app
app.UseSwagger();
app.UseSwaggerUI();

Console.WriteLine("http://localhost:5043");
app.Run();
#endregion