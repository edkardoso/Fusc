using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;

namespace Fusc.Library.Core.Conditions
{
    public class CaseNoEvents : Condition<IFResult>
    {
        private IUseCaseValidator _validator;

        public CaseNoEvents() : base(FResultFactory.NullStop())
        {
        }

        public CaseNoEvents(IUseCaseValidator validator, IUseCaseValidationResult validationResult) 
            :base(FResultFactory.NullValidator(validator, validationResult))
        {
        }
    }

}
