using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KontinentService.Helpers
{
    public static class HashCodeGenerator
    {
        public static string GetHash(string input)
        {
            input += "saltdefaultphrase";
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}