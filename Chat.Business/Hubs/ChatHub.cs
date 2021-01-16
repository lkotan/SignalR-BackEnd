using Chat.Business.Abstract;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Repositories;
using Chat.Entities;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Business.Hubs
{
    public class ChatHub:Hub
    {

        private readonly LoggedInUsers _loggedInUsers;
        private readonly IDataAccessRepository<Account> _dalAccount;

        public ChatHub(LoggedInUsers loggedInUsers,IDataAccessRepository<Account> dalAccount)
        {
            _loggedInUsers = loggedInUsers;
            _dalAccount = dalAccount;
        }

        public async Task GetCountAccounts()
        {
            await Clients.All.SendAsync("CountAccounts", _loggedInUsers.UserInfo.ToList().Count);
        }

        public async Task SendMessage(ListMessageModel model,string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", model);
        }

        public async Task GetRoomAccount(int roomId,string groupName)
        {
            var entities=await _dalAccount.TableNoTracking.Where(x => x.RoomId==roomId).Select(x=>new RoomAccountModel {
                AccountId=x.Id,
                UserName=x.UserName
            }).ToListAsync();
            var count = entities.Count;

            await Clients.Group(groupName).SendAsync("ReceiveRoomAccount", entities,count);
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("AddGroup", $"{Context.ConnectionId} in add group {groupName}");
        }
        public async Task RemoveToGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("RemoveGroup", $"{Context.ConnectionId} in remove group {groupName}");
        }

     
    }
}
