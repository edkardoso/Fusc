using Fusc.Library.Core.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Fusc.Library.Models
{
    [DataContract]
    public sealed class PageModel : IPagination
    {
        public PageModel(int page, int take, int defaultPageSize)
        {
            Page = page < 0 ? 0 : page;

            Take = take <= 0 ? defaultPageSize : Math.Max(1, Math.Min(take, defaultPageSize));
           
            Skip =  Page * Take;

        }

        public PageModel(IPagination pageRequest, int totalPage, int totalRecords)
        {
            Page = pageRequest.Page;
            Skip = pageRequest.Skip;
            Take = pageRequest.Take;
            TotalPage = totalPage;
            TotalRecords = totalRecords;
        }

        [DataMember(Name = "page", Order = 0)]
        public int Page { get;  }

        [DataMember(Name = "skip", Order = 1)]
        public int Skip { get; }

        [DataMember(Name = "take", Order = 2)]
        public int Take { get; }

        [DataMember(Name = "totalRecords", Order = 3)]
        public int TotalRecords { get; }

        [DataMember(Name = "totalPage", Order = 4)]
        public int TotalPage { get; }
    }
}
