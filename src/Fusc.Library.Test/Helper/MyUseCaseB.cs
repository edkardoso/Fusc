using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Models;
using System;

namespace Fusc.Library.Test.Helper
{
    public class MyUseCaseB : UseCase<ResponseModel>
    {
        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((_, __) => {
                throw new Exception();
                               
            });
        }
    }
}
