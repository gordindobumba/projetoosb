using System;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;

namespace Cadastro.Serviços
{
    public class HashSegurança
    {
        public static string HashSenha(string senha){
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            var config = new Argon2Config
            {
                Salt = salt, 
                Password = Encoding.UTF8.GetBytes(senha),
                
                TimeCost = 4,
                MemoryCost = 1024 * 64,
                Lanes = 4,
                Threads = Environment.ProcessorCount,
                HashLength = 32,
                Type = Argon2Type.DataDependentAddressing
            };

            var argon2 = new Argon2(config);

            using (var hash = argon2.Hash()) return config.EncodeString(hash.Buffer);
        }

        public static bool VerificarSenha(string senha, string hashArmazenado)
        {
            return Argon2.Verify(hashArmazenado, senha);
        }
    }
}
