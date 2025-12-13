using System;
using Habitos.Domain.Entidades;

namespace Habitos.Application.Interface
{
    public interface IntRepositorioHabito
    {
        Task<List<Habito>> GetHabitosDoMes();
    }
}