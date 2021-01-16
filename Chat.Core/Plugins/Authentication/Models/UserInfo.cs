using Chat.Core.Enums;
using System.Collections.Generic;

namespace Chat.Core.Plugins.Authentication.Models
{
    public class UserInfo
    {
        public AccountType AccountType { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }
}
