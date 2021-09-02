using Fusc.Library.Validation;

namespace Fusc.Library.Exceptions
{
    public class FuscUnauthorizedException : FuscExceptions
    {
        public FuscUnauthorizedException() : base("Not Authorized", Severity.Unauthorized, "SC401")
        {
        }
    }
}
