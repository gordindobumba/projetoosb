using System;
using Habitos.Application.DTOs;
using Habitos.Domain.Entidades;


namespace Habitos.Application.RegrasDeNegocio;
public class CalendarioServico
{
    // Verifica quando uma atividade ocorre
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

    // Retorna a cor baseada na quantidade de atividades completadas no dia
    public string Cor(double porcentagemCompletos, int totalHabitos, DateTime data)
    {
        if(totalHabitos == 0 || data.Date > DateTime.Today) return "#515151";

        if(porcentagemCompletos == 100.0) return "#008000";
        else if(porcentagemCompletos > 0.0 && porcentagemCompletos < 100.0) return "#FFDE21";
        return "#FF0000";
    }

    // Cria o calendário do mês, com os hábitos e suas cores equivalentes
    public CalendarioMensalDTO GerarCalendarioMensal(int ano, int mes, List<Habito> habitos, List<HabitoCompleto> habitosCompletos)
    {
        var dias = new List<DiaCalendarioDTO>();
        int totalDias = DateTime.DaysInMonth(ano, mes);

        for(int d = 1; d <= totalDias; d++)
        {
            DateTime data = new DateTime(ano, mes, d);
            var ocorrendo = habitos.Where(h => Ocorrencia(h, data)).ToList();
            int total = ocorrendo.Count;

            var completosDoDia = habitosCompletos.Where(hc => hc.Data.Date == data.Date && ocorrendo.Any(h => h.ID == hc.IDHabito)).ToList();
            int completos = ocorrendo.Count(h => completosDoDia.Any(c => c.IDHabito == h.ID && c.Completo));

            double taxa = 0.0;
            if(total != 0) taxa = (completos * 100.0)/total;
            string cor = Cor(taxa, total, data);

            dias.Add(new DiaCalendarioDTO(d, ocorrendo.Select(h => h.Nome).ToList(), total, completos, Math.Round(taxa, 2), cor));
        }
        return new CalendarioMensalDTO(ano, mes, dias);
    }
}

