using MinamalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces;

public interface IVeiculoServico
{
  List<Veiculo> Todos(int? paginas = 1, string? nome = null, string? marca = null);

  Veiculo? BuscaPorId(string id);

  void Incluir(Veiculo veiculo);

  void Atualizar(Veiculo veiculo);

  void ApagarPorId(Veiculo veiculo);


}