using Fusc.Library.Core.Result;
using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Core.Interfaces
{
    public interface IBuilder<TResponse> where TResponse : IResponseModel
    {
        IBuilder<TResponse> CaseIsNotValid(Func<dynamic, UseCaseValidationResult, FResult> func);
        IBuilder<TResponse> CaseIsValid(Func<dynamic, UseCaseValidationResult, FResult> func);
        IBuilder<TResponse> OnBefore(Action<IUseCase<TResponse>, dynamic> act);
        IBuilder<TResponse> OnAction(Func<IUseCase<TResponse>, dynamic, FResult> func);
        IBuilder<TResponse> OnSuccess(Func<IUseCase<TResponse>, FResult> func);
        IBuilder<TResponse> OnFail(Func<IUseCase<TResponse>, FResult> func);
        IBuilder<TResponse> OnException(Func<Exception, FResult> func);
        IBuilder<TResponse> OnCompleted(Action<IUseCase<TResponse>> act);
    }


}
