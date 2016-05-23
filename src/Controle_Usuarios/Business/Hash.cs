using System;
using System.Security.Cryptography;
using System.Text;

namespace Controle_Usuarios.Business
{
    public class Hash
    {
        private HashAlgorithm _algoritmo;

        public Hash(HashAlgorithm algoritmo)
        {
            _algoritmo = algoritmo;
        }

        public string Criptografar(string valor)
        {
            var encryptedPassword = _algoritmo.ComputeHash(Encoding.UTF8.GetBytes(valor));

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerificarValor(string valorDigitado, string valorCadastrado)
        {
            return Criptografar(valorDigitado) == valorCadastrado;
        }
    }
}
