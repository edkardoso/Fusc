using Fusc.Library.Helper;
using Fusc.Library.Validation;

namespace Fusc.Library.Models
{
    public class MessageModelFactory
    {
        public static MessageModel Create(string message, Severity severity = Severity.Info, string code = Constants.ERROR_CODE_DEFAULT)
            => new MessageModel(message, severity.ToString().ToUpper(), code);
    }
}
