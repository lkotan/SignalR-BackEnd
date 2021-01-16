using AutoMapper;
using Chat.Business.Abstract;
using Chat.Core.Aspects.Security;
using Chat.Core.Messages;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Repositories;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;
using Chat.Entities;
using Chat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IDataAccessRepository<Account> _dal;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountService(IDataAccessRepository<Account> dal, IUserService userService, IMapper mapper)
        {
            _dal = dal;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IResponse> RoomJoin(int roomId)
        {
            var entity = await _dal.GetAsync(_userService.AccountId);
            entity.RoomId = roomId;
            return await _dal.UpdateAsync(entity);
        }

        public async Task<IResponse> RoomLeft()
        {
            var entity = await _dal.GetAsync(_userService.AccountId);
            entity.RoomId = null;
            return await _dal.UpdateAsync(entity);
        }

        [IsSuperAdminAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }

        [IsSuperAdminAspect]
        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            return _mapper.Map<List<AccountModel>>(await _dal.TableNoTracking.ToListAsync());
        }


        [SecurityAspect]
        public async Task<AccountModel> GetAsync(int id)
        {
            return _mapper.Map<AccountModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x=>x.Id==id));
        }

        [SecurityAspect]
        public Task<IDataResponse<int>> InsertAsync(AccountModel model)
        {
            throw new NotImplementedException();
        }

        [SecurityAspect]
        public async Task<IResponse> UpdateAsync(AccountModel model)
        {
            var entity = await _dal.GetAsync(x => x.Id == model.Id);
            if (entity == null) return new ErrorResponse(DbMessage.DataNotFound);

            entity.Email = model.Email;
            entity.AccountType = model.AccountType==0 ? Core.Enums.AccountType.Account : model.AccountType;
            entity.UserName = model.UserName;

            return await _dal.UpdateAsync(entity);
        }
    }
}
