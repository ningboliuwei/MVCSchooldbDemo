using System;
using System.Security.Cryptography;
using System.Text;

namespace MVCSchooldbDemo.Common
{
    public class UtilityHelper
    {
        public static string MD5(string encryptString)
        {
            var result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(result);
            var encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult;
        }
    }
}