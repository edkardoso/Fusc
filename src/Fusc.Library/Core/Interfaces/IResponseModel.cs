using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Fusc.Library.Core.Interfaces
{

    public interface IResponseModel : IActionResult
    {
        bool Success { get; }
        public HttpStatusCode StatusCode { get; }
        string Description { get; }
        IList<IMessageModel> Messages { get; }
        IReadOnlyCollection<ILinkModel> Links { get; }
        IPagination Page { get; }
        IResponseModel AddPage(IPagination request, int totalPage, int totalRecords);
        IResponseModel AddLink(string href, string rel, string code, string action );
        IResponseModel AddNavigationLink(int skip
            , int take
            , int records
            , string uri
            , string ActionFirstPage
            , string ActionNextPage
            , string ActionPreviousPage
            , string ActionLastPage);
    }

    public interface IResponseModel<T> : IResponseModel
    {
        T Data { get; }
    }



}
