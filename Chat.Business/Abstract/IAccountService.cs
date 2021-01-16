using Chat.Core.Repositories;
using Chat.Core.Utilities.Results.Result;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Abstract
{
    public interface IAccountService:IServiceRepository<AccountModel>
    {
        Task<IEnumerable<AccountModel>> GetAllAsync();
        Task<IResponse> RoomJoin(int roomId);
        Task<IResponse> RoomLeft();
    }
}
