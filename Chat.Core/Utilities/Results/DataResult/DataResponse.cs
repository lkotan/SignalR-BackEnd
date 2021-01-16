using System.Collections.Generic;
using System.Linq;
using Chat.Core.Utilities.Results.Result;

namespace Chat.Core.Utilities.Results.DataResult
{
   
    public class DataResponse<T> : Response, IDataResponse<T>
    {
        protected DataResponse(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
        protected DataResponse(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
    public class PaginationDataResponse<T>
    {
        public PaginationDataResponse(List<T> data, int page, int listing)
        {
            Data = data.Skip((page - 1) * listing).Take(listing).ToList();
            TotalPage = data.Count / listing + (data.Count % listing == 0 ? 0 : 1);
            Page = page;
            Listing = Data.Count;
            TotalItem = data.Count;
        }

        public List<T> Data { get; }
        public int Page { get; }
        public int Listing { get; }
        public int TotalPage { get; }
        public int TotalItem { get; }
    }
}
