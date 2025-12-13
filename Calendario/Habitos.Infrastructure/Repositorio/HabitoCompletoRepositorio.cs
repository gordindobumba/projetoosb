using System;
using Habitos.Application.Interface;
using Habitos.Domain.Entidades;
using Habitos.Infrastructure.Dados;

namespace Habitos.Infrastructure.Repositorio
{
    public class HabitoCompletoRepositorio : IntRepositorioHabitoCompleto
    {
        public Task<List<HabitoCompleto>> GetHabitosCompletosDoMes(int ano, int mes)
        {
            var list = BDTeste.HabitosCompletos.Where(hc => hc.Data.Year == ano && hc.Data.Month == mes).ToList();
            return Task.FromResult(list);
        }
    }
}