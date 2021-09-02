using System;

namespace Fusc.Library.Validation
{
    [Flags]
    public enum Severity
    {
        Info = 0,
        Warning = 1,
        Created = 2,
        NoContent = 4,
        Success = Info | Warning | Created | NoContent,
        Unauthorized = 8,
        Forbidden = 16,
        Denied = Forbidden | Unauthorized,
        InvalidData = 32,
        BusinessError = 64,
        FatalError = 128,
        Error = Denied | InvalidData | NoContent | BusinessError | FatalError

    }
}
