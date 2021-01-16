namespace Chat.Core.Utilities.Results.Result
{
    public class Response : IResponse
    {
        protected Response(bool success, string message) : this(success)
        {
            Message = message;
        }

        protected Response(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
