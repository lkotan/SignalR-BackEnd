using AutoMapper;
using Chat.Business.Abstract;
using Chat.Core.Aspects.Security;
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
    public class RoomService : IRoomService
    {
        private readonly IDataAccessRepository<Room> _dal;
        private readonly IMapper _mapper;

        public RoomService(IDataAccessRepository<Room> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        [IsSuperAdminAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }

        [SecurityAspect]
        public async Task<RoomModel> GetAsync(int id)
        {
            return _mapper.Map<RoomModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [SecurityAspect]
        public async Task<IEnumerable<RoomModel>> GetAllAsync()
        {
            return _mapper.Map<List<RoomModel>>(await _dal.TableNoTracking.ToListAsync());
        }

        [IsSuperAdminAspect]
        public async Task<IDataResponse<int>> InsertAsync(RoomModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Room>(model));
        }

        [IsSuperAdminAspect]
        public async Task<IResponse> UpdateAsync(RoomModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Room>(model));
        }
    }
}
