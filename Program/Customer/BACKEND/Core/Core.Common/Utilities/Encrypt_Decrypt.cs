using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common.Utilities
{
    public class Encrypt_Decrypt
    {
       
        public static string base64Encode(string data)
        {
           
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string base64Decode(string sData)
        {
           
            var base64EncodedBytes = System.Convert.FromBase64String(sData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new System.Security.Cryptography.
                RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static string EncodePassword(string pass, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(pass);
            var src = Convert.FromBase64String(salt);
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pass + salt));
            return Convert.ToBase64String(data);
        }
        public static string GenerateOTP()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }
        //// hash and save a password
        public static string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password,10);
        }

        //Check a password

        public static bool comparePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
