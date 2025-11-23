using System.Text.RegularExpressions;

namespace Cadastro.Serviços
{
    public static class ValidadorEmail // Verifica se um e-mail está no formato correto (ex: usuario@dominio.com)
    {
        private static readonly Regex EmailRegex = 
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static bool EmailValido(string email){
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }
    }
}