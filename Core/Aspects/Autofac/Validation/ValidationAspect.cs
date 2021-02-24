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
    // ValidationAspect bir attribute.
    // Aspect -> methodun başında, sonunda, içinde, hata verdiğinde çalışacak olan
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) 
        {
            // Defensive coding -> Savunma amaçlı yazılan kodlar
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // parametre olarak gönderilen validorType bir IValidator mı, onu kontrol ediyor.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil." /*AspectMessages.WrongValidationType*/);
            }

            _validatorType = validatorType;
        }
        //Invocation : Method demek
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection : girilen değerin çalışma anında instanceını oluşturuyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  // product validator'ın base type'ını bul. Onun generic argumanlarından ilkini bul. (çalışma tipini bul)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // ilk generic argumanın parametrelerini bul. - business katmanındaki methoda girilen parametre
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); // Validation tool'u kullanarak  validate et.
            }
        }
    }
}
