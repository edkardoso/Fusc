using Fusc.Library.Config;
using Fusc.Library.Core.Conditions;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Exceptions;
using Fusc.Library.Validation;
using System;

namespace Fusc.Library.Core
{
    public class EventManager<TResponse> where TResponse : IResponseModel
    {
        #region Fields
        private readonly Builder<TResponse> _builder;
        private readonly IUseCase<TResponse> _useCase;
        private readonly IUseCaseValidator _validator;
        private readonly dynamic _input;

        #endregion

        #region Constructors
        public EventManager(IUseCase<TResponse> useCase, IBuilder<TResponse> builder, IUseCaseValidator validator)
        {
            _useCase = useCase;
            _validator = validator;
            _input = _validator.Input;
            _builder = builder as Builder<TResponse>;
        }


        #endregion

        #region Methods
        public void InvokeOnBeforeIfAny()
        {
            if (_builder.HasOnBeforeEvent)
                _builder.OnBeforeEvent.Invoke(_useCase, _input);
        }

        public IFResult InvokeOnValidationIfAny(IUseCaseValidator validator, UseCaseValidationResult validationResult )
        {
            var caseNoEvents = new CaseNoEvents(validator, validationResult);
            var caseSuccess = new CaseSuccessAndHasEventSuccess<TResponse>(validationResult, _builder, _input, caseNoEvents);
            var caseError = new CaseErrorAndHasEventError<TResponse>(validationResult, _builder, _input, caseSuccess);

            return caseError.Evaluate();
        }

        public IFResult InvokeOnAction(IFResult result)
        {
            if (result.Stop)
                return result;

            if (!_builder.HasActionEvent)
                throw new FuscDevelopmentException("EventManager", _useCase.Name, "It is necessary to define an OnAction event.");

            return _builder.OnActionEvent(_useCase, _input) ?? (FuscConfig.StopWhenReturnIsNull ? FResultFactory.NullStop() : FResultFactory.NullContinue());


        }

        public bool HasSuccessOrFailureEvent(IFResult result)
        {
            var success = result.Success && _builder.HasOnSuccessEvent;
            var fail = result.Failure && _builder.HasOnFailEvent;

            return success || fail;
        }

        public IFResult InvokeOnSuccess() => _builder.OnSuccessEvent(_useCase);

        public IFResult InvokeOnFailure() => _builder.OnFailEvent(_useCase);

        public IFResult InvokeOnException(Exception exception)
        {
            if (_builder.HasOnExceptionEvent)
                return _builder.OnExceptionEvent(exception);

            return null;

        }

        public void InvokeOnCompleted()
        {
            if (_builder.HasOnCompletedEvent)
                _builder.OnCompletedEvent(_useCase);
        }

        public void InvokeNextUseCaseIfAny(IFResult result)
        {
            if (_useCase != null && _useCase.ContainsNext)
            {
                var _next = _useCase.Next;
                _next.SetResult(result);
                _next.Execute();
            }
        }

        #endregion
    }

}
