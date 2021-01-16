using Chat.Business.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.API.Installers.Services
{
    public class SignalRInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
        }
    }
}
