using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Models;

namespace Fusc.Library.Test.Core
{
    internal class MyUseCaseException : UseCase<int, ResponseModel>
    {

        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((useCase, input) => input / 0);
              
        }

    }

}
