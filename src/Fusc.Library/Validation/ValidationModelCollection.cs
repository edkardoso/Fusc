using Fusc.Library.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fusc.Library.Validation
{
    public class ValidationModelCollection : ICollection<IFResultMessage>
    {

        #region Fields

        private readonly List<IFResultMessage> _validationModels;
        #endregion

        #region Properties
        public int Count => _validationModels.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public IFResultMessage this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        #endregion

        #region Constructors
        public ValidationModelCollection() : this(new List<IFResultMessage>()) { }
        public ValidationModelCollection(List<IFResultMessage> validationModels)
        {
            _validationModels = validationModels ?? new List<IFResultMessage>();

        }
        #endregion

        #region Methods

        public IReadOnlyCollection<IFResultMessage> Filter(Func<IFResultMessage, bool> predicate)
        {
            return _validationModels.Where(predicate).ToList().AsReadOnly();
        }

        public List<IMessageModel> ConvertToMessages() =>
           _validationModels.Select(v => v.ConvertToMessage()).ToList();

        public IReadOnlyCollection<IFResultMessage> GetErrors()
         => Filter((error) => error != null
                 && (error.Severity == Severity.FatalError
                 || error.Severity == Severity.Unauthorized
                 || error.Severity == Severity.InvalidData
                 || error.Severity == Severity.BusinessError
                 || error.Severity == Severity.Forbidden
         ));

        public IReadOnlyCollection<IFResultMessage> GetNotErrors()
          => Filter((error) => error != null
                 && error.Severity != Severity.FatalError
                 && error.Severity != Severity.Unauthorized
                 && error.Severity != Severity.InvalidData
                 && error.Severity != Severity.BusinessError
                 && error.Severity != Severity.Forbidden
         );

        public void Add(IFResultMessage item)
        {
            _validationModels.Add(item);
        }

        public void AddRange(IReadOnlyCollection<IFResultMessage> items) => _validationModels.AddRange(items);

        public void Clear() => _validationModels.Clear();

        public bool Contains(IFResultMessage item) => _validationModels.Contains(item);

        public void CopyTo(IFResultMessage[] array, int arrayIndex) => _validationModels.CopyTo(array, arrayIndex);

        public bool Remove(IFResultMessage item) => _validationModels.Remove(item);

        public IEnumerator<IFResultMessage> GetEnumerator() => _validationModels.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _validationModels.GetEnumerator();
        #endregion


    }
}
