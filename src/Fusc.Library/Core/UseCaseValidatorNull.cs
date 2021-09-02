using Fusc.Library.Validation;

namespace Fusc.Library.Core
{
    public sealed class UseCaseValidatorNull<TRequest> : IUseCaseValidator<TRequest>
    {
        private dynamic _input;

       
        public dynamic Input => _input;

        public UseCaseValidationResult Result => new UseCaseValidationResult();

        TRequest IUseCaseValidator<TRequest>.Input => _input;
        public bool IsValid() => true;

        public void SetData(dynamic input)
        {
            _input = input;
        }
        public UseCaseValidationResult Validate()
        {
            return new UseCaseValidationResult();
        }
        public bool IsObjectNull => true;
    }
}
