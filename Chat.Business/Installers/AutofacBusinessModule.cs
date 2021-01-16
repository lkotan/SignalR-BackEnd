using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Chat.Core.Utilities.Interceptors;
using Module = Autofac.Module;

namespace Chat.Business.Installers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var x = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(x).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() }).SingleInstance();
        }
    }
}
