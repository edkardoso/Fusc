using Fusc.Library.Core.Interfaces;
using Fusc.Library.Validation;

namespace Fusc.Library.Core.Conditions
{
    public class CaseSuccessAndHasEventSuccess<TResponse> : Condition<IFResult>
        where TResponse : IResponseModel
    {
        public CaseSuccessAndHasEventSuccess(UseCaseValidationResult result, Builder<TResponse> builder, dynamic input, Condition<IFResult> condition)
           : base(() => result.IsValid && builder.HasCaseIsValidEvent
           , () => builder.CaseIsValidEvent(input, result)
           , condition)
        {
        }
    }

}
