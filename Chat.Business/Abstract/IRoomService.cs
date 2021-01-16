using Chat.Core.Repositories;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Abstract
{
    public interface IRoomService:IServiceRepository<RoomModel>
    {
        Task<IEnumerable<RoomModel>> GetAllAsync();
    }
}
