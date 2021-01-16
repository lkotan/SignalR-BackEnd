using Chat.Core.Enums;
using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Entities
{
    public class Account:IBaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Account;
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredDate { get; set; }

        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
