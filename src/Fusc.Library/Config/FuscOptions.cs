namespace Fusc.Library.Config
{
    public class FuscOptions
    {
        public bool ErrorHandlingEventRequired { get; set; }

        public ILoggerService Logger { get; set; }
    }
}
