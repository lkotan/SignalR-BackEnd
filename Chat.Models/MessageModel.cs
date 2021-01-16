using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Chat.Models
{
    public class MessageModel:IBaseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int RoomId { get; set; }
        public int AccountId { get; set; }
    }

    public class ListMessageModel : IBaseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoomId { get; set; }
        public int AccountId { get; set; }

        public string RoomName { get; set; }
        public string UserName { get; set; }

    }
}
