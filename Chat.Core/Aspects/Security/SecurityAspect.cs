using System;
using System.Linq;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Chat.Core.Exceptions;
using Chat.Core.Extensions;
using Chat.Core.Messages;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Utilities.Interceptors;
using Chat.Core.Utilities.IoC;
using Chat.Core.Enums;

namespace Chat.Core.Aspects.Security
{
    public class SecurityAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private LoggedInUsers _loggedInUsers;
        public SecurityAspect()
        {

        }

        protected override void OnBefore(IInvocation invocation)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _loggedInUsers = ServiceTool.ServiceProvider.GetService<LoggedInUsers>();

            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var accountId = user.GetAccountId();
            if (accountId == 0)
                throw new AuthenticationException(AspectMessage.AuthenticationError);

            var userInfo = _loggedInUsers.UserInfo.FirstOrDefault(x => x.AccountId == accountId);
            if (userInfo == null)
                throw new SecurityException(AspectMessage.AccessDenied);

            //var isSuperVisor = userInfo.AccountType == AccountType.SuperAdmin;
            //if (isSuperVisor)
            //    return;

            return;
        }
    }
}
