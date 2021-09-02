using Fusc.Library.Core.Interfaces;
using Fusc.Library.Models;

namespace Fusc.Library.Validation
{
    public class FResultMessage : IFResultMessage
    {
        #region Fields

        private readonly string _message;
        private readonly Severity _severity;
        private readonly string _errorCode;

        #endregion

        #region Properties
        public string ErrorCode => _errorCode;
        public string Message => _message;
        public Severity Severity => _severity;

        #endregion

        #region Constructors
        public FResultMessage(string message, Severity severity = Severity.BusinessError, string errorCode = "000")
        {
            _errorCode = errorCode;
            _message = message;
            _severity = severity;
        }


        #endregion


        public IMessageModel ConvertToMessage() => MessageModelFactory.Create(_message, _severity, _errorCode);

    }
}
