using Fusc.Library.Exceptions;
using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Core
{

    public abstract class UseCaseValidator<TRequest> : IUseCaseValidator<TRequest>
    {
        public UseCaseValidationResult Result { get; private set; }

        public TRequest Input { get; private set; }

        dynamic IUseCaseValidator.Input => Input;

        public bool IsObjectNull => false;

        protected UseCaseValidator(TRequest input)
        {
            Result = new UseCaseValidationResult();

            Input = input;
        }

        public bool IsValid()
        {

            if (Result is null)
                Result = Validate();

            if (Result is null)
                throw new FuscDevelopmentException("Validador", "Unknown", "The validation method returned null");

            return Result.IsValid;

        }
        public abstract UseCaseValidationResult Validate();

        public void SetData(TRequest input)
        {
            Input = input;
        }


        public void SetData(object input)
        {
            Input = (TRequest)input;
        }
    }
}
