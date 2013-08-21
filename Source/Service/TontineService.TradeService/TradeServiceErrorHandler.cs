using System;
using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TontineService.TradeService
{
    public class TradeServiceErrorHandler : IErrorHandler
    {
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            fault = Message.CreateMessage(version,
                                          FaultCode.CreateSenderFaultCode("GenericFaultCode", "http://www.tontine.com/tradeService"),
                                          "An error has been encountered trying to process your request. Please try again. If the problem persists contact support@tontine.com.",
                                          "Error Code : " + 12345,
                                          ""
                );
        }

        public bool HandleError(Exception error)
        {
            File.WriteAllText(@"c:\temp\error.txt", "*** TradeServiceErrorHandler.HandleError called."  + Environment.NewLine);
            File.AppendAllText(@"c:\temp\error.txt", "Error Code : " + 12345 + Environment.NewLine);
            File.AppendAllText(@"c:\temp\error.txt", error.Message);
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