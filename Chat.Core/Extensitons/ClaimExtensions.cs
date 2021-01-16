using System.Collections.Generic;
using System.Security.Claims;

namespace Chat.Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
        public static void AddUserData(this ICollection<Claim> claims, string userData)
        {
            claims.Add(new Claim(ClaimTypes.UserData, userData));
        }
    }
}
