using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Exceptions
{


    public class FuscExceptions : Exception
    {
        public FuscExceptions(string message, Severity severity, string code = "0000") : base(message)
        {
            Severity = severity;
            Code = code;
        }

        public Severity Severity { get; }
        public string Code { get; }

        public FResultMessage ConverToResultMessage() => new FResultMessage(Message, Severity, Code);
    }
}
