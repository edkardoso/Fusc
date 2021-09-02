using Fusc.Library.Validation;

namespace Fusc.Library.Core.Interfaces
{
    public interface IFResultMessage
    {
        string ErrorCode { get; }
        string Message { get; }
        Severity Severity { get; }

        IMessageModel ConvertToMessage();
    }
}