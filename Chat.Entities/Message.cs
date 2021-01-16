using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Entities
{
    public class Message: IBaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int AccountId { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public Account Account { get; set; }

    }
}
