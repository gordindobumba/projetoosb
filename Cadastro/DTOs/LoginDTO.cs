using System.ComponentModel.DataAnnotations;
namespace Cadastro.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O usuário ou e-mail não podem ser vazios.")]
        public string Login { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A senha não pode ser vazia.")]
        public string Senha { get; set; } = string.Empty;
    }
}