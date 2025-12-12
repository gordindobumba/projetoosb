using System.ComponentModel.DataAnnotations;

namespace Cadastro.Models
{
    public class CódigoSenha
    {
        [Key]
        public int Id {get; set;}
        
        public string Email {get; set;} = string.Empty;
        public string Codigo {get; set;} = string.Empty;

        public DateTime TempoExpirar {get; set;}
        public bool Usado {get; set;}
    }
}