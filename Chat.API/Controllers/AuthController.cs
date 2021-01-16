using Chat.Business.Abstract;
using Chat.Core.Messages;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Plugins.Authentication.Models;
using Chat.Core.Utilities.Results.DataResult;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IUserService _userService;

        public AuthController(IAuthService service,IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _service.LoginAsync(model);
            if (result.Success) return Ok(result);
            return Unauthorized(result.Message);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return Ok(await _service.RegisterAsync(model));
        }

        [HttpPost("LoginByRefreshToken")]
        public async Task<IActionResult> LoginByRefreshToken([FromBody] RefreshTokenModel model)
        {
            var result = await _service.LoginByRefreshTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(await _service.LogoutAsync());
        }
        [HttpGet("User")]
        public async Task<IActionResult> UserInfo()
        {
            await Task.CompletedTask;
            if (_userService.IsLogin)
                return Ok(new SuccessDataResponse<UserInfo>(_userService.UserInfo));
            return Unauthorized(AccountMessage.AuthenticationError);
        }
    }
}
