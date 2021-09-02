using Fusc.Library.Core.Interfaces;
using System.Collections.Generic;

namespace Fusc.Library.Cases
{
    public abstract class Story<TResponse> where TResponse: IResponseModel
    {

        #region Fields
        private readonly Dictionary<string, IFResult> _stories = new Dictionary<string, IFResult>();
        private readonly List<IUseCase<TResponse>> _useCases = new List<IUseCase<TResponse>>();
        #endregion
   
        #region Methods
        public Story<TResponse> Add(IUseCase<TResponse> useCase)
        {
            _useCases.Add(useCase);
            return this;
        }

        public void Add(string nameUseCase, IFResult result)
        {
            _stories.Add(nameUseCase, result);
        }

        public void Run()
        {
            foreach (var useCase in _useCases)
            {
                useCase.SetStory(this);
                useCase.Execute();

            }
        }

        #endregion

    }

}
