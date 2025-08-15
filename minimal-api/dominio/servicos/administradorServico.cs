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

  public Administrador? BuscarPorId(string id)
  {
    return _contexto.Administradores.Where(v => v.Id.Equals(id)).FirstOrDefault();

  }

  public Administrador? Incluir(Administrador administrador)
  {
    _contexto.Administradores.Add(administrador);
    _contexto.SaveChanges();

    return administrador;
  }

  public Administrador? Login(LoginDTO loginDTO)
  {
    var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();

    return adm;
  }

  public List<Administrador> Todos(int? pagina)
  {
    var query = _contexto.Administradores.AsQueryable();

    int itensPorPagina = 10;

    if (pagina != null)
    {
      query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
    }
    return query.ToList();
  }

}
