﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Argix.AgentLineHaul {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceInfo", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class ServiceInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConnectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TerminalIDField;
        
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
        public string Connection {
            get {
                return this.ConnectionField;
            }
            set {
                if ((object.ReferenceEquals(this.ConnectionField, value) != true)) {
                    this.ConnectionField = value;
                    this.RaisePropertyChanged("Connection");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Number {
            get {
                return this.NumberField;
            }
            set {
                if ((object.ReferenceEquals(this.NumberField, value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TerminalID {
            get {
                return this.TerminalIDField;
            }
            set {
                if ((this.TerminalIDField.Equals(value) != true)) {
                    this.TerminalIDField = value;
                    this.RaisePropertyChanged("TerminalID");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class ConfigurationFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="UserConfiguration", Namespace="http://schemas.datacontract.org/2004/07/Argix", ItemName="KeyValueOfstringstring", KeyName="Key", ValueName="Value")]
    [System.SerializableAttribute()]
    public class UserConfiguration : System.Collections.Generic.Dictionary<string, string> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TraceMessage", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class TraceMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ComputerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EventField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword3Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Argix.AgentLineHaul.LogLevel LogLevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserField;
        
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
        public string Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryField, value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Computer {
            get {
                return this.ComputerField;
            }
            set {
                if ((object.ReferenceEquals(this.ComputerField, value) != true)) {
                    this.ComputerField = value;
                    this.RaisePropertyChanged("Computer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Event {
            get {
                return this.EventField;
            }
            set {
                if ((object.ReferenceEquals(this.EventField, value) != true)) {
                    this.EventField = value;
                    this.RaisePropertyChanged("Event");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword1 {
            get {
                return this.Keyword1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword1Field, value) != true)) {
                    this.Keyword1Field = value;
                    this.RaisePropertyChanged("Keyword1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword2 {
            get {
                return this.Keyword2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword2Field, value) != true)) {
                    this.Keyword2Field = value;
                    this.RaisePropertyChanged("Keyword2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword3 {
            get {
                return this.Keyword3Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword3Field, value) != true)) {
                    this.Keyword3Field = value;
                    this.RaisePropertyChanged("Keyword3");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Argix.AgentLineHaul.LogLevel LogLevel {
            get {
                return this.LogLevelField;
            }
            set {
                if ((this.LogLevelField.Equals(value) != true)) {
                    this.LogLevelField = value;
                    this.RaisePropertyChanged("LogLevel");
                }
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogLevel", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    public enum LogLevel : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Debug = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Information = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Warning = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ISDClient", Namespace="http://schemas.datacontract.org/2004/07/Argix.AgentLineHaul")]
    [System.SerializableAttribute()]
    public partial class ISDClient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CLIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CounterKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExportFormatField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExportPathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ScannerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIDField;
        
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
        public string CLID {
            get {
                return this.CLIDField;
            }
            set {
                if ((object.ReferenceEquals(this.CLIDField, value) != true)) {
                    this.CLIDField = value;
                    this.RaisePropertyChanged("CLID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Client {
            get {
                return this.ClientField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientField, value) != true)) {
                    this.ClientField = value;
                    this.RaisePropertyChanged("Client");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CounterKey {
            get {
                return this.CounterKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.CounterKeyField, value) != true)) {
                    this.CounterKeyField = value;
                    this.RaisePropertyChanged("CounterKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExportFormat {
            get {
                return this.ExportFormatField;
            }
            set {
                if ((object.ReferenceEquals(this.ExportFormatField, value) != true)) {
                    this.ExportFormatField = value;
                    this.RaisePropertyChanged("ExportFormat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExportPath {
            get {
                return this.ExportPathField;
            }
            set {
                if ((object.ReferenceEquals(this.ExportPathField, value) != true)) {
                    this.ExportPathField = value;
                    this.RaisePropertyChanged("ExportPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Scanner {
            get {
                return this.ScannerField;
            }
            set {
                if ((object.ReferenceEquals(this.ScannerField, value) != true)) {
                    this.ScannerField = value;
                    this.RaisePropertyChanged("Scanner");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIDField, value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="AgentLineHaulFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.AgentLineHaul")]
    [System.SerializableAttribute()]
    public partial class AgentLineHaulFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Argix.AgentLineHaul", ConfigurationName="AgentLineHaul.IISDExportService")]
    public interface IISDExportService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetServiceInfo", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetServiceInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.AgentLineHaul.ConfigurationFault), Action="http://Argix.ConfigurationFault", Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        Argix.AgentLineHaul.ServiceInfo GetServiceInfo(int terminalID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetUserConfiguration", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetUserConfigurationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.AgentLineHaul.ConfigurationFault), Action="http://Argix.ConfigurationFault", Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        Argix.AgentLineHaul.UserConfiguration GetUserConfiguration(int terminalID, string application, string[] usernames);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://Argix.AgentLineHaul/IISDExportService/WriteLogEntry")]
        void WriteLogEntry(int terminalID, Argix.AgentLineHaul.TraceMessage m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetPickups", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetPickupsResponse")]
        System.Data.DataSet GetPickups(int terminalID, System.DateTime pickupDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetSortedItems", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetSortedItemsResponse")]
        System.Data.DataSet GetSortedItems(int terminalID, string pickupID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetExportFilename", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetExportFilenameResponse")]
        string GetExportFilename(int terminalID, string counterKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/GetISDClients", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/GetISDClientsResponse")]
        System.Data.DataSet GetISDClients(int terminalID, string clientNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/CreateISDClient", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/CreateISDClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.AgentLineHaul.AgentLineHaulFault), Action="http://Argix.AgentLineHaul.AgentLineHaulFault", Name="AgentLineHaulFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.AgentLineHaul")]
        bool CreateISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/UpdateISDClient", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/UpdateISDClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.AgentLineHaul.AgentLineHaulFault), Action="http://Argix.AgentLineHaul.AgentLineHaulFault", Name="AgentLineHaulFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.AgentLineHaul")]
        bool UpdateISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.AgentLineHaul/IISDExportService/DeleteISDClient", ReplyAction="http://Argix.AgentLineHaul/IISDExportService/DeleteISDClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.AgentLineHaul.AgentLineHaulFault), Action="http://Argix.AgentLineHaul.AgentLineHaulFault", Name="AgentLineHaulFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.AgentLineHaul")]
        bool DeleteISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IISDExportServiceChannel : Argix.AgentLineHaul.IISDExportService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ISDExportServiceClient : System.ServiceModel.ClientBase<Argix.AgentLineHaul.IISDExportService>, Argix.AgentLineHaul.IISDExportService {
        
        public ISDExportServiceClient() {
        }
        
        public ISDExportServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ISDExportServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ISDExportServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ISDExportServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Argix.AgentLineHaul.ServiceInfo GetServiceInfo(int terminalID) {
            return base.Channel.GetServiceInfo(terminalID);
        }
        
        public Argix.AgentLineHaul.UserConfiguration GetUserConfiguration(int terminalID, string application, string[] usernames) {
            return base.Channel.GetUserConfiguration(terminalID, application, usernames);
        }
        
        public void WriteLogEntry(int terminalID, Argix.AgentLineHaul.TraceMessage m) {
            base.Channel.WriteLogEntry(terminalID, m);
        }
        
        public System.Data.DataSet GetPickups(int terminalID, System.DateTime pickupDate) {
            return base.Channel.GetPickups(terminalID, pickupDate);
        }
        
        public System.Data.DataSet GetSortedItems(int terminalID, string pickupID) {
            return base.Channel.GetSortedItems(terminalID, pickupID);
        }
        
        public string GetExportFilename(int terminalID, string counterKey) {
            return base.Channel.GetExportFilename(terminalID, counterKey);
        }
        
        public System.Data.DataSet GetISDClients(int terminalID, string clientNumber) {
            return base.Channel.GetISDClients(terminalID, clientNumber);
        }
        
        public bool CreateISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client) {
            return base.Channel.CreateISDClient(terminalID, client);
        }
        
        public bool UpdateISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client) {
            return base.Channel.UpdateISDClient(terminalID, client);
        }
        
        public bool DeleteISDClient(int terminalID, Argix.AgentLineHaul.ISDClient client) {
            return base.Channel.DeleteISDClient(terminalID, client);
        }
    }
}
