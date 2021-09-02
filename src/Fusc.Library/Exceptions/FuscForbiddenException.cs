using Fusc.Library.Validation;

namespace Fusc.Library.Exceptions
{
    public class FuscForbiddenException : FuscExceptions
    {
        public FuscForbiddenException() : base("Access Denied", Severity.Forbidden, "SC403")
        {
        }
    }
}
