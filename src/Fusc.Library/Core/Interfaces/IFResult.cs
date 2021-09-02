using System.Collections.Generic;

namespace Fusc.Library.Core.Interfaces
{
    public interface IFResult : IObjecNull
    {
        object Data { get; }
        bool DataIsNotNull { get; }
        bool DataIsNull { get; }
        IReadOnlyCollection<IFResultMessage> Errors { get; }
        bool Failure { get; }
        bool HasErrors { get; }
        IReadOnlyCollection<IFResultMessage> Messages { get; }
        bool NotHasError { get; }
        bool Stop { get; }
        bool Success { get; }
        bool ValidationRun { get; }

        void AddData(dynamic data);
        void AddBusinessError(string error);
        void AddValidationModels(IReadOnlyCollection<IFResultMessage> errors);
        void AddInfo(string info);
        void AddWarning(string warning);
    }
}