using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Business.Hubs
{
    public class TestHub : Hub
    {
        public async Task SendMessage(string groupName,string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
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
