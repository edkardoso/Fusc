using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;

namespace Fusc.Library.Test.Helper
{
    public class MyUseCaseA : UseCase<ResponseModel>
    {
        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((u, _) => FResultFactory.WithSuccess(data: new object[] { 1 }));
             
        }
    }
}
