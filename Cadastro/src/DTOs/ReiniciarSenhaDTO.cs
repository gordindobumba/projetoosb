using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.DTOs
{
    public class ReiniciarSenhaDTO
    {
        public string Codigo {get; set;} = string.Empty;
        public string Senha {get; set;} = string.Empty;
    }
}