namespace Fusc.Library.Validation
{
    public class FResultMessageFactory
    {
        public static FResultMessage CreateInstance(string message, Severity severity, string code = default) => new FResultMessage(message, severity, code);
        public static FResultMessage Success(string message, string code = default) => CreateInstance(message, Severity.Info, code);
        public static FResultMessage Warning(string message, string code = default) => CreateInstance(message, Severity.Warning, code);
        public static FResultMessage Created(string message, string code = default) => CreateInstance(message, Severity.Created, code);
        public static FResultMessage NotFoundExcluded(string message, string code = default) => CreateInstance(message, Severity.NoContent, code);
        public static FResultMessage NotFound(string message, string code = default) => CreateInstance(message, Severity.Info, code);
        public static FResultMessage BusinessError(string message, string code = default) => CreateInstance(message, Severity.BusinessError, code);
        public static FResultMessage InvalidData(string message, string code = default) => CreateInstance(message, Severity.InvalidData, code);
        public static FResultMessage Unauthorized(string message, string code = default) => CreateInstance(message, Severity.Unauthorized, code);
        public static FResultMessage Forbidden(string message, string code = default) => CreateInstance(message, Severity.Forbidden, code);
        public static FResultMessage FatalError(string message, string code = default) => CreateInstance(message, Severity.FatalError, code);
    }
}
