using Chat.Core.Utilities.Results.Result;

namespace Chat.Core.Utilities.Results.DataResult
{
    
    public interface IDataResponse<out T> : IResponse
    {
        T Data { get; }
    }
}