using Fusc.Library.Core.Interfaces;
using Fusc.Library.Exceptions;
using Fusc.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusc.Library.Core.Result
{

    public class FResult : IFResult
    {

        #region Fields

        private object _data;
        private readonly ValidationModelCollection _validation = new ValidationModelCollection();
        private readonly bool _success;
        private readonly bool _stop;
        private readonly bool _validationRun;

        #endregion

        #region Properties
        public bool Success => _success && NotHasError;
        public bool Failure => !Success;
        public object Data => _data;
        public bool Stop => _stop;
        public bool DataIsNull => _data == null;
        public bool DataIsNotNull => !DataIsNull;
        public bool HasErrors => Errors.Any();
        public bool NotHasError => !HasErrors;
        public bool ValidationRun => _validationRun;
        public IReadOnlyCollection<IFResultMessage> Errors => _validation.GetErrors();
        public IReadOnlyCollection<IFResultMessage> Messages => _validation.GetNotErrors();

        public bool IsObjectNull => false;

        #endregion

        #region Constructors

        public FResult(Func<dynamic, bool> funcSuccess, object data, IFResultMessage validation = default, bool stop = false)
        {
            _success = funcSuccess.Invoke(data);
            _data = data;
            _stop = stop;
            _validation.Add(validation);
        }
        public FResult(IUseCaseValidationResult validationResult)
        {
            _validation.AddRange(validationResult.Messages);
            _validationRun = true;
            _success = validationResult.IsValid;
        }

        #endregion

        #region Methods

        public void AddData(dynamic data)
        {
            if (_data != null)
                throw new FuscExceptions("An attempt was made to replace an existing datum.", Severity.FatalError, "EX0001");

            _data = data;
        }

        public void AddValidationModels(IReadOnlyCollection<IFResultMessage> models) => _validation.AddRange(models);
        public void AddBusinessError(string error) => _validation.Add(FResultMessageFactory.BusinessError(error));
        public void AddFatalError(string error) => _validation.Add(FResultMessageFactory.FatalError(error));
        public void AddWarning(string warning) => _validation.Add(FResultMessageFactory.Warning(warning));
        public void AddInfo(string info) => _validation.Add(FResultMessageFactory.Success(info));
        public void AddSuccess(string success) => _validation.Add(FResultMessageFactory.Success(success));

        #endregion

    }
}
