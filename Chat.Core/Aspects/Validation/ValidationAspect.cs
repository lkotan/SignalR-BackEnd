using Castle.DynamicProxy;
using Chat.Core.Messages;
using Chat.Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Linq;
using ValidationException = Chat.Core.Exceptions.ValidationException;

namespace Chat.Core.Aspects.Validation
{
    public class ValidationAspect: MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new ValidationException(AspectMessage.WrongValidationType);
            }
            Priority = 2;
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            if (_validatorType.BaseType == null) return;
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
