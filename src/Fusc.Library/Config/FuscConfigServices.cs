using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fusc.Library.Config
{

    public static class FuscConfig
    {
        public static bool ErrorHandlingEventRequired { get; private set; } = false;
        public static bool StopWhenReturnIsNull { get; private set; } = true;
        public static bool ForceExceptionWhenReturnIsNull { get; private set; } = false;
        

        public static ILoggerService Logger { get; private set; }

        public static IServiceCollection SetupFusc(this IServiceCollection services,Func<FuscOptions, FuscOptions> options)
        {
            var _options = options.Invoke(new FuscOptions());

            ErrorHandlingEventRequired = _options.ErrorHandlingEventRequired;

            Logger = _options.Logger;

            return services;
        }
    }
}
