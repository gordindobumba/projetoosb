using System;
using Habitos.Domain.Entidades;

namespace Habitos.Infrastructure.Dados;
public static class BDTeste
{
    public static List<Habito> Habitos = new()
    {
        new Habito
        {
            ID = Guid.NewGuid(),
            Nome = "Caminhar 5km",
            DataInicial = DateTime.Today.AddDays(-10),
            FrequenciaHabito = Frequencia.Semanal,
            DiasDaSemana = new(){DayOfWeek.Monday, DayOfWeek.Wednesday}
        },

        new Habito
        {
            ID = Guid.NewGuid(),
            Nome = "Beber 5 litros de água",
            DataInicial = DateTime.Today.AddDays(-2),
            FrequenciaHabito = Frequencia.Diario
        },

        new Habito
        {
            ID = Guid.NewGuid(),
            Nome = "Realizar relatório",
            DataInicial = DateTime.Today.AddDays(3),
            FrequenciaHabito = Frequencia.Mensal,
            DiasDoMes = new(){30}
        },
    };
}