namespace Fusc.Library.Core.Interfaces
{
    public interface IObserver<TResponse> where TResponse : IResponseModel
    {
        void Execute(IObserver<TResponse> subject);
    }

}
