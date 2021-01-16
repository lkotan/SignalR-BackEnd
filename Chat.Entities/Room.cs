using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Entities
{
    public class Room: IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
