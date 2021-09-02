using Fusc.Library.Core.Interfaces;
using Fusc.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Fusc.Library.Helper
{
    public static class ReadOnlyCollectionExtensions
    {
        public static List<IMessageModel> ConvertToMessages(this IReadOnlyCollection<IFResultMessage> source)
            => source.Select(s => new MessageModel(s.Message, s.Severity.ToString().ToUpper(), s.ErrorCode)).ToList<IMessageModel>();

    }
}
