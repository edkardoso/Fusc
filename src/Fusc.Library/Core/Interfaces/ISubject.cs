namespace Fusc.Library.Core.Interfaces
{
    public interface ISubject<TResponse> where TResponse : IResponseModel
    {
        void Attach(IObserver<TResponse> observer);

        void Detach(IObserver<TResponse> observer);

        void Notify();
    }

}
