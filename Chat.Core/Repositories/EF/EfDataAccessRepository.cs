using Microsoft.EntityFrameworkCore;
using Chat.Core.Signatures;

namespace Chat.Core.Repositories.EF
{
    public class EfDataAccessRepository<TEntity> : EfRepository<TEntity, DbContext>, IDataAccessRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        public EfDataAccessRepository(DbContext context) : base(context)
        {
        }
    }
}