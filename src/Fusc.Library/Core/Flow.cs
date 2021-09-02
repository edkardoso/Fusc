using Fusc.Library.Config;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Exceptions;

namespace Fusc.Library.Core
{
    public class Flow<TResponse> where TResponse : IResponseModel
    {
        #region Fields
        private readonly IUseCaseValidator _validator;
        private readonly IUseCase<TResponse> _useCase;
        private readonly Builder<TResponse> _builder;
        //private readonly dynamic _input;
        private readonly EventManager<TResponse> _events;
        private IFResult _result;
        #endregion

        #region Constructors
        public Flow(IUseCase<TResponse> useCase, IBuilder<TResponse> builder, IUseCaseValidator validator)
        {
            _useCase = useCase;
            _builder = builder as Builder<TResponse>;
            _validator = validator;
            _events = new EventManager<TResponse>(_useCase, _builder, _validator);
        }

        #endregion

        #region Methods
        public IFResult Execute()
        {
            try
            {
                _events.InvokeOnBeforeIfAny();

                var resultValidation = _validator.Validate();

                _result = _events.InvokeOnValidationIfAny(_validator, resultValidation);

                _useCase.SetResult(_result);

                FuscDevelopmentException.PostWhenRequired(!_validator.IsObjectNull, _result.IsObjectNull, FuscConfig.ErrorHandlingEventRequired, _useCase.Name);

                if (_result.IsObjectNull && !resultValidation.IsValid)
                    return new FResult(resultValidation);

                _result = _events.InvokeOnAction(_result);

                if (_result.IsObjectNull && FuscConfig.ForceExceptionWhenReturnIsNull)
                {
                    throw new FuscDevelopmentException("Flow", _useCase.Name, "The Action event returned a null value.");
                }

                _useCase.SetResult(_result);

                if (_result.Stop || !_events.HasSuccessOrFailureEvent(_result))
                {
                    return _result;
                }

                _result = _result.Success ? _events.InvokeOnSuccess() : _events.InvokeOnFailure();

                return _result;

            }
            catch (System.Exception ex)
            {
                // TODO: Verificar o ambiente e só enviar o stack trace quando estiver em DEV

                // TODO: Registrar log, somente quando estiver em ambiente de Produção

                return _events.InvokeOnException(ex) ?? FResultFactory.Exception(_useCase.GetType().Name, ex);
            }
            finally
            {
                _events.InvokeOnCompleted();

                _events.InvokeNextUseCaseIfAny(_result);
            }
        }

        #endregion

    }
}
