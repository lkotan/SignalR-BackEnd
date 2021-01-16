using AutoMapper;
using Chat.Business.Abstract;
using Chat.Core.Aspects.Security;
using Chat.Core.Enums;
using Chat.Core.Exceptions;
using Chat.Core.Helpers;
using Chat.Core.Messages;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Plugins.Authentication.Jwt;
using Chat.Core.Plugins.Authentication.Models;
using Chat.Core.Repositories;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Core.Utilities.Results.Result;
using Chat.Entities;
using Chat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IDataAccessRepository<Account> _dal;
        private readonly IMapper _mapper;

        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly LoggedInUsers _loggedInUsers;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IDataAccessRepository<Account> dal,IMapper mapper, ITokenHelper tokenHelper, IUserService userService,LoggedInUsers loggedInUsers, JwtOptions jwtOptions)
        {
            _dal = dal;

            _tokenHelper = tokenHelper;
            _userService = userService;
            _loggedInUsers = loggedInUsers;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
        }


        private async Task<IDataResponse<LoginResultModel>> LoginAsync(Account account,bool isRefreshLogin = false)
        {
            if (account.Email == "lutfikotann@gmail.com")
                account.AccountType = AccountType.SuperAdmin;

            var user = new UserInfo
            {
                AccountId = account.Id,
                AccountType = account.AccountType,
                UserName=account.UserName,
            };

            var accessToken = _tokenHelper.CreateToken(account.Id);
            var tokenOptions = _jwtOptions;
            var acc = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == account.Id);

            acc.RefreshToken = accessToken.RefreshToken;
            acc.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 30);

            await _dal.UpdateAsync(acc);

            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.AccountId != account.Id).ToList();
            _loggedInUsers.UserInfo.Add(user);

            var result = new LoginResultModel
            {
                AccountId = account.Id,
                UserName=account.UserName,
                AccountType=account.AccountType,
                Token = accessToken.Token,
                RefreshToken = accessToken.RefreshToken,
                TokenExpiration = DateTime.Now
            };
            return new SuccessDataResponse<LoginResultModel>(result);

        }

        public async Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel loginModel)
        {
            var account = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
            if (account == null)
                return new ErrorDataResponse<LoginResultModel>(AccountMessage.AccountNotFound);

            if (HashingHelper.VerifyPasswordHash(loginModel.Password, account.PasswordHash, account.PasswordSalt))
                return await LoginAsync(account);

            return new ErrorDataResponse<LoginResultModel>(AccountMessage.PasswordWrong);
        }

        public async Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model)
        {
            var account = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.RefreshToken == model.Token);
            if (account == null)
            {
                throw new AuthenticationException(AccountMessage.AuthenticationError);
            }
            if (!string.IsNullOrEmpty(account.RefreshToken) && account.RefreshTokenExpiredDate > DateTime.Now)
            {
                return await LoginAsync(account, true);
            }
            throw new AuthenticationException(AccountMessage.AuthenticationError);
        }

        [SecurityAspect]
        public async Task<IResponse> LogoutAsync()
        {
            var account = await _dal.GetAsync(x => x.Id == _userService.AccountId);
            account.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(-30);
            await _dal.UpdateAsync(account);
            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.AccountId != account.Id).ToList();
            return new SuccessResponse(AccountMessage.LogoutSuccessful);
        }

        public async Task<IResponse> RegisterAsync(RegisterModel model)
        {
            var account=await AccountExistsAsync(model.Email);
            if (!account.Success) return new ErrorResponse(AccountMessage.AccountExists);

            var entity = _mapper.Map<Account>(model);
            HashingHelper.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            entity.RefreshToken = Helper.CreateToken();
            entity.RefreshTokenExpiredDate = DateTime.Now.AddDays(-1);
            var result = await _dal.InsertAsync(entity);

            if (!result.Success) return result;
            return new SuccessResponse(DbMessage.DataInserted);
        }

        private async Task<IResponse> AccountExistsAsync(string email)
        {
            var entity = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null) return new ErrorResponse();
            return new SuccessResponse();
        }
    }
}
