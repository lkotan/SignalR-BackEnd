using AutoMapper;
using Chat.Entities;
using Chat.Models;

namespace Chat.API.Installers.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomModel>();
            CreateMap<RoomModel, Room>();

            CreateMap<Account, AccountModel>();
            CreateMap<AccountModel, Account>();

            CreateMap<Account, RegisterModel>();
            CreateMap<RegisterModel, Account>();


            CreateMap<Message, MessageModel>();
            CreateMap<MessageModel, Message>();

            CreateMap<Message, ListMessageModel>();
            CreateMap<ListMessageModel, Message>();
        }
    }
}
