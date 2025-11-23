using Microsoft.EntityFrameworkCore;
using Cadastro.Models;

namespace Cadastro.Data
{
    public class BancoDados : DbContext
    {
        public BancoDados(DbContextOptions<BancoDados> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
    }
}