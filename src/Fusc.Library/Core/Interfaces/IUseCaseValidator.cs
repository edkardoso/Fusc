using Fusc.Library.Core.Interfaces;
using Fusc.Library.Validation;

namespace Fusc.Library.Core
{
    public interface IUseCaseValidator : IObjecNull
    {
        object Input { get; }
        bool IsValid();
        UseCaseValidationResult Validate();
        void SetData(object input);
       
    }
    public interface IUseCaseValidator<TRequest> : IUseCaseValidator
    {
        new TRequest Input { get; }
    }
}
