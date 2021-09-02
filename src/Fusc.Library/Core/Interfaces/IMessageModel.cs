using Fusc.Library.Validation;

namespace Fusc.Library.Core.Interfaces
{
    public interface IMessageModel
    {
        string Code { get; }
        string Message { get; }
        string Severity { get; }
    }
}
