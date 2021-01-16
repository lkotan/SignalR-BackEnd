namespace Chat.Core.Plugins.Authentication.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(int accountId);
    }
}