using System;

namespace Fusc.Library.Core.Conditions
{
    public class Condition<T>
    {
        private readonly Func<bool> _evaluation;
        private readonly Func<T> _truePredicate;
        private readonly Condition<T> _newCondition;
        private readonly T _result;

        public Condition(T result)
        {
            _result = result;
            _evaluation = null;
        }

        public Condition(Func<bool> evaluation, Func<T> truePredicate, Condition<T> newCondition)
        {
            _evaluation = evaluation;
            _truePredicate = truePredicate;
            _newCondition = newCondition;
        }

        public T Evaluate()
        {
            if (_evaluation == null)
                return _result;

            return _evaluation.Invoke() ? _truePredicate.Invoke() : _newCondition.Evaluate();
        }

    }

}
