using Fusc.Library.Cases;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Exceptions;
using Fusc.Library.Helper;
using Fusc.Library.Models;
using System.Collections.Generic;

namespace Fusc.Library.Core
{
    public abstract class UseCase<TResponse>
            : IUseCase<TResponse> where TResponse : IResponseModel
    {
        #region Fields
        private IUseCase<TResponse> _nextUseCase;
        private readonly string _name;
        private readonly bool _mustReturnData;
        private dynamic _response;
        #endregion

        #region Properties
        public IFResult Result { get; protected set; }
        public string Name => _name;
        public bool ContainsNext => _nextUseCase != null;
        public IUseCase<TResponse> Next => _nextUseCase;
        public bool MustReturnData => _mustReturnData;
        public TResponse Response => (TResponse)BuildResponse(new UseCaseResponse<TResponse>(this, Result)).Execute();


        #endregion

        #region Constructors
        public UseCase(string name = default, bool mustReturnData = true)
        {
            _mustReturnData = mustReturnData;
            _name = name;

        }
        #endregion

        #region Methods
        public void Execute()
        {
            IBuilder<TResponse> builder = new Builder<TResponse>();
            var flow = new Flow<TResponse>(this, Build(builder), new UseCaseValidatorNull<object>());
            Result = flow.Execute();

        }

        public virtual void Undo()
        {
        }

        public IUseCaseValidator GetValidator() => new UseCaseValidatorNull<object>();

        public void SetNext(IUseCase<TResponse> useCase) => _nextUseCase = useCase;

        public abstract IBuilder<TResponse> Build(IBuilder<TResponse> builder);

        public virtual IUseCaseResponse<TResponse> BuildResponse(IUseCaseResponse<TResponse> builder)
        {
            return builder
                .WhenBadRequest((usecase) => ResponseModelFactory.BadRequest(usecase.Result.Errors.ConvertToMessages()))
                .WhenBusinessError((usecase) => ResponseModelFactory.Conflicts(usecase.Result.Errors.ConvertToMessages()))
                .WhenCreated((usecase) => ResponseModelFactory.Created((dynamic)usecase.Result.Data, usecase.Result.Errors.ConvertToMessages()))
                .WhenFatalError((usecase) => ResponseModelFactory.InternalServerError(usecase.Result.Errors.ConvertToMessages()))
                .WhenForbidden((usecase) => ResponseModelFactory.Forbidden(usecase.Result.Errors.ConvertToMessages()))
                .WhenInvalidData((usecase) => ResponseModelFactory.BadRequest(usecase.GetValidator().Input, usecase.Result.Errors.ConvertToMessages()))
                .WhenNoContent((usecase) => ResponseModelFactory.NoContent(usecase.Result.Errors.ConvertToMessages()))
                .WhenNotFound((usecase) => ResponseModelFactory.NotFound(usecase.Result.Errors.ConvertToMessages()))
                .WhenOK((usecase) => ResponseModelFactory.OK(data: usecase.Result.Data, messages: usecase.Result.Errors.ConvertToMessages()))
                .WhenUnauthorized((usecase) => ResponseModelFactory.Unauthorized(usecase.Result.Errors.ConvertToMessages()));
        }

        public void SetResult(IFResult result)
        {
            Result = result;
        }

        public void SetStory(Story<TResponse> story)
        {
            story.Add(Name, Result);
        }


        public void SetResponse(TResponse response) => _response = response;

        #endregion

    }

    public abstract class UseCase<TRequest, TResponse>
        : IObserver<TResponse>
        , ISubject<TResponse>
        , IUseCase<TRequest, TResponse> where TResponse : IResponseModel
    {
        #region Fields

        protected IUseCaseValidator<TRequest> _validator;
        private readonly string _name;
        private readonly bool _mustReturnData;
        private IUseCase<TResponse> _nextUseCase;
        private TResponse _response;
        private List<IObserver<TResponse>> _observers;


        #endregion

        #region Properties
        public IFResult Result { get; protected set; }

        public string Name => _name;

        public bool ContainsNext => _nextUseCase != null;

        public IUseCase<TResponse> Next => _nextUseCase;

        public bool MustReturnData => _mustReturnData;

        public TResponse Response => _response;


        #endregion

        #region Constructors

        protected UseCase(IUseCaseValidator<TRequest> validator = default, string name = default, bool mustReturnData = true)
        {
            _validator = validator ?? new UseCaseValidatorNull<TRequest>();
            _name = name ?? GetType().Name;
            _mustReturnData = mustReturnData;
        }

        #endregion

        #region Methods

        public abstract IBuilder<TResponse> Build(IBuilder<TResponse> builder);
        public virtual IUseCaseResponse<TResponse> BuildResponse(IUseCaseResponse<TResponse> builder)
        {
            // TODO: aplicar herança

            return builder
               .WhenBadRequest((usecase) => ResponseModelFactory.BadRequest(usecase.Result.Errors.ConvertToMessages()))
               .WhenBusinessError((usecase) => ResponseModelFactory.Conflicts(usecase.Result.Errors.ConvertToMessages()))
               .WhenCreated((usecase) => ResponseModelFactory.Created((dynamic)usecase.Result.Data, usecase.Result.Messages.ConvertToMessages()))
               .WhenFatalError((usecase) => ResponseModelFactory.InternalServerError(usecase.Result.Errors.ConvertToMessages()))
               .WhenForbidden((usecase) => ResponseModelFactory.Forbidden(usecase.Result.Errors.ConvertToMessages()))
               .WhenInvalidData((usecase) => ResponseModelFactory.BadRequest(usecase.GetValidator().Input, usecase.Result.Errors.ConvertToMessages()))
               .WhenNoContent((usecase) => ResponseModelFactory.NoContent(usecase.Result.Messages.ConvertToMessages()))
               .WhenNotFound((usecase) => ResponseModelFactory.NotFound(usecase.Result.Errors.ConvertToMessages()))
               .WhenOK((usecase) => ResponseModelFactory.OK(data: usecase.Result.Data, messages: usecase.Result.Messages.ConvertToMessages()))
               .WhenUnauthorized((usecase) => ResponseModelFactory.Unauthorized(usecase.Result.Errors.ConvertToMessages()));

        }

        public void Execute(dynamic input)
        {
            if (_validator.IsObjectNull)
            {
                _validator.SetData(input);
            }

            Execute();
        }

        public void Execute()
        {
            var builder = Build(new Builder<TResponse>(_validator)) as Builder<TResponse>;
            var flow = new Flow<TResponse>(this, Build(builder), _validator);
            Result = flow.Execute();

            var builderResponse = BuildResponse(new UseCaseResponse<TResponse>(this, Result));

            SetResponse((TResponse)builderResponse.Execute());

        }

        public void Undo() { }
        public IUseCaseValidator GetValidator() => _validator;

        public void SetNext(IUseCase<TResponse> useCase) => _nextUseCase = useCase;

        public void SetResult(FResult result)
        {
            if (Result != null)
                Throw.Development(this.GetType(), "Improper attempt to change data");

            Result = result;
        }

        public void SetStory(Story<TResponse> story)
        {
            throw new System.NotImplementedException();
        }


        public void SetResponse(TResponse response) => _response = response;

        public void SetResult(IFResult result)
        {
            Result = result;
        }


        public void Attach(IObserver<TResponse> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver<TResponse> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Execute(this);
            }
        }

        public void Execute(IObserver<TResponse> subject)
        {
            subject.Execute(this);
        }


        #endregion

    }

}
