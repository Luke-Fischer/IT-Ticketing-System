using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IT_Ticketing_System.Controllers
{
    public class Md5Security
    {
        public string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}
