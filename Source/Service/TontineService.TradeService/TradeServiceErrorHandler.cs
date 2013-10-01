using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using NLog;

namespace TontineService.TradeService
{
    public class TradeServiceErrorHandler : IErrorHandler
    {   
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if(!(error is FaultException))
                fault = Message.CreateMessage(version
                                              , FaultCode.CreateSenderFaultCode("GenericFaultCode", "http://www.tontine.com/tradeService")
                                              , "An error has been encountered trying to process your request. Please try again. If the problem persists contact support@tontine.com."
                                              , "http://www.tontine.com/GenericFaultCode");
        }

        public bool HandleError(Exception error)
        {
            Logger logger = LogManager.GetLogger("TradeService");

            if (OperationContext.Current != null)
            {
                Exception currentException = error;
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

                logger.Error(string.Format("{0}|{1}", (OperationContext.Current.Channel).LocalAddress, consolidatedErrors));

            }

            return true;
        }
    }

    public class TradeServiceErrorHandlerAttribute : Attribute, IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach(ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
                cd.ErrorHandlers.Add(new TradeServiceErrorHandler());
        }
    }
}