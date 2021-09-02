using Fusc.Library.Core.Interfaces;
using Fusc.Library.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Fusc.Library.Models
{

    [DataContract]
    public class ResponseModel : IResponseModel
    {
        #region Fields
        private readonly List<LinksModel> _links = new List<LinksModel>();
        #endregion

        #region Properties

        [DataMember(Name = "description", Order = 2)]
        public string Description { get; }
        [DataMember(Name = "success", Order = 3)]
        public bool Success { get; }

        [DataMember(Name = "messages", EmitDefaultValue = false, Order = 5)]
        public IList<IMessageModel> Messages { get; }

        [DataMember(Name = "links", EmitDefaultValue = false, Order = 7)]
        public IReadOnlyCollection<ILinkModel> Links => _links.AsReadOnly();

        [DataMember(Name = "page", EmitDefaultValue = false, Order = 6)]
        public IPagination Page { get; private set; }

        [DataMember(Name = "statusCode", EmitDefaultValue = false, Order = 1)]
        public HttpStatusCode StatusCode { get; private set; }

        #endregion

        #region Constructors

        public ResponseModel(HttpStatusCode statusCode,
                                    string description,
                                    bool success,
                                    IList<IMessageModel> messages)
        {
            Description = description;
            Success = success;
            Messages = messages;
            StatusCode = statusCode;
        }
        #endregion

        #region Methods

        public IResponseModel AddPage(IPagination request, int totalPage, int totalRecords)
        {
            Page = new PageModel(request, totalPage, totalRecords);
            return this;
        }

        public IResponseModel AddLink(string href
            , string rel
            , string code
            , string action)
        {
            _links.Add(new LinksModel(href, rel, code, action));

            return this;
        }


        public IResponseModel AddNavigationLink(
            int page
            , int take
            , int records
            , string uri
            , string ActionFirstPage
            , string ActionNextPage
            , string ActionPreviousPage
            , string ActionLastPage)
        {
            AddLink(BuildUri(0, take, uri), ActionFirstPage, Constants.PERMISSION_FIRST, Constants.ACTION_GET);
            AddLink(BuildUri(page + 1, take, uri), ActionNextPage, Constants.PERMISSION_NEXT, Constants.ACTION_GET);
            AddLink(BuildUri(page - 1, take, uri), ActionPreviousPage, Constants.PERMISSION_PREVIOUS, Constants.ACTION_GET);
            AddLink(BuildUri(records / take, take, uri), ActionLastPage, Constants.PERMISSION_LAST, Constants.ACTION_GET);

            return this;
        }

        private string BuildUri(int page, int take, string uri)
            => $"{uri}?page={ Math.Max(0, page)}&take={take}";

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode

            };

            await objectResult.ExecuteResultAsync(context);
        }

        #endregion
    }

    [DataContract]
    public class ResponseModel<T> : ResponseModel, IResponseModel<T>
    {
        #region Properties

        [DataMember(Name = "data", EmitDefaultValue = false, Order = 4)]
        public T Data { get; }

        #endregion

        #region Constructors

        public ResponseModel(HttpStatusCode statusCode,
                                string description,
                                bool success,
                                T data,
                                IList<IMessageModel> messages)
            : base(statusCode, description, success, messages)
        {
            Data = data;
        }

        #endregion

    }
}
