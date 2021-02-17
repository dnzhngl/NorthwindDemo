using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil." /*AspectMessages.WrongValidationType*/);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection : girilen değeri çalışma anında instanceını oluşturuyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  // product validator'ın base type'ını bul. Onun generic argumanlarından ilkini bul. (çalışma tipini bul)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // ilk generic argumanın parametrelerini bul. - business katmanındaki methoda girilen parametre
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); // Validation tool'u kullanarak  validate et.
            }
        }
        //Invocation : Method demek
    }
}
