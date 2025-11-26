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
        private readonly ServiçoEmail servico;

        public Autenticador(BancoDados bd, ServiçoEmail servicoEmail)
        {
            banco = bd;
            servico = servicoEmail;
        }

        // ------------------- CADASTRAR -------------------
        public async Task<string> Cadastrar(CadastrarDTO dto)
        {
            if (!ValidadorEmail.EmailValido(dto.Email))
                return "E-mail inválido.";

            if (!ValidadorSenha.SenhaValida(dto.Senha))
                return "Formato de senha inválido. A senha deve ter no mínimo 8 caracteres, 1 número, 1 letra maiúscula e 1 símbolo. Tente novamente.";

            if (await banco.Users.AnyAsync(u => u.Email == dto.Email))
                return "E-mail já cadastrado.";

            var novo = new User
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = HashSegurança.HashSenha(dto.Senha)
            };

            banco.Users.Add(novo);
            await banco.SaveChangesAsync();

            return "Usuário cadastrado com sucesso.";
        }

        // ------------------- LOGIN -------------------
        public async Task<string?> Login(LoginDTO dto)
        {
            var user = await banco.Users.FirstOrDefaultAsync(u =>
                u.Email == dto.Login || u.Nome == dto.Login
            );

            if (user == null)
                return null;
            
            bool senhaExiste = HashSegurança.VerificarSenha(dto.Senha, user.Senha);

            if(senhaExiste == false) return null;

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

        // --------------- EDITAR PERFIL ----------------
        public async Task<string> EditarPerfil(string token, string? nome, string? senhaAtual, string? senhaNova)
        {
            var sessao = await banco.Sessoes.FirstOrDefaultAsync(s => s.Token == token);
            if(sessao == null) return "Não pode alterar dados sem estar no sistema. Tente novamente.";

            var usuario = await banco.Users.FirstOrDefaultAsync(u => u.Nome == sessao.NomeUser);
            if(usuario == null) return "Usuário não encontrado. Tente novamente.";

            bool alterou = false;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                usuario.Nome = nome;
                alterou = true;
            }

            if (!string.IsNullOrWhiteSpace(senhaAtual) || !string.IsNullOrWhiteSpace(senhaNova))
            {
                if(string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(senhaNova))
                return "Para alterar a senha, envie tanto a senha nova quanto a atual. Tente novamente.";

                if(!HashSegurança.VerificarSenha(senhaAtual, usuario.Senha)) return "Senha atual incorreta. Tente novamente.";

                if(!ValidadorSenha.SenhaValida(senhaNova)) 
                return "Nova senha inválida. Lembre-se que a senha deve ter no mínimo 8 caracteres, 1 número, 1 letra maiúscula e 1 símbolo. Tente novamente.";

                usuario.Senha = HashSegurança.HashSenha(senhaNova);
                alterou = true;
            }

            if(alterou == false) return "Nenhuma alteração feita.";

            await banco.SaveChangesAsync();
            return "Edição de perfil realizada com sucesso!";
        }

        // --------------- RECUPERAR SENHA ---------------
        public async Task<string> GerarCodigo(string email)
        {
            var user = await banco.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return "Se o email estiver cadastrado, um código foi enviado.";

            string codigo = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

            var codigoRecuperacao = new CódigoSenha
            {
                Email = email,
                Codigo = codigo,
                TempoExpirar = DateTime.UtcNow.AddMinutes(15),
                Usado = false
            };

            string corpo = $@"
                <h2>Recuperação de Senha</h2>
                <p>Use o código abaixo para redefinir sua senha:</p>
                <h3>{codigo}</h3>
                <p>Este código expira em 15 minutos.</p>
            ";

            await servico.EnviarEmail(email, "Recuperar Senha", corpo);
            
            banco.CodigosSenha.Add(codigoRecuperacao);
            await banco.SaveChangesAsync();
            
            return "Se o email estiver cadastrado, um código será enviado à ele.";
        }
        
        public async Task<string> ReiniciarSenha(string codigo, string NovaSenha)
        {
            var registro = await banco.CodigosSenha.FirstOrDefaultAsync(c => c.Codigo == codigo);

            if(registro == null) return "Código inválido.";

            if(registro.Usado) return "Esse código já foi usado.";

            if(registro.TempoExpirar < DateTime.UtcNow) return "Código expirado.";

            var usuario = await banco.Users.FirstOrDefaultAsync(u => u.Email == registro.Email);

            if(usuario == null) return "Usuário não encontrado.";

            if(!ValidadorSenha.SenhaValida(NovaSenha)) 
            return "Formato de senha inválido. A senha deve ter no mínimo 8 caracteres, 1 número, 1 letra maiúscula e 1 símbolo. Tente novamente.";
            
            usuario.Senha = HashSegurança.HashSenha(NovaSenha);
            registro.Usado = true;
            await banco.SaveChangesAsync();
            return "Senha redefinida com sucesso!";
        }
    }
}
