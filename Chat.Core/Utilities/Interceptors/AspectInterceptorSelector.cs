using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Chat.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var mt = type.GetMethod(method.Name);
            var methodAttributes = (mt ?? throw new InvalidOperationException())
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
