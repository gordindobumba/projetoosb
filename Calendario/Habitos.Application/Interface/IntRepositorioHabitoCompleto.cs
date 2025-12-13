using System;
using Habitos.Domain.Entidades;

namespace Habitos.Application.Interface
{
    public interface IntRepositorioHabitoCompleto
    {
        Task<List<HabitoCompleto>> GetHabitosCompletosDoMes(int ano, int mes);
    }
}