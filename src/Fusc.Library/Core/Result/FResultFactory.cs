using Fusc.Library.Core.Interfaces;
using Fusc.Library.Exceptions;
using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Core.Result
{
    public class FResultFactory
    {
        private static FResult Create(Func<dynamic, bool> funcSuccess, object data, IFResultMessage message = default, bool stop = false)
            => new FResult(funcSuccess, data, message, stop);

        public static FResult WithSuccess(object data = default, FResultMessage message = default, bool stop = false)
          => Create((_) => true, data, message, stop);

        public static FResult WithSuccess<T>(Func<dynamic, bool> funcSuccess, object data = default, FResultMessage message = default, bool stop = false)
            => Create(funcSuccess, data, message, stop);

        public static FResult WithSuccessVoid(object data = default, FResultMessage message = default, bool stop = false)
            => Create((d) => d == null, data, message, stop);

        public static FResult WithFail(object data = default, FResultMessage message = default, bool stop = false)
            => Create((_) => false, data, message, stop);

        public static FResult Validate(UseCaseValidationResult uCValidationResult)
            => new FResult(uCValidationResult);

        public static FResult IsTrue<T>(bool condition, T data, FResultMessage messageFailure = default, FResultMessage messageSuccess = default, bool stopFailure = false, bool stopSuccess = false)
            => condition ? WithSuccess(data, messageSuccess, stopSuccess) : WithFail(data, messageFailure, stopFailure);

        public static FResult IsNotNull<T>(T data)
            => Evaluate(data, _ => data != null);

        public static FResult Evaluate<T>(T data, Func<T, bool> func)
            => func.Invoke(data) ? WithSuccess(data) : WithFail(data);

        public static FResult ExceptionMyDomain(Exception exception, Type exceptionMyDomain, FResultMessage messageOtherExceptions, bool stop = true)
            => WithFail(message: exception.GetType() == exceptionMyDomain
                               ? FResultMessageFactory.FatalError(exception.Message)
                               : messageOtherExceptions, stop: stop);

        public static FResult Exception(string nameUseCase, Exception exception)
        {
            var message = new FResultMessage($"{nameUseCase} :: {exception}", Severity.FatalError, ((int)Severity.FatalError).ToString());

            if (exception is FuscExceptions exceptions)
            {
                message = exceptions.ConverToResultMessage();
            }

            return WithFail(message: message, stop: true);
        }

        public static FResultNull NullValidator(IUseCaseValidator validator, IUseCaseValidationResult validationResult) => new FResultNull(validator.IsObjectNull, validationResult.IsValid);

        public static FResultNull NullContinue() => new FResultNull(true, true);

        public static FResultNull NullStop() => new FResultNull(false, false);

    }
}
