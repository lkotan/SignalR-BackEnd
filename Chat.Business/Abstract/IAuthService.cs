using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel loginModel);
        Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model);
        Task<IResponse> RegisterAsync(RegisterModel model);
        Task<IResponse> LogoutAsync();
    }
}
