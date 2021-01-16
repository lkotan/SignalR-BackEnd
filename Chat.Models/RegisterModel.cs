using Chat.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
