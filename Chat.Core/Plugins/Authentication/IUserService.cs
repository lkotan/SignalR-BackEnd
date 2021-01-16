using Chat.Core.Plugins.Authentication.Models;

namespace Chat.Core.Plugins.Authentication
{
    public interface IUserService
    {
        UserInfo UserInfo { get; }
        int AccountId { get; }
        bool IsLogin { get; }
        string UserName { get; }
    }
}