using System;
using System.Text;
using System.Web.Http.Filters;
using NLog;

namespace TontineService.CountryReferenceData.Filters
{
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