using Microsoft.EntityFrameworkCore;
using MinamalApi.Infraestrutura.DB;
using MinimalApi.DTOs;

var builder = WebApplication.CreateBuilder(args);

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



app.MapGet("/", () => "Eai pessoal!");

app.MapPost("/login", (LoginDTO loginDTO) =>
{
  if (loginDTO.Email == "adm@teste.com" && loginDTO.Senha == "123456")
  {
    return Results.Ok("Login com sucesso");
  }
  else
  {
    return Results.Unauthorized();
  }
});



Console.WriteLine("http://localhost:5043");
app.Run();
