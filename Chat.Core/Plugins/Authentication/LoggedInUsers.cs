using System.Collections.Generic;
using Chat.Core.Plugins.Authentication.Models;

namespace Chat.Core.Plugins.Authentication
{
    public class LoggedInUsers
    {
        public LoggedInUsers()
        {
            UserInfo = new List<UserInfo>();
        }
        public List<UserInfo> UserInfo { get; set; }
    }
}
