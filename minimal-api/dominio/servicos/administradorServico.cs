using MinamalApi.Dominio.Entidades;
using MinamalApi.Infraestrutura.DB;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

namespace MinamalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
  public readonly DbContexto _contexto;
  public AdministradorServico(DbContexto contexto)
  {
    _contexto = contexto;
  }
  public Administrador? Login(LoginDTO loginDTO)
  {
    var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();

    return adm;
  }
}