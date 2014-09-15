﻿using System;
using System.Text;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace TontineService.CountryReferenceData.ExceptionHandling
{
    public class GenericExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Logger logger = LogManager.GetLogger("CountryRefDataService");

            Exception currentException = context.Exception;
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