namespace Fusc.Library.Core.Interfaces
{
    public interface IPagination
    {
        public int Page { get;  }
        public int Skip { get;  }
        public int Take { get;  }
        public int TotalRecords { get;  }
        public int TotalPage { get;  }
    }



}
