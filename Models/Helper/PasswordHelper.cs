using System;
using System.Security.Cryptography;
using System.Text;

namespace DINGSOMETHING.Models.Helper
{
    public class PasswordHelper
    {
        public static string EncryptString(string data) {

            string combinedPassword = String.Concat(data, "EZgzhbK9IagPWNPxNE");
            SHA256Managed sha256 = new SHA256Managed();
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);

        }

    }


}