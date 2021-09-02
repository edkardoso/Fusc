using System;

namespace Fusc.Library.Core.Interfaces
{
    public interface IUseCaseResponse<TResponse> where TResponse: IResponseModel
    {
        public IUseCaseResponse<TResponse> WhenOK(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenCreated(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenNoContent(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenBadRequest(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenInvalidData(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenUnauthorized(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenForbidden(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenNotFound(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenBusinessError(Func<IUseCase<TResponse>, IResponseModel> func);
        public IUseCaseResponse<TResponse> WhenFatalError(Func<IUseCase<TResponse>, IResponseModel> func);
        public IResponseModel Execute();


    }
}
