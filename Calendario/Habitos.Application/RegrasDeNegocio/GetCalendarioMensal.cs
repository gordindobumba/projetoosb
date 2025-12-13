using System;
using Habitos.Application.DTOs;
using Habitos.Application.Interface;

namespace Habitos.Application.RegrasDeNegocio;

public class GetCalendarioMensal
{
    private readonly IntRepositorioHabito re;
    private readonly IntRepositorioHabitoCompleto rec;
    private readonly CalendarioServico ca;

    public GetCalendarioMensal(IntRepositorioHabito repositorio, CalendarioServico calendario, IntRepositorioHabitoCompleto repositorioCompleto)
    {
        re = repositorio;
        ca = calendario;
        rec = repositorioCompleto;
    }

    // Retorna o calendário feito em CalendarioServico
    public async Task<CalendarioMensalDTO> Assincronizar(int ano, int mes)
    {
        var habitos = await re.GetHabitosDoMes();
        var completos = await rec.GetHabitosCompletosDoMes(ano, mes);

        return ca.GerarCalendarioMensal(ano, mes, habitos, completos);
    }
}
