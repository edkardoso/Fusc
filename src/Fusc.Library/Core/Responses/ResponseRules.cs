using Fusc.Library.Core.Interfaces;
using System.Linq;

namespace Fusc.Library.Core.Responses
{
    public class ResponseRules<TResponse> where TResponse : IResponseModel
    {
        #region Fields
        public readonly IUseCase<TResponse> _useCase;
        public readonly IFResult _executionResult;
        #endregion

        #region Constructors
        public ResponseRules(IUseCase<TResponse> useCase, IFResult executionResult)
        {
            _useCase = useCase;
            _executionResult = executionResult;
        }

        #endregion

        #region Methods
        public bool OK() => _executionResult.Success;
        public bool Created() => _executionResult.Success && _executionResult.DataIsNotNull && _executionResult.Messages.Any(error => error.Severity.HasFlag(Validation.Severity.Created));
        public bool NoContent() => _executionResult.Success && _executionResult.DataIsNull || _executionResult.Messages.Any(error => error.Severity.HasFlag(Validation.Severity.NoContent));
        public bool DataValidationError() => _executionResult.Errors.Any(error => error.Severity.HasFlag(Validation.Severity.InvalidData));
        public bool BusinessError() => _executionResult.Errors.Any(error => error.Severity.HasFlag(Validation.Severity.BusinessError));
        public bool NotAuthenticated() => _executionResult.Errors.Any(error => error.Severity.HasFlag(Validation.Severity.Unauthorized));
        public bool Forbidden() => _executionResult.Errors.Any(error => error.Severity.HasFlag(Validation.Severity.Forbidden));
        public bool NotFound() => _executionResult.Success && _useCase.MustReturnData && _executionResult.DataIsNull;

        #endregion
    }
}
