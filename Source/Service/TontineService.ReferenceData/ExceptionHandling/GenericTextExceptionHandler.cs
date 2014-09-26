using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace TontineService.ReferenceData.ExceptionHandling
{
    public class GenericTextExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerTextPlainResult(
                "A server error has occurred.", context.Request);
        }
    }
     
    public class InternalServerTextPlainResult : IHttpActionResult
    {
        public string Message { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public InternalServerTextPlainResult(string message, HttpRequestMessage request)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (request == null)
                throw new ArgumentNullException("request");

            Message = message;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        
        public HttpResponseMessage Execute()
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(Message),
                RequestMessage = Request
            };
        }
    }
}