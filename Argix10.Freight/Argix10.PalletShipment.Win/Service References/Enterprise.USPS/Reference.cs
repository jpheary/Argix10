﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Argix.Enterprise.USPS {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class EnterpriseFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Argix.Enterprise", ConfigurationName="Enterprise.USPS.IUSPSService")]
    public interface IUSPSService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/TrackRequest", ReplyAction="http://Argix.Enterprise/IUSPSService/TrackRequestResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet TrackRequest(string itemNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/TrackFieldRequest", ReplyAction="http://Argix.Enterprise/IUSPSService/TrackFieldRequestResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet TrackFieldRequest(string itemNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/TrackFieldRequests", ReplyAction="http://Argix.Enterprise/IUSPSService/TrackFieldRequestsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet TrackFieldRequests(string[] itemNumbers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/VerifyAddress", ReplyAction="http://Argix.Enterprise/IUSPSService/VerifyAddressResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet VerifyAddress(string firmName, string address1, string address2, string city, string state, string zip5, string zip4);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/LookupZipCode", ReplyAction="http://Argix.Enterprise/IUSPSService/LookupZipCodeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet LookupZipCode(string firmName, string address1, string address2, string city, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Enterprise/IUSPSService/LookupCityState", ReplyAction="http://Argix.Enterprise/IUSPSService/LookupCityStateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Enterprise.USPS.EnterpriseFault), Action="http://Argix.EnterpriseFault", Name="EnterpriseFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        System.Data.DataSet LookupCityState(string zip5);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUSPSServiceChannel : Argix.Enterprise.USPS.IUSPSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class USPSServiceClient : System.ServiceModel.ClientBase<Argix.Enterprise.USPS.IUSPSService>, Argix.Enterprise.USPS.IUSPSService {
        
        public USPSServiceClient() {
        }
        
        public USPSServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public USPSServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public USPSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public USPSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet TrackRequest(string itemNumber) {
            return base.Channel.TrackRequest(itemNumber);
        }
        
        public System.Data.DataSet TrackFieldRequest(string itemNumber) {
            return base.Channel.TrackFieldRequest(itemNumber);
        }
        
        public System.Data.DataSet TrackFieldRequests(string[] itemNumbers) {
            return base.Channel.TrackFieldRequests(itemNumbers);
        }
        
        public System.Data.DataSet VerifyAddress(string firmName, string address1, string address2, string city, string state, string zip5, string zip4) {
            return base.Channel.VerifyAddress(firmName, address1, address2, city, state, zip5, zip4);
        }
        
        public System.Data.DataSet LookupZipCode(string firmName, string address1, string address2, string city, string state) {
            return base.Channel.LookupZipCode(firmName, address1, address2, city, state);
        }
        
        public System.Data.DataSet LookupCityState(string zip5) {
            return base.Channel.LookupCityState(zip5);
        }
    }
}