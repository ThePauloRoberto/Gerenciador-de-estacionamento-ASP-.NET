using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MinamalApi.Dominio.Entidades;
using MinamalApi.Infraestrutura.DB;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

namespace MinamalApi.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
  public readonly DbContexto _contexto;
  public VeiculoServico(DbContexto contexto)
  {
    _contexto = contexto;
  }

  public void ApagarPorId(Veiculo veiculo)
  {
    _contexto.Veiculos.Remove(veiculo);
    _contexto.SaveChanges();
  }

  public void Atualizar(Veiculo veiculo)
  {
    _contexto.Veiculos.Update(veiculo);

    _contexto.SaveChanges();
  }

  public Veiculo? BuscaPorId(string id)
  {
    return _contexto.Veiculos.Where(v => v.Id.Equals(id)).FirstOrDefault();
  }

  public void Incluir(Veiculo veiculo)
  {
    _contexto.Veiculos.Add(veiculo);
    _contexto.SaveChanges();
  }

  public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
  {
    var query = _contexto.Veiculos.AsQueryable();

    if (!string.IsNullOrEmpty(nome))
    {
      query = query.Where(v =>
      EF.Functions.Like(v.Nome.ToLower(),
       $"%{nome.ToLower()}%"));
    }

    int itensPorPagina = 10;

    if (pagina != null)
    {
      query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
    }
    return query.ToList();
  }

}