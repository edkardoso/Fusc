using Fusc.Library.Core;
using Fusc.Library.Helper;
using Fusc.Library.Validation;

namespace Fusc.Library.Exceptions
{

    public class FuscDevelopmentException : FuscExceptions
    {
        public FuscDevelopmentException(string module, string nameUseCase, string message, string code = Constants.ERROR_CODE_DEFAULT)
            : base($"{module} :: {nameUseCase} :: {message}", Severity.FatalError, code)
        {
        }

        public static void PostWhenRequired(bool validadorIsNotNull, bool resultIsNUll, bool errorHandlingEventRequired, string nameUseCase)
        {
            if (validadorIsNotNull && resultIsNUll && errorHandlingEventRequired)
                throw new FuscDevelopmentException("Flow", nameUseCase, @"A 'validator' was defined but the 'CaseIsValid' or 'CaseIsNotValid' event for eventual errors was not implemented.");

        }
    }
}
