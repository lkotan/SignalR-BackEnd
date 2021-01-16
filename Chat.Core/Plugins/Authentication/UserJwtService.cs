using System.Linq;
using Microsoft.AspNetCore.Http;
using Chat.Core.Extensions;
using Chat.Core.Plugins.Authentication.Models;

namespace Chat.Core.Plugins.Authentication
{
    public class UserJwtService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggedInUsers _loggedInUsers;
        public UserJwtService(IHttpContextAccessor httpContextAccessor, LoggedInUsers loggedInUsers)
        {
            _httpContextAccessor = httpContextAccessor;
            _loggedInUsers = loggedInUsers;
        }
        private UserInfo GetUser()
        {
            var userInfo = _httpContextAccessor?.HttpContext?.User;
            var accountId = userInfo.GetAccountId();

            return _loggedInUsers.UserInfo.FirstOrDefault(x => x.AccountId == accountId);
        }

        public UserInfo UserInfo => GetUser();

        public int AccountId => GetUser()?.AccountId ?? 0;

        public bool IsLogin => GetUser() != null;

        public string UserName => GetUser()?.UserName ?? "";

    }
}