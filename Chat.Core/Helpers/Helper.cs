using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Chat.Core.Helpers
{
    public static class Helper
    {
        public static string CreateToken()
        {
            var number = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            var token = Convert.ToBase64String(number).ToUpper().Replace("İ", "I");
            var source = new[] {
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','R','S','T','U','V','Y','Z','W','X',
                '0','1','2','3','4','5','6','7','8','9'
            };
            var builder = new StringBuilder();
            foreach (var ch in token.Where(ch => source.Contains(ch)))
            {
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}