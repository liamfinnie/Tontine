﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TontineClient.Plutus.TradeService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CreateTradeResult", Namespace="http://schemas.datacontract.org/2004/07/TontineModel.DataLayer")]
    [System.SerializableAttribute()]
    public partial class CreateTradeResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] ErrorsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Errors {
            get {
                return this.ErrorsField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorsField, value) != true)) {
                    this.ErrorsField = value;
                    this.RaisePropertyChanged("Errors");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InvalidTradeSubmission", Namespace="http://schemas.datacontract.org/2004/07/TontineService.TradeService")]
    [System.SerializableAttribute()]
    public partial class InvalidTradeSubmission : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TradeService.ITradeService")]
    public interface ITradeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITradeService/CreateTrade", ReplyAction="http://tempuri.org/ITradeService/CreateTradeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TontineClient.Plutus.TradeService.InvalidTradeSubmission), Action="http://tempuri.org/ITradeService/CreateTradeInvalidTradeSubmissionFault", Name="InvalidTradeSubmission", Namespace="http://schemas.datacontract.org/2004/07/TontineService.TradeService")]
        TontineClient.Plutus.TradeService.CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITradeService/CreateTrade", ReplyAction="http://tempuri.org/ITradeService/CreateTradeResponse")]
        System.Threading.Tasks.Task<TontineClient.Plutus.TradeService.CreateTradeResult> CreateTradeAsync(string tradeRepresentation, string sourceApplicationCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITradeServiceChannel : TontineClient.Plutus.TradeService.ITradeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TradeServiceClient : System.ServiceModel.ClientBase<TontineClient.Plutus.TradeService.ITradeService>, TontineClient.Plutus.TradeService.ITradeService {
        
        public TradeServiceClient() {
        }
        
        public TradeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TradeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TradeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TradeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TontineClient.Plutus.TradeService.CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode) {
            return base.Channel.CreateTrade(tradeRepresentation, sourceApplicationCode);
        }
        
        public System.Threading.Tasks.Task<TontineClient.Plutus.TradeService.CreateTradeResult> CreateTradeAsync(string tradeRepresentation, string sourceApplicationCode) {
            return base.Channel.CreateTradeAsync(tradeRepresentation, sourceApplicationCode);
        }
    }
}
