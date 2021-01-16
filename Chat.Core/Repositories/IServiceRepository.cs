using System.Threading.Tasks;
using Chat.Core.Signatures;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;

namespace Chat.Core.Repositories
{
    public interface IServiceRepository<TModel> 
        where TModel : class, IBaseModel, new() 
    {
        /// <summary>
        /// Get Entitiy by identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Entity</returns>
        Task<TModel> GetAsync(int id);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="model">Entity</param>
        /// <returns>Response Message</returns>
        Task<IDataResponse<int>> InsertAsync(TModel model);
        
        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="model">Entities</param>
        /// <returns>Response Message</returns>
        Task<IResponse> UpdateAsync(TModel model);

        /// <summary>
        /// Delete entity by identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Response Message</returns>
        Task<IDataResponse<int>> DeleteAsync(int id);
    }
}