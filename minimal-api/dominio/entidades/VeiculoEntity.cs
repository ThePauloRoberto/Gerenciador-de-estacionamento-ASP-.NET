using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinamalApi.Dominio.Entidades;

public class Veiculo
{

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Decorator de chave primária na entidade 
  public string Id { get; set; } = default!;
  [Required]
  [StringLength(150)]
  public string Nome { get; set; } = default!;

  [Required]
  [StringLength(100)]
  public string Marca { get; set; } = default!;

  [Required]
  public int Ano { get; set; } = default!;
}