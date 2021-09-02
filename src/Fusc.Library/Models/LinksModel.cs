using Fusc.Library.Core.Interfaces;

namespace Fusc.Library.Models
{
    public sealed class LinksModel : ILinkModel
    {
        #region Properties
        public string Href { get; }
        public string Rel { get; }
        public string Code { get; }
        public string Action { get; }


        #endregion

        #region Constructors
        public LinksModel(string href, string rel, string code, string action)
        {
            Href = href;
            Rel = rel;
            Code = code;
            Action = action;
        }

        #endregion

    }
}
