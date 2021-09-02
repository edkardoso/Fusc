using Fusc.Library.Core.Interfaces;
using Fusc.Library.Validation;
using System.Collections.Generic;

namespace Fusc.Library.Core.Result
{
    public class FResultNull : IFResult
    {
        private readonly bool _success;
        private readonly bool _validatorIsNull;


        public FResultNull() : this(true, true)
        {

        }

        public FResultNull(bool validatorIsNull = true, bool success = false)
        {
            _success = success;
            _validatorIsNull = validatorIsNull;
        }


        public object Data => null;

        public bool DataIsNotNull => !DataIsNull;

        public bool DataIsNull => true;

        public IReadOnlyCollection<IFResultMessage> Errors => new List<FResultMessage>();

        public bool Failure => !Success;

        public bool HasErrors => false;

        public IReadOnlyCollection<IFResultMessage> Messages => new List<FResultMessage>();

        public bool NotHasError => !HasErrors;

        public bool Stop => !_success;

        public bool Success => _success;

        public bool ValidationRun => !_validatorIsNull;

        public bool IsObjectNull => true;

        public void AddBusinessError(string error)
        {
        }

        public void AddData(dynamic data)
        {
        }

        public void AddInfo(string info)
        {
        }

        public void AddValidationModels(IReadOnlyCollection<IFResultMessage> errors)
        {
        }

        public void AddWarning(string warning)
        {
        }
    }
}
