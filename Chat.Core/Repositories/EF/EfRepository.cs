using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chat.Core.Messages;
using Chat.Core.Signatures;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;

namespace Chat.Core.Repositories.EF
{
    public class EfRepository<TEntity, TContext> : IRepository<TEntity>
         where TEntity : class, IBaseEntity, new()
         where TContext : DbContext
    {
        private readonly TContext _context;
        private DbSet<TEntity> _entities;

        protected EfRepository(TContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entities.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IDataResponse<int>> InsertAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorDataResponse<int>(DbMessage.DataNotFound);

            try
            {
                Entities.Add(entity);
                await SaveChangesAsync();
                return new SuccessDataResponse<int>(entity.Id, DbMessage.DataInserted);
            }
            catch (Exception e)
            {
                return new ErrorDataResponse<int>(e.Message);
            }
        }
        public async Task<IResponse> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorResponse(DbMessage.DataNotFound);
            try
            {
                var local = _context.Set<TEntity>().Local.FirstOrDefault(entry => entry.Id.Equals(entity.Id));
                if (local != null)
                    _context.Entry(local).State = EntityState.Detached;

                Entities.Update(entity);
                await SaveChangesAsync();
                return new SuccessResponse(DbMessage.DataUpdated);
            }
            catch (DbUpdateException e)
            {
                return new ErrorResponse(e.Message);
            }
            catch (Exception e)
            {
                return new ErrorResponse($"{e.Message} {e.InnerException}");
            }
        }

        public async Task<IDataResponse<int>> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                return new ErrorDataResponse<int>(DbMessage.DataNotFound);

            try
            {
                Entities.Remove(entity);
                await SaveChangesAsync();
                return new SuccessDataResponse<int>(entity.Id, DbMessage.DataRemoved);
            }
            catch (Exception e)
            {
                return new ErrorDataResponse<int>(entity.Id,e.Message);
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        private DbSet<TEntity> Entities => _entities ??= _context.Set<TEntity>();
        public IQueryable<TEntity> Table => Entities;
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
    }
}
