using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitos.Domain.Entidades
{
    public class Habito
    {
        public Guid ID {get; set;} // GUID pra ser um inteiro aleatório
        public string Nome {get; set;} = string.Empty;
        public DateTime DataInicial {get; set;}
        public Frequencia FrequenciaHabito {get; set;}

        public List<DayOfWeek>? DiasDaSemana {get; set;}
        public List<int>? DiasDoMes {get; set;}
    }
}