using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;
using System.Text;

namespace Fusc.Library.Test.Core
{

    internal class MyUseCaseOnAction : UseCase<StringBuilder, ResponseModel>
    {
        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((u, input) => AppendValue(input, $"{u.Name} => Action"));

        }

        private FResult AppendValue(StringBuilder input, string value)
        {
            input.Append(value);
            return FResultFactory.WithSuccess(input);
        }

    }

}
