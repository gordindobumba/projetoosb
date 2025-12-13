using System;

namespace Habitos.Domain.Entidades
{
    public class HabitoCompleto
    {
        public Guid IDHabito {get; set;}
        public DateTime Data {get; set;}
        public bool Completo {get; set;}
    }
}