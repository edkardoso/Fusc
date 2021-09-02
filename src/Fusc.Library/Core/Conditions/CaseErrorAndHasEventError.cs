using Fusc.Library.Core.Interfaces;
using Fusc.Library.Validation;

namespace Fusc.Library.Core.Conditions
{
    public class CaseErrorAndHasEventError<TResponse> : Condition<IFResult>
       where TResponse : IResponseModel
    {
        public CaseErrorAndHasEventError(UseCaseValidationResult result, Builder<TResponse> builder, dynamic input, Condition<IFResult> condition)
           : base(() => result.IsNotValid && builder.HasCaseIsNotValidEvent
           , () => builder.CaseIsNotValidEvent(input, result)
           , condition)
        {
        }
    }

}
