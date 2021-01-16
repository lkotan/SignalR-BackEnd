using Chat.Core.Enums;
using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Models
{
    public class AccountModel:IBaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public int? RoomId { get; set; }
    }
}
