﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItOps.MessageHandlers.Finance.WCF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InstallmentDto", Namespace="http://schemas.datacontract.org/2004/07/Finance.WCF.DataTransferObjects")]
    [System.SerializableAttribute()]
    public partial class InstallmentDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ItOps.MessageHandlers.Finance.WCF.AccountDto AccountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DueDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PaidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ItOps.MessageHandlers.Finance.WCF.InstallmentStatus StatusField;
        
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
        public ItOps.MessageHandlers.Finance.WCF.AccountDto Account {
            get {
                return this.AccountField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountField, value) != true)) {
                    this.AccountField = value;
                    this.RaisePropertyChanged("Account");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DueDate {
            get {
                return this.DueDateField;
            }
            set {
                if ((this.DueDateField.Equals(value) != true)) {
                    this.DueDateField = value;
                    this.RaisePropertyChanged("DueDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Paid {
            get {
                return this.PaidField;
            }
            set {
                if ((this.PaidField.Equals(value) != true)) {
                    this.PaidField = value;
                    this.RaisePropertyChanged("Paid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItOps.MessageHandlers.Finance.WCF.InstallmentStatus Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="AccountDto", Namespace="http://schemas.datacontract.org/2004/07/Finance.WCF.DataTransferObjects")]
    [System.SerializableAttribute()]
    public partial class AccountDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid AgreementIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid ClientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExpiryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ItOps.MessageHandlers.Finance.WCF.InstallmentDto[] InstallmentsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ItOps.MessageHandlers.Finance.WCF.AccountStatus StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValueField;
        
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
        public System.Guid AgreementId {
            get {
                return this.AgreementIdField;
            }
            set {
                if ((this.AgreementIdField.Equals(value) != true)) {
                    this.AgreementIdField = value;
                    this.RaisePropertyChanged("AgreementId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ClientId {
            get {
                return this.ClientIdField;
            }
            set {
                if ((this.ClientIdField.Equals(value) != true)) {
                    this.ClientIdField = value;
                    this.RaisePropertyChanged("ClientId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Expiry {
            get {
                return this.ExpiryField;
            }
            set {
                if ((this.ExpiryField.Equals(value) != true)) {
                    this.ExpiryField = value;
                    this.RaisePropertyChanged("Expiry");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItOps.MessageHandlers.Finance.WCF.InstallmentDto[] Installments {
            get {
                return this.InstallmentsField;
            }
            set {
                if ((object.ReferenceEquals(this.InstallmentsField, value) != true)) {
                    this.InstallmentsField = value;
                    this.RaisePropertyChanged("Installments");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItOps.MessageHandlers.Finance.WCF.AccountStatus Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="InstallmentStatus", Namespace="http://schemas.datacontract.org/2004/07/Finance.Domain.Enumerations")]
    public enum InstallmentStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pending = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Due = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Overdue = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Paid = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotRequired = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AccountStatus", Namespace="http://schemas.datacontract.org/2004/07/Finance.Domain.Enumerations")]
    public enum AccountStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Open = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Closed = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Suspended = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Finance.WCF.IInstallmentService")]
    public interface IInstallmentService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInstallmentService/GetById", ReplyAction="http://tempuri.org/IInstallmentService/GetByIdResponse")]
        ItOps.MessageHandlers.Finance.WCF.InstallmentDto GetById(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IInstallmentServiceChannel : ItOps.MessageHandlers.Finance.WCF.IInstallmentService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InstallmentServiceClient : System.ServiceModel.ClientBase<ItOps.MessageHandlers.Finance.WCF.IInstallmentService>, ItOps.MessageHandlers.Finance.WCF.IInstallmentService {
        
        public InstallmentServiceClient() {
        }
        
        public InstallmentServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InstallmentServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InstallmentServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InstallmentServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ItOps.MessageHandlers.Finance.WCF.InstallmentDto GetById(System.Guid id) {
            return base.Channel.GetById(id);
        }
    }
}