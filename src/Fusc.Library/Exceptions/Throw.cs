using Fusc.Library.Core.Interfaces;
using Fusc.Library.Helper;
using System;

namespace Fusc.Library.Exceptions
{
    public class Throw
    {
        public static void Development(Type type, string message, IUseCase<IResponseModel> useCase = null, string code = Constants.ERROR_CODE_DEFAULT)
        {
            var nameUseCase = useCase == null ? "Unknown" : useCase.Name;
            throw new FuscDevelopmentException(type.Namespace, nameUseCase, message, code);
        }

     
    }
}
