using Fusc.Library.Core.Interfaces;
using Fusc.Library.Helper;
using Fusc.Library.Validation;

namespace Fusc.Library.Models
{
    public sealed class MessageModel : IMessageModel
    {
        #region Properties

        public string Message { get; }
        public string Severity { get; }
        public string Code { get; }

        #endregion

        #region Constructors

        public MessageModel(string message, string severity = Constants.SEVERITY_DEFAULT , string code = Constants.ERROR_CODE_DEFAULT)
        {
            Message = message;
            Severity = severity;
            Code = code;
        }

        #endregion
    }
}
