using System;
using System.Text;
using System.Web.Http.Filters;
using NLog;

namespace TontineService.ReferenceData.Filters
{
    /// <summary>
    /// Usage of this filter has been removed. A global error handling framework was introduced in WebApi 2.1
    /// http://www.asp.net/web-api/overview/web-api-routing-and-actions/web-api-global-error-handling
    /// </summary>
    [Obsolete]
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger logger = LogManager.GetLogger("CountryRefDataService");

            Exception currentException = actionExecutedContext.Exception;
            var consolidatedErrors = new StringBuilder();

            while (currentException != null)
            {
                consolidatedErrors.Append(currentException.Message);
                consolidatedErrors.Append(string.Format(" ({0})", currentException.GetType()));
                consolidatedErrors.Append(currentException.StackTrace);

                if (currentException.InnerException != null)
                {
                    consolidatedErrors.Append(" --> ");
                    currentException = currentException.InnerException;
                }
                else
                    break;
            }

            logger.Error(consolidatedErrors);
        }
    }
}