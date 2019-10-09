using System;
using System.Security.Cryptography;
using System.Text;

namespace bizapps_test.WEB
{
    public static class SignGenerator
    {

        public static string GetSign(string s)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] hash = provider.ComputeHash(Encoding.Default.GetBytes(s));
            return BitConverter.ToString(hash).ToLower().Replace("-", "");
        }
    }
}