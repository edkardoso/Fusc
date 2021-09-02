using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Responses;
using System;

namespace Fusc.Library.Core
{
    public class UseCaseResponse<TResponse> 
        : IUseCaseResponse<TResponse>  where TResponse: IResponseModel
    {

        #region Fields
        private readonly IUseCase<TResponse> _useCase;
        private readonly IFResult _executionResult;
        private Func<IUseCase<TResponse>, IResponseModel> _response_200OK;
        private Func<IUseCase<TResponse>, IResponseModel> _response_201Created;
        private Func<IUseCase<TResponse>, IResponseModel> _response_204NoContent;
        private Func<IUseCase<TResponse>, IResponseModel> _response_400BadRequest;
        private Func<IUseCase<TResponse>, IResponseModel> _response_401Unauthorized;
        private Func<IUseCase<TResponse>, IResponseModel> _response_403Forbidden;
        private Func<IUseCase<TResponse>, IResponseModel> _response_404NotFound;
        private Func<IUseCase<TResponse>, IResponseModel> _response_409Conflict;
        private Func<IUseCase<TResponse>, IResponseModel> _response_500InternalErrorServer;
        private ResponseEvaluator<TResponse> _evaluateResponse;
        #endregion

        #region Constructors
        public UseCaseResponse(IUseCase<TResponse> useCase, IFResult executionResult)
        {
            _useCase = useCase;
            _executionResult = executionResult;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Response 200
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IUseCaseResponse<TResponse> WhenOK(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_200OK = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenCreated(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_201Created = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenNoContent(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_204NoContent = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenUnauthorized(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_401Unauthorized = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenForbidden(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_403Forbidden = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenNotFound(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_404NotFound = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenBusinessError(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_409Conflict = func;
            return this;

        }

        public IUseCaseResponse<TResponse> WhenBadRequest(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_400BadRequest = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenInvalidData(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_400BadRequest = func;
            return this;
        }

        public IUseCaseResponse<TResponse> WhenFatalError(Func<IUseCase<TResponse>, IResponseModel> func)
        {
            _response_500InternalErrorServer = func;
            return this;
        }

        private ResponseEvaluator<TResponse> Build()
            => new ResponseEvaluator<TResponse>(
                _response_200OK, _response_201Created, _response_204NoContent, _response_400BadRequest,
                _response_401Unauthorized, _response_403Forbidden, _response_404NotFound, _response_409Conflict,
                _response_500InternalErrorServer);


        public IResponseModel Execute()
        {
            _evaluateResponse ??= Build();

            return _evaluateResponse.Execute(_useCase, _executionResult);

        }

      

        #endregion

    }
}
