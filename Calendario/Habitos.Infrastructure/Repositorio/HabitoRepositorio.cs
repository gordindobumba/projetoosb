using System;
using Habitos.Application.Interface;
using Habitos.Domain.Entidades;
using Habitos.Infrastructure.Dados;

namespace Habitos.Infrastructure.Repositorio
{
    public class HabitoRepositorio : IntRepositorioHabito
    {
        public Task<List<Habito>> GetAllAsync() => Task.FromResult(BDTeste.Habitos);
    }
}