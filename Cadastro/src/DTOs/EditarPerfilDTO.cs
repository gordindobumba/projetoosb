namespace Cadastro.DTOs
{
    public class EditarPerfilDTO
    {
        public string Token { get; set; } = string.Empty;

        // Campos opcionais. Só altera se o usuário enviar.
        public string? NovoNome { get; set; }
        public string? SenhaAtual { get; set; }
        public string? NovaSenha { get; set; }
    }
}