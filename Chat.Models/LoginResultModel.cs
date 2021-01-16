using Chat.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Models
{
    public class LoginResultModel
    {
        public int AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
