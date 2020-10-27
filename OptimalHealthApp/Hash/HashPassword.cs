using System;
using System.Security.Cryptography;
using System.Text;

namespace OptimalHealthApp.Hash
{
    public class HashPassword
    {
        public String GenerateSaltedHash(string plainText)
        {
            string salt = "GrønnKlumpfot";

            var saltedPassword = ASCIIEncoding.ASCII.GetBytes(plainText + "" + salt);
            byte[] b = System.Text.Encoding.Unicode.GetBytes(plainText + "" + salt);
            SHA512 sha = new SHA512Managed();
            byte[] hashedBytes = sha.ComputeHash(b);
            return Convert.ToBase64String(hashedBytes);
        }

        public static String GenerateSaltedHash2(string plainText)
        {
            string salt = "GrønnKlumpfot";

            var saltedPassword = ASCIIEncoding.ASCII.GetBytes(plainText + "" + salt);
            byte[] b = System.Text.Encoding.Unicode.GetBytes(plainText + "" + salt);
            SHA512 sha = new SHA512Managed();
            byte[] hashedBytes = sha.ComputeHash(b);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
