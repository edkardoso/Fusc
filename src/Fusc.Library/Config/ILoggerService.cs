using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Config
{
    public interface ILoggerService
    {
        void Register(string useCaseName, Exception exception, Severity severity);

        void Register(string useCaseName, string message, Severity severity);
    }
}
