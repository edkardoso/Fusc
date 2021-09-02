using Fusc.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusc.Library.Validation
{
    public class UseCaseValidationResult : IUseCaseValidationResult
    {
        #region Fields

        private const string ERROR_CODE_DEFAULT = "0000";
        private readonly List<IFResultMessage> _errors = new List<IFResultMessage>();
        #endregion

        #region Properties
        public bool ContainErrorCritical => Messages.Any((e) => IsSeverityError(e.Severity));
        public bool IsValid => !ContainErrorCritical;
        public bool IsNotValid => !IsValid;
        public IReadOnlyCollection<IFResultMessage> Messages => _errors.AsReadOnly();

        #endregion

        #region Methods
        public void Add(IFResultMessage validationError) => _errors.Add(validationError);

        public void Add(string message, Severity severity = Severity.FatalError, string errorCode = ERROR_CODE_DEFAULT)
            => Add(FResultMessageFactory.CreateInstance(message, severity, errorCode));

        public void AddBusinessError(string message, string errorCode = ERROR_CODE_DEFAULT)
          => Add(FResultMessageFactory.BusinessError(message, errorCode));

        public void AddFatalError(string message, string errorCode = ERROR_CODE_DEFAULT)
            => Add(FResultMessageFactory.FatalError(message, errorCode));

        public void AddWarning(string message, string errorCode = ERROR_CODE_DEFAULT)
           => Add(FResultMessageFactory.Warning(message, errorCode));

        public void AddInfo(string message, string errorCode = ERROR_CODE_DEFAULT)
          => Add(FResultMessageFactory.Success(message, errorCode));

        private bool IsSeverityError(Severity severity)
        {
            return severity == Severity.Unauthorized
                || severity == Severity.BusinessError
                || severity == Severity.Denied
                || severity == Severity.Error
                || severity == Severity.FatalError
                || severity == Severity.Forbidden
                || severity == Severity.InvalidData;



        }


        #endregion
    }
}
