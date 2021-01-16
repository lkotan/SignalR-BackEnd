using Chat.Core.Signatures;

namespace Chat.Core.Repositories
{
    public interface IDataAccessRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {

    }
}