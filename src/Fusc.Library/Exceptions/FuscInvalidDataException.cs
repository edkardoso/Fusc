using Fusc.Library.Validation;

namespace Fusc.Library.Exceptions
{
    public class FuscInvalidDataException : FuscExceptions
    {
        public FuscInvalidDataException():this("Errors occur during validation.", Severity.InvalidData)
        {
        }
        public FuscInvalidDataException(string message, Severity severity, string code = "0000") : base(message, severity, code)
        {
        }
    }
}
