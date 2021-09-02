using Fusc.Library.Core.Interfaces;
using Fusc.Library.Exceptions;
using System;

namespace Fusc.Library.Core.Responses
{
    public class ResponseEvaluator<TResponse> where TResponse : IResponseModel
    {
        #region Fields

        private ResponseRules<TResponse> _rules;

        #endregion

        #region Properties
        public Func<IUseCase<TResponse>, IResponseModel> OK { get; }
        public Func<IUseCase<TResponse>, IResponseModel> Created { get; }
        public Func<IUseCase<TResponse>, IResponseModel> NoContent { get; }
        public Func<IUseCase<TResponse>, IResponseModel> BadRequest { get; }
        public Func<IUseCase<TResponse>, IResponseModel> Unauthorized { get; }
        public Func<IUseCase<TResponse>, IResponseModel> Forbidden { get; }
        public Func<IUseCase<TResponse>, IResponseModel> NotFound { get; }
        public Func<IUseCase<TResponse>, IResponseModel> Conflict { get; }
        public Func<IUseCase<TResponse>, IResponseModel> InternalServerError { get; }

        #endregion

        #region Constructors
        public ResponseEvaluator(Func<IUseCase<TResponse>, IResponseModel> ok
            , Func<IUseCase<TResponse>, IResponseModel> created
            , Func<IUseCase<TResponse>, IResponseModel> noContent
            , Func<IUseCase<TResponse>, IResponseModel> badRequest
            , Func<IUseCase<TResponse>, IResponseModel> unauthorized
            , Func<IUseCase<TResponse>, IResponseModel> forbidden
            , Func<IUseCase<TResponse>, IResponseModel> notFound
            , Func<IUseCase<TResponse>, IResponseModel> conflict
            , Func<IUseCase<TResponse>, IResponseModel> internalServerError)
        {
            OK = ok;
            Created = created;
            NoContent = noContent;
            BadRequest = badRequest;
            Unauthorized = unauthorized;
            Forbidden = forbidden;
            NotFound = notFound;
            Conflict = conflict;
            InternalServerError = internalServerError;
        }

        #endregion

        #region Methods
        public IResponseModel Execute(IUseCase<TResponse> useCase, IFResult executionResult)
        {
            _rules ??= new ResponseRules<TResponse>(useCase, executionResult);

            return executionResult.HasErrors ? ErrorsRules(useCase) : GeneralRules(useCase);

        }

        private IResponseModel ErrorsRules(IUseCase<TResponse> useCase)
        {
            if (_rules.NotAuthenticated())
                return GetRuleSpecicOrDefault(Unauthorized, InternalServerError).Invoke(useCase);

            if (_rules.Forbidden())
                return GetRuleSpecicOrDefault(Forbidden, InternalServerError).Invoke(useCase);

            if (_rules.DataValidationError())
                return GetRuleSpecicOrDefault(BadRequest, InternalServerError).Invoke(useCase);

            if (_rules.BusinessError())
                return GetRuleSpecicOrDefault(Conflict, InternalServerError).Invoke(useCase);

            return GetRuleSpecicOrDefault(InternalServerError, null).Invoke(useCase);
        }

        private IResponseModel GeneralRules(IUseCase<TResponse> useCase)
        {
            if (_rules.NoContent())
                return GetRuleSpecicOrDefault(NoContent, BadRequest).Invoke(useCase);

            if (_rules.Created())
                return GetRuleSpecicOrDefault(Created, OK).Invoke(useCase);

            if (_rules.Created())
                return GetRuleSpecicOrDefault(OK, null).Invoke(useCase);

            if (_rules.NotFound())
                return GetRuleSpecicOrDefault(NotFound, BadRequest).Invoke(useCase);

            if (_rules.OK())
                return GetRuleSpecicOrDefault(OK, null).Invoke(useCase);


            return GetRuleSpecicOrDefault(BadRequest, InternalServerError).Invoke(useCase);
        }

        private Func<IUseCase<TResponse>, IResponseModel> GetRuleSpecicOrDefault(Func<IUseCase<TResponse>, IResponseModel> specific, Func<IUseCase<TResponse>, IResponseModel> general)
        {
            if (specific == null && general == null)
                Throw.Development(GetType(), "The specific or generic response were not implemented.");

            return specific ?? general;

        }

        #endregion

    }
}
