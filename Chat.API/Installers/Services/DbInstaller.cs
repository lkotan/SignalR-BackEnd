using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chat.Business;
using Chat.DataAccess;
using Chat.API.Installers;

namespace Chat.API.Installers.Services
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatContext>(o => o.UseSqlServer(configuration.GetConnectionString("Api")));
            services.AddTransient<ChatContext>();
            services.AddTransient<DbContext, ChatContext>();
        }

        public void InstallConfigure(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ChatContext>();
            context.Database.Migrate();
            //SeedManager.SeedData(context);
        }
    }
}
