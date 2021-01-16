using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chat.API.Installers.Profiles;
using Chat.Core.Utilities.IoC;

namespace Chat.API.Installers.Services
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            ServiceTool.Create(services);
        }

        public void InstallConfigure(IApplicationBuilder app)
        {

        }
    }
}