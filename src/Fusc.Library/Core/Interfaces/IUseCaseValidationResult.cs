using Fusc.Library.Validation;
using System.Collections.Generic;

namespace Fusc.Library.Core.Interfaces
{
    public interface IUseCaseValidationResult
    {
        bool ContainErrorCritical { get; }
        IReadOnlyCollection<IFResultMessage> Messages { get; }
        bool IsValid { get; }
        bool IsNotValid { get; }
        void Add(string message, Severity severity = Severity.FatalError, string errorCode = "0000");
        void Add(IFResultMessage validationError);
        void AddFatalError(string message, string errorCode = "0000");
        void AddInfo(string message, string errorCode = "0000");
        void AddWarning(string message, string errorCode = "0000");
    }
}