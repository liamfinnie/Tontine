using System;
using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using NLog;

namespace TontineService.TradeService
{
    public class TradeServiceErrorHandler : IErrorHandler
    {   
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if(!(error is FaultException))
                fault = Message.CreateMessage(version,
                                              FaultCode.CreateSenderFaultCode("GenericFaultCode", "http://www.tontine.com/tradeService"),
                                              "An error has been encountered trying to process your request. Please try again. If the problem persists contact support@tontine.com. Error Code : " + 12345,
                                              "");
        }

        public bool HandleError(Exception error)
        {
            Logger logger = LogManager.GetLogger("TradeServiceErrorHandler");
            logger.Error("*** TradeServiceErrorHandler.HandleError called.");
            logger.Error("Error Code : 12345.");
            logger.Error(error.Message);
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