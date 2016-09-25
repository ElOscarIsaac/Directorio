using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.BusinessLogic
{
    public class Security
    {
        /// <summary>
        /// Método que permite encriptar las credenciales y obtener el token asociado al usuario
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string Encrypt(string cadena)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(ConfigurationManager.AppSettings["Clave"].ToString()));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(cadena);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            string Resultado = Convert.ToBase64String(Results);
            return ToHex(Resultado);
        }
        /// <summary>
        /// Permite desencriptar la cadena con el algoritmo de rijndael
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string Decrypt(string Token)
        {
            string CadenaEncriptada = HexToString(Token);
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(ConfigurationManager.AppSettings["Clave"].ToString()));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(CadenaEncriptada);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        /// <summary>
        /// Permite validar si el token es válido
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static bool ValidateToken(string Token)
        {
            bool Valid = false;
            string[] Elements = Decrypt(Token).Split('|');
            if (Elements.Length > 0)
            {
                Entities.Usuario USER = new Entities.Usuario();
                USER = DataAccess.DirectorioDA.GetUser(Elements[1], Elements[2]);
                if (USER.Id == Convert.ToInt32(Elements[0]))
                    Valid = true;
            }
            return Valid;
        }
        /// <summary>
        /// Convierte un string en su representación en hexadecimal.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private static string ToHex(string cadena)
        {
            string hex = "";
            char[] values = cadena.ToCharArray();
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                hex += String.Format("{0:x}", value);
            }
            return hex;
        }
        /// <summary>
        /// Converte una cadena de hexadecimal a un string, método inverso de ToHex
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static string HexToString(string hex)
        {
            string cadena = "";
            if (hex.Length % 2 == 0)
            {
                for (int i = 0; i < hex.Length; i = i + 2)
                {
                    int value = Convert.ToInt32(hex.Substring(i, 2), 16);
                    cadena += (char)value;
                }
            }
            else
            {
                throw new Exception("Cadena inválida");
            }
            return cadena;
        }
    }
}
