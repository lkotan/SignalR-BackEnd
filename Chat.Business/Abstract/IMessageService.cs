using Chat.Core.Repositories;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Abstract
{
    public interface IMessageService:IServiceRepository<MessageModel>
    {
        Task<IEnumerable<ListMessageModel>> GetRoomMessagesAsync(int roomId);
        Task<IDataResponse<ListMessageModel>> InsertMessageAsync(ListMessageModel model);
    }
}
