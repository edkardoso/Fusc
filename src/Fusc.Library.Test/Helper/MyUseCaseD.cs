using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;
using Fusc.Library.Validation;

namespace Fusc.Library.Test.Helper
{
    public class MyUseCaseD : UseCase<int, ResponseModel>
    {
        private int _value;

        public MyUseCaseD() : base()
        {
        }

        public void Execute(int value)
        {
            _value = value;

            base.Execute();
        }

        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder
                .OnAction((_, __) => FResultFactory.Evaluate<int>(_value, (_) => _value > 0))
                .OnSuccess((useCase) => FResultFactory.WithSuccess(((int)useCase.Result.Data) * 2))
                .OnFail((useCase) => FResultFactory.WithFail(useCase.Result.Data, FResultMessageFactory.BusinessError("Value is 0 or less.")));
        }
    }
}
