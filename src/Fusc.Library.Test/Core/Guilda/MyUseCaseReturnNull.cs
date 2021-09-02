using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Models;
using System.Text;

namespace Fusc.Library.Test.Core
{

  internal class MyUseCaseReturnNull : UseCase<StringBuilder, ResponseModel>
    {

        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((useCase, input) => null);
        }

    }
}
