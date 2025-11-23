using System.Text.RegularExpressions;

namespace Cadastro.Serviços
{
    public class ValidadorSenha // Verifica se uma senha tem no mínimo 8 caracteres, 1 número,
                                // 1 caractere especial e 1a letra maiúscula
    {
        private static readonly Regex SenhaRegex =
            new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", RegexOptions.Compiled);

        public static bool SenhaValida(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            return SenhaRegex.IsMatch(senha);
        }
    }
}