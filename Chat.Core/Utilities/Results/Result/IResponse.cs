namespace Chat.Core.Utilities.Results.Result
{
    public interface IResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}