using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinamalApi.Dominio.Entidades;
using MinimalApi.Dominio.ModelViews;
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

ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{

  var validacao = new ErrosDeValidacao
  {
    Mensagens = new List<string>()
  };
  if (string.IsNullOrEmpty(veiculoDTO.Nome))
  {
    validacao.Mensagens.Add("O nome não pode ser vazio!");
  }
  if (string.IsNullOrEmpty(veiculoDTO.Marca))
  {
    validacao.Mensagens.Add("O marca não pode ficar em branco!");
  }
  if (veiculoDTO.Ano < 1950)
  {
    validacao.Mensagens.Add("Veículo muito antigo, aceito somente anos superiores a 1950!");
  }

  return validacao;
}


app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{

  var validacao = validaDTO(veiculoDTO);
  if (validacao.Mensagens.Count() > 0)
  {
    return Results.BadRequest(validacao);
  }

  var veiculo = new Veiculo
  {
    Nome = veiculoDTO.Nome,
    Marca = veiculoDTO.Marca,
    Ano = veiculoDTO.Ano
  };
  veiculoServico.Incluir(veiculo);

  return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculo");


app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{

  var veiculos = veiculoServico.Todos(pagina);

  return Results.Ok(veiculos);
}).WithTags("Veiculo");

app.MapGet("/veiculos/{id}", ([FromRoute] string id, IVeiculoServico veiculoServico) =>
{

  var veiculo = veiculoServico.BuscaPorId(id);

  if (veiculo == null) return Results.NotFound();

  return Results.Ok(veiculo);
}).WithTags("Veiculo");

app.MapPut("/veiculos/{id}", ([FromRoute] string id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{

  var veiculo = veiculoServico.BuscaPorId(id);
  if (veiculo == null) return Results.NotFound();

  var validacao = validaDTO(veiculoDTO);
  if (validacao.Mensagens.Count() > 0)
  {
    return Results.BadRequest(validacao);
  }

  veiculo.Nome = veiculoDTO.Nome;
  veiculo.Marca = veiculoDTO.Marca;
  veiculo.Ano = veiculoDTO.Ano;

  veiculoServico.Atualizar(veiculo);

  return Results.Ok(veiculo);
}).WithTags("Veiculo");

app.MapDelete("/veiculos/{id}", ([FromRoute] string id, IVeiculoServico veiculoServico) =>
{

  var veiculo = veiculoServico.BuscaPorId(id);
  if (veiculo == null) return Results.NotFound();

  veiculoServico.ApagarPorId(veiculo);

  return Results.NoContent();
}).WithTags("Veiculo");

#endregion

#region app
app.UseSwagger();
app.UseSwaggerUI();

Console.WriteLine("http://localhost:5043");
app.Run();
#endregion