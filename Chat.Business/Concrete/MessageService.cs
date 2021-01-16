using AutoMapper;
using Chat.Business.Abstract;
using Chat.Core.Aspects.Security;
using Chat.Core.Repositories;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;
using Chat.Entities;
using Chat.Models;
using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Concrete
{
    [SecurityAspect]
    public class MessageService : IMessageService
    {
        private readonly IDataAccessRepository<Message> _dal;
        private readonly IMapper _mapper;

        public MessageService(IDataAccessRepository<Message> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }

        public async Task<IEnumerable<ListMessageModel>> GetRoomMessagesAsync(int roomId)
        {
            return await _dal.TableNoTracking
                .Include(x => x.Account)
                .Include(x => x.Room)
                .Where(x=>x.RoomId==roomId)
                .Select(x => new ListMessageModel
                {
                    Id=x.Id,
                    AccountId=x.Account.Id,
                    RoomId=x.Room.Id,
                    Text=x.Text,
                    RoomName=x.Room.Name,
                    UserName=x.Account.UserName,
                    CreatedAt=x.CreatedAt
                }).ToListAsync();
        }

        public async Task<MessageModel> GetAsync(int id)
        {
            return _mapper.Map<MessageModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x=>x.Id==id));
        }

        public Task<IDataResponse<int>> InsertAsync(MessageModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResponse<ListMessageModel>> InsertMessageAsync(ListMessageModel model)
        {
            var result = await _dal.InsertAsync(_mapper.Map<Message>(model));
            var resultModel = await _dal.TableNoTracking
                .Include(x => x.Room)
                .Include(x => x.Account)
                .Where(x => x.Id == result.Data)
                .Select(x => new ListMessageModel
                {
                    Id = x.Id,
                    AccountId = x.Account.Id,
                    RoomId = x.Room.Id,
                    Text = x.Text,
                    RoomName = x.Room.Name,
                    UserName = x.Account.UserName,
                    CreatedAt = x.CreatedAt
                }).FirstOrDefaultAsync();
            return new SuccessDataResponse<ListMessageModel>(resultModel);
        }

        public async Task<IResponse> UpdateAsync(MessageModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Message>(model));
        }
        
    }
}
