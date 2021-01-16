using System.Diagnostics;
using System.Net.Http;
using Chat.Core.Plugins.Authentication;
using Chat.Core.Repositories;
using Chat.Core.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;
using Chat.Core.Plugins.Authentication.Jwt;
using Chat.Core.Utilities.IoC;

namespace Chat.Core.Extensions
{
    public static class ServiceInstallerExtensions
    {
        public static void InstallCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddSingleton<IUserService, UserJwtService>();
            services.AddSingleton<Stopwatch>();

            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<,>));
            services.AddTransient(typeof(IDataAccessRepository<>), typeof(EfDataAccessRepository<>));
            ServiceTool.Create(services);
        }
    }
}