﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bizapps_test.MVC.WcfBlogUserServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BlogUserDC", Namespace="http://schemas.datacontract.org/2004/07/bizapps_test.SL.Wcf_Services.DataContract" +
        "s")]
    [System.SerializableAttribute()]
    public partial class BlogUserDC : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BlogNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserPasswordField;
        
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
        public string BlogName {
            get {
                return this.BlogNameField;
            }
            set {
                if ((object.ReferenceEquals(this.BlogNameField, value) != true)) {
                    this.BlogNameField = value;
                    this.RaisePropertyChanged("BlogName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
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
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserPassword {
            get {
                return this.UserPasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.UserPasswordField, value) != true)) {
                    this.UserPasswordField = value;
                    this.RaisePropertyChanged("UserPassword");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfBlogUserServiceReference.IWcfBlogUserService")]
    public interface IWcfBlogUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/CreateBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/CreateBlogUserResponse")]
        int CreateBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/CreateBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/CreateBlogUserResponse")]
        System.Threading.Tasks.Task<int> CreateBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/UpdateBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/UpdateBlogUserResponse")]
        int UpdateBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/UpdateBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/UpdateBlogUserResponse")]
        System.Threading.Tasks.Task<int> UpdateBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/DeleteBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/DeleteBlogUserResponse")]
        int DeleteBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/DeleteBlogUser", ReplyAction="http://tempuri.org/IWcfBlogUserService/DeleteBlogUserResponse")]
        System.Threading.Tasks.Task<int> DeleteBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/ChangePassword", ReplyAction="http://tempuri.org/IWcfBlogUserService/ChangePasswordResponse")]
        int ChangePassword(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC blogUserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/ChangePassword", ReplyAction="http://tempuri.org/IWcfBlogUserService/ChangePasswordResponse")]
        System.Threading.Tasks.Task<int> ChangePasswordAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC blogUserDC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetAllUsers", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetAllUsersResponse")]
        bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetAllUsers", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC[]> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserById", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserByIdResponse")]
        bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserById(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserById", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserByIdResponse")]
        System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserByIdAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserByName", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserByNameResponse")]
        bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserByName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserByName", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserByNameResponse")]
        System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserByNameAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserNameAndPassword", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserNameAndPasswordResponse")]
        bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserNameAndPassword(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC incomingUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetBlogUserNameAndPassword", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetBlogUserNameAndPasswordResponse")]
        System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserNameAndPasswordAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC incomingUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetAdminPermission", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetAdminPermissionResponse")]
        int GetAdminPermission(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfBlogUserService/GetAdminPermission", ReplyAction="http://tempuri.org/IWcfBlogUserService/GetAdminPermissionResponse")]
        System.Threading.Tasks.Task<int> GetAdminPermissionAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfBlogUserServiceChannel : bizapps_test.MVC.WcfBlogUserServiceReference.IWcfBlogUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WcfBlogUserServiceClient : System.ServiceModel.ClientBase<bizapps_test.MVC.WcfBlogUserServiceReference.IWcfBlogUserService>, bizapps_test.MVC.WcfBlogUserServiceReference.IWcfBlogUserService {
        
        public WcfBlogUserServiceClient() {
        }
        
        public WcfBlogUserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WcfBlogUserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfBlogUserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfBlogUserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int CreateBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.CreateBlogUser(bloguserDC);
        }
        
        public System.Threading.Tasks.Task<int> CreateBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.CreateBlogUserAsync(bloguserDC);
        }
        
        public int UpdateBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.UpdateBlogUser(bloguserDC);
        }
        
        public System.Threading.Tasks.Task<int> UpdateBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.UpdateBlogUserAsync(bloguserDC);
        }
        
        public int DeleteBlogUser(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.DeleteBlogUser(bloguserDC);
        }
        
        public System.Threading.Tasks.Task<int> DeleteBlogUserAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC bloguserDC) {
            return base.Channel.DeleteBlogUserAsync(bloguserDC);
        }
        
        public int ChangePassword(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC blogUserDC) {
            return base.Channel.ChangePassword(blogUserDC);
        }
        
        public System.Threading.Tasks.Task<int> ChangePasswordAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC blogUserDC) {
            return base.Channel.ChangePasswordAsync(blogUserDC);
        }
        
        public bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC[]> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserById(int userId) {
            return base.Channel.GetBlogUserById(userId);
        }
        
        public System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserByIdAsync(int userId) {
            return base.Channel.GetBlogUserByIdAsync(userId);
        }
        
        public bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserByName(string userName) {
            return base.Channel.GetBlogUserByName(userName);
        }
        
        public System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserByNameAsync(string userName) {
            return base.Channel.GetBlogUserByNameAsync(userName);
        }
        
        public bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC GetBlogUserNameAndPassword(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC incomingUser) {
            return base.Channel.GetBlogUserNameAndPassword(incomingUser);
        }
        
        public System.Threading.Tasks.Task<bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC> GetBlogUserNameAndPasswordAsync(bizapps_test.MVC.WcfBlogUserServiceReference.BlogUserDC incomingUser) {
            return base.Channel.GetBlogUserNameAndPasswordAsync(incomingUser);
        }
        
        public int GetAdminPermission(string userName) {
            return base.Channel.GetAdminPermission(userName);
        }
        
        public System.Threading.Tasks.Task<int> GetAdminPermissionAsync(string userName) {
            return base.Channel.GetAdminPermissionAsync(userName);
        }
    }
}
