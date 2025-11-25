using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Cadastro.Servicos
{
    public class ServiçoEmail
    {
        private readonly IConfiguration configurar;

        public ServiçoEmail(IConfiguration config)
        {
            configurar = config;
        }

        public async Task EnviarEmail(string para, string assunto, string conteudo)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Sistema de Cadastro", configurar["Email:Usuario"]));
            mensagem.To.Add(new MailboxAddress("", para));
            mensagem.Subject = assunto;

            mensagem.Body = new TextPart("html")
            {
                Text = conteudo
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                configurar["Email:Smtp"],
                int.Parse(configurar["Email:Porta"]),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                configurar["Email:Usuario"],
                configurar["Email:Senha"]
            );

            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }
    }
}