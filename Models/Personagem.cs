using System.ComponentModel.DataAnnotations;

namespace crudGus.Models;

public class Personagem
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é um campo obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O tipo é um campo obrigatório")]
    [MaxLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
    public string Tipo { get; set; }
}