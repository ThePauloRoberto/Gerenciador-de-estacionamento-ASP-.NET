using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinamalApi.Dominio.Entidades;

public class Administrador
{

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Decorator de chave primária na entidade 
  public string Id { get; set; } = default!;
  [Required]
  [StringLength(255)]
  public string Email { get; set; } = default!;

  [Required]
  [StringLength(50)]
  public string Senha { get; set; } = default!;

  [Required]
  [StringLength(10)]
  public string Perfil { get; set; } = default!;
}