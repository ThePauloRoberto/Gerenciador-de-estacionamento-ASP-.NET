var builder = WebApplication.CreateBuilder(args);
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


public class LoginDTO {
  public string Email { get; set; } = default!;
  public string Senha { get; set; } = default!;

} 