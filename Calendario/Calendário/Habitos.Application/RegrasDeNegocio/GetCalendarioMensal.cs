using System;
using Habitos.Application.DTOs;
using Habitos.Application.Interface;

namespace Habitos.Application.RegrasDeNegocio;

public class GetCalendarioMensal
{
    private readonly IntRepositorioHabito re;
    private readonly CalendarioServico ca;

    public GetCalendarioMensal(IntRepositorioHabito repositorio, CalendarioServico calendario)
    {
        re = repositorio;
        ca = calendario;
    }

    public async Task<CalendarioMensalDTO> Sincronizar(int ano, int mes)
    {
        var habitos = await re.GetAllAsync();
        var dias = new List<DiaCalendarioDTO>();

        int totalDias = DateTime.DaysInMonth(ano, mes);

        for(int d = 1; d <= totalDias; d++)
        {
            DateTime data = new DateTime(ano, mes, d);

            var ocorrendo = habitos.Where(h => ca.Ocorrencia(h, data)).Select(h => h.Nome).ToList();
            dias.Add(new DiaCalendarioDTO(d, ocorrendo));
        }

        return new CalendarioMensalDTO(ano, mes, dias);
    }
}
