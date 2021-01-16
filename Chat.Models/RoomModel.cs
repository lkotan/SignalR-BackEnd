using Chat.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Models
{
    public class RoomModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RoomAccountModel
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }
}
