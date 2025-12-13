using System;
using Habitos.Domain.Entidades;

namespace Habitos.Infrastructure.Dados;
public static class BDTeste
{
    public static List<Habito> Habitos = new()
    {
        new Habito
        {
            ID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Nome = "Caminhar 5km",
            DataInicial = DateTime.Today.AddDays(-10),
            FrequenciaHabito = Frequencia.Semanal,
            DiasDaSemana = new(){DayOfWeek.Monday, DayOfWeek.Thursday}
        },

        new Habito
        {
            ID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
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
        }
    };

    public static List<HabitoCompleto> HabitosCompletos = new()
    {
        new HabitoCompleto
        {
            IDHabito = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Data = DateTime.Today.Date.AddDays(-2),
            Completo = true
        },

        new HabitoCompleto
        {
            IDHabito = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Data = DateTime.Today.Date.AddDays(0),
            Completo = true
        }
    };
}