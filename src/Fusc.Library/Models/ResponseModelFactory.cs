using Fusc.Library.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Fusc.Library.Models
{
    public class ResponseModelFactory
    {
        public static ResponseModel<T> CreateInstance<T>(HttpStatusCode statusCode
            , string description
            , bool success
            , T data
            , IList<IMessageModel> messages)
        => new ResponseModel<T>(statusCode, description, success, data, messages);


        public static ResponseModel CreateInstance(HttpStatusCode statusCode
           , string description
           , bool success
           , IList<IMessageModel> messages)
       => new ResponseModel(statusCode, description, success, messages);



        public static ResponseModel<T> OK<T>(string description = "Success",
                                           T data = default,
                                           IList<IMessageModel> messages = default)
          => CreateInstance(HttpStatusCode.OK, description, true, data, messages);

        public static ResponseModel OK(string description = "Success",
                                         IList<IMessageModel> messages = default)
        => CreateInstance(HttpStatusCode.OK, description, true, messages);

        public static ResponseModel<T> Created<T>(string description = "Created",
                                                 T data = default,
                                                 IList<IMessageModel> messages = default)
           => CreateInstance(HttpStatusCode.Created, description, true, data, messages);

        public static ResponseModel NoContent(IList<IMessageModel> messages = default)
        => CreateInstance(HttpStatusCode.NoContent, HttpStatusCode.NoContent.ToString(), false, messages);

        public static ResponseModel<T> BadRequest<T>(T data, [NotNull] IList<IMessageModel> messages)
            => CreateInstance(HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), false, data, messages);

        public static ResponseModel BadRequest([NotNull] IList<IMessageModel> messages)
        => CreateInstance(HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), false,  messages);


        public static ResponseModel Unauthorized(IList<IMessageModel> messages = default)
            => CreateInstance(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), false, messages);

        public static ResponseModel Forbidden(IList<IMessageModel> messages = default)
           => CreateInstance(HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.ToString(), false, messages);

        public static ResponseModel NotFound(IList<IMessageModel> messages = default)
          => CreateInstance(HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), false, messages);

        public static ResponseModel Conflicts(IList<IMessageModel> messages = default)
        => CreateInstance(HttpStatusCode.Conflict, HttpStatusCode.Conflict.ToString(), false, messages);

        public static ResponseModel InternalServerError([NotNull] IList<IMessageModel> messages)
            => CreateInstance(HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), false, messages);
    }
}
