using System.Linq;
using System.Security.Claims;
using Chat.Core.Plugins.Authentication.Models;

namespace Chat.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.Name)?.Select(x => x.Value).FirstOrDefault();
        }
        public static string GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).FirstOrDefault();
        }

        public static int GetAccountId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).FirstOrDefault()?.ToInt() ?? 0;
        }
    }
}
