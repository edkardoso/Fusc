using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;

namespace Fusc.Library.Test.Helper
{
    public class MyUseCaseC : UseCase<int, ResponseModel>
    {
        private int _value;

        public MyUseCaseC() : base()
        {
        }

        public void Execute(int value)
        {
            _value = value;

            base.Execute();
        }

        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder.OnAction((_, __) => FResultFactory.WithSuccess(_value));
        }
    }
}
