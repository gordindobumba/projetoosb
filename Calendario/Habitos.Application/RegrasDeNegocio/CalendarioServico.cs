using System;
using Habitos.Domain.Entidades;


namespace Habitos.Application.RegrasDeNegocio;
public class CalendarioServico
{
    public bool Ocorrencia(Habito habito, DateTime data)
    {
        if (data.Date < habito.DataInicial.Date) return false;
        return habito.FrequenciaHabito switch
        {
            Frequencia.Diario => true,

            Frequencia.Semanal => habito.DiasDaSemana != null && habito.DiasDaSemana.Contains(data.DayOfWeek),

            Frequencia.Mensal => habito.DiasDoMes != null && habito.DiasDoMes.Contains(data.Day),

            _ => false,
        };
    }
}