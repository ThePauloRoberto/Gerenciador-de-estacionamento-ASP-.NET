using MinimalApi.Dominio.Enums;

namespace MinimalApi.Dominio.ModelViews;

public record AdministradorModelView
{
  
  public string Email { get; set; } = default!;
  public string Id { get; set; } = default!;
  public string Perfil { get; set; } = default!;


}
