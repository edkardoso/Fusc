using Fusc.Library.Cases;

namespace Fusc.Library.Core.Interfaces
{
    public interface IUseCase<TResponse> where TResponse : IResponseModel
    {
        string Name { get; }
        public bool MustReturnData { get; }
        IFResult Result { get; }
        TResponse Response { get; }
        IBuilder<TResponse> Build(IBuilder<TResponse> builder);
        IUseCaseResponse<TResponse> BuildResponse(IUseCaseResponse<TResponse> builder);
        void Execute();
        void Undo();
        void SetNext(IUseCase<TResponse> useCase);
        bool ContainsNext { get; }
        IUseCase<TResponse> Next { get; }
        IUseCaseValidator GetValidator();
        void SetResult(IFResult result);
        void SetStory(Story<TResponse> story);
        void SetResponse(TResponse response);

    }

    public interface IUseCase<TRequest, TResponse> : IUseCase<TResponse> where TResponse : IResponseModel
    {

    }


}
