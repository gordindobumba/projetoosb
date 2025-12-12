using System.ComponentModel.DataAnnotations;
namespace Cadastro.DTOs
{
    public class CadastrarDTO
    {
        [Required(ErrorMessage = "O nome não pode ser vazio.")]
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
    }
}