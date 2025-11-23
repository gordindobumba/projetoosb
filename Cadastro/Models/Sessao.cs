using System.ComponentModel.DataAnnotations;

namespace Cadastro.Models
{
    public class Sessao
    {
        [Key]
        public int Id {get; set;}
        public string Token {get; set;} = String.Empty;
        public string NomeUser {get; set;} = String.Empty;
    }
}