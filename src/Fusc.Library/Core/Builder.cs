using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Core
{
    public class Builder<TResponse> : IBuilder<TResponse> where TResponse: IResponseModel
    {
        #region Fields
        protected object _input;
        protected readonly IUseCaseValidator _validator;

        #endregion

        #region Properties
        public FResult Result { get; private set; }
        public Func<dynamic, UseCaseValidationResult, FResult> CaseIsNotValidEvent { get; private set; }
        public Func<dynamic, UseCaseValidationResult, FResult> CaseIsValidEvent { get; private set; }
        public Action<IUseCase<TResponse>, dynamic> OnBeforeEvent { get; private set; }
        public Func<IUseCase<TResponse>, dynamic, FResult> OnActionEvent { get; private set; }
        public Func<IUseCase<TResponse>, FResult> OnFailEvent { get; private set; }
        public Func<IUseCase<TResponse>, FResult> OnSuccessEvent { get; private set; }
        public Func<Exception, FResult> OnExceptionEvent { get; private set; }
        public Action<IUseCase<TResponse>> OnCompletedEvent { get; private set; }
        public bool HasOnBeforeEvent => OnBeforeEvent != null;
        public bool HasActionEvent => OnActionEvent != null;
        public bool HasOnFailEvent => OnFailEvent != null;
        public bool HasOnSuccessEvent => OnSuccessEvent != null;
        public bool HasOnExceptionEvent => OnExceptionEvent != null;
        public bool HasOnCompletedEvent => OnCompletedEvent != null;
        public bool HasCaseIsValidEvent => CaseIsValidEvent != null;
        public bool HasCaseIsNotValidEvent => CaseIsNotValidEvent != null;
        public bool HasValidationEvent => HasCaseIsValidEvent || HasCaseIsNotValidEvent;

        #endregion

        #region Constructors
        public Builder() { }
        public Builder(IUseCaseValidator validator)
        {
            _input = validator.Input;
            _validator = validator;
        }

        #endregion

        #region Methods

        public IBuilder<TResponse> CaseIsNotValid(Func<dynamic, UseCaseValidationResult, FResult> func)
        {
            CaseIsNotValidEvent = func;
            return this;
        }
        public IBuilder<TResponse> CaseIsValid(Func<dynamic, UseCaseValidationResult, FResult> func)
        {
            CaseIsValidEvent = func;
            return this;
        }
        public IBuilder<TResponse> OnBefore(Action<IUseCase<TResponse>, dynamic> act)
        {
            OnBeforeEvent = act;
            return this;
        }
        public IBuilder<TResponse> OnAction(Func<IUseCase<TResponse>, dynamic, FResult> func)
        {
            OnActionEvent = func;
            return this;
        }

        public IBuilder<TResponse> OnFail(Func<IUseCase<TResponse>, FResult> func)
        {
            OnFailEvent = func;
            return this;
        }

        public IBuilder<TResponse> OnSuccess(Func<IUseCase<TResponse>, FResult> func)
        {
            OnSuccessEvent = func;
            return this;
        }
        public IBuilder<TResponse> OnException(Func<Exception, FResult> func)
        {
            OnExceptionEvent = func;
            return this;
        }

        public IBuilder<TResponse> OnCompleted(Action<IUseCase<TResponse>> act)
        {
            OnCompletedEvent = act;
            return this;
        }

        #endregion
    }
}
