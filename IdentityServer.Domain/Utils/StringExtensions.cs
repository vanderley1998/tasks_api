using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LubyTasks.Domain.Utils
{
    public static class StringExtensions
    {
        public static String GetSHA512(this string text)
        {
            SHA512 shaM = new SHA512Managed();
            var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }
    }
}
