using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Common
{
    public static class EncryptHelper
    {
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            var bytes = Encoding.UTF8.GetBytes(toEncrypt);

            byte[] numArray;
            if (useHashing)
            {
                var cryptoServiceProvider = new MD5CryptoServiceProvider();
                numArray = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes("QuestionBackEnd"));
                cryptoServiceProvider.Clear();
            }

            else
            {
                numArray = Encoding.UTF8.GetBytes("QuestionBackEnd");
            }

            var cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider1.Key = numArray;
            cryptoServiceProvider1.Mode = CipherMode.ECB;
            cryptoServiceProvider1.Padding = PaddingMode.PKCS7;
            var inArray = cryptoServiceProvider1.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            cryptoServiceProvider1.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }
    }
}
