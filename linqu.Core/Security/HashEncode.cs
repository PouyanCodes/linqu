
using System;
using System.Security.Cryptography;
using System.Text;


namespace linqu.Core.Security
{
    public static class HashEncode
    {
        public static string GetHashedString(string text)
        {
            byte[] mainBytes;
            byte[] encodeBytes;

            MD5 md5 = new MD5CryptoServiceProvider();

            mainBytes = ASCIIEncoding.Default.GetBytes(text);
            encodeBytes = md5.ComputeHash(mainBytes);

            return BitConverter.ToString(encodeBytes);
        }
    }
}
