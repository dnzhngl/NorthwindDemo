using FluentValidation.Results;
using System.Collections.Generic;

namespace Core.Extensions
{
    // Validation hataları olması durumunda kullanılması gereken hata sınıfı
    // ErrorDetails'den inherit alır. Böylece içeriside StatusCode ve Message yapısıda olur.
    public class ValidationErrorDetails: ErrorDetails
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
    }
}
