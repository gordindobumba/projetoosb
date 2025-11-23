using Cadastro.Data;
using Cadastro.DTOs;
using Cadastro.Models;
using Cadastro.Serviços;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Servicos
{
    public class Autenticador
    {
        private readonly BancoDados banco;

        public Autenticador(BancoDados db)
        {
            banco = db;
        }

        // ------------------- CADASTRAR -------------------
        public async Task<string> Cadastrar(CadastrarDTO dto)
        {
            if (!ValidadorEmail.EmailValido(dto.Email))
                return "E-mail inválido.";

            if (!ValidadorSenha.SenhaValida(dto.Senha))
                return "Senha fraca. Use 8+ caracteres, número, letra maiúscula e símbolo.";

            if (await banco.Users.AnyAsync(u => u.Email == dto.Email))
                return "E-mail já cadastrado.";

            var novo = new User
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            banco.Users.Add(novo);
            await banco.SaveChangesAsync();

            return "Usuário cadastrado com sucesso.";
        }

        // ------------------- LOGIN -------------------
        public async Task<string?> Login(LoginDTO dto)
        {
            var user = await banco.Users.FirstOrDefaultAsync(u =>
                (u.Email == dto.Login || u.Nome == dto.Login) &&
                u.Senha == dto.Senha
            );

            if (user == null)
                return null;

            var token = Guid.NewGuid().ToString();

            banco.Sessoes.Add(new Sessao
            {
                NomeUser = user.Nome,
                Token = token
            });

            await banco.SaveChangesAsync();
            return token;
        }

        // ------------------- LOGOUT -------------------
        public async Task<bool> Logout(string token)
        {
            var sessao = await banco.Sessoes.FirstOrDefaultAsync(s => s.Token == token);

            if (sessao == null)
                return false;

            banco.Sessoes.Remove(sessao);
            await banco.SaveChangesAsync();
            return true;
        }
    }
}