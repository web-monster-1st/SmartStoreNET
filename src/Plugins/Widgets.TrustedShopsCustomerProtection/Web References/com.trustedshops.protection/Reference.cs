﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.18052
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Der Quellcode wurde automatisch mit Microsoft.VSDesigner generiert. Version 4.0.30319.18052.
// 
#pragma warning disable 1591

namespace SmartStore.Plugin.Widgets.TrustedShopsCustomerProtection.com.trustedshops.protection {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ApplicationRequestServiceBinding", Namespace="http://tsp.ts.nhp.com/")]
    public partial class ApplicationRequestService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback aliveOperationCompleted;
        
        private System.Threading.SendOrPostCallback getRequestStateOperationCompleted;
        
        private System.Threading.SendOrPostCallback pingOperationCompleted;
        
        private System.Threading.SendOrPostCallback requestForProtectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback requestForProtectionV2OperationCompleted;
        
        private System.Threading.SendOrPostCallback setShippingInformationOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ApplicationRequestService() {
            this.Url = global::SmartStore.Plugin.Widgets.TrustedShopsCustomerProtection.Properties.Settings.Default.SmartStore_Plugin_Widgets_TrustedShopsCustomerProtection_com_trustedshops_protection_ApplicationRequestService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event aliveCompletedEventHandler aliveCompleted;
        
        /// <remarks/>
        public event getRequestStateCompletedEventHandler getRequestStateCompleted;
        
        /// <remarks/>
        public event pingCompletedEventHandler pingCompleted;
        
        /// <remarks/>
        public event requestForProtectionCompletedEventHandler requestForProtectionCompleted;
        
        /// <remarks/>
        public event requestForProtectionV2CompletedEventHandler requestForProtectionV2Completed;
        
        /// <remarks/>
        public event setShippingInformationCompletedEventHandler setShippingInformationCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public int alive() {
            object[] results = this.Invoke("alive", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void aliveAsync() {
            this.aliveAsync(null);
        }
        
        /// <remarks/>
        public void aliveAsync(object userState) {
            if ((this.aliveOperationCompleted == null)) {
                this.aliveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnaliveOperationCompleted);
            }
            this.InvokeAsync("alive", new object[0], this.aliveOperationCompleted, userState);
        }
        
        private void OnaliveOperationCompleted(object arg) {
            if ((this.aliveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.aliveCompleted(this, new aliveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("getRequestState")]
        public long getRequestState(string tsId, long applicationId) {
            object[] results = this.Invoke("getRequestState", new object[] {
                        tsId,
                        applicationId});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void getRequestStateAsync(string tsId, long applicationId) {
            this.getRequestStateAsync(tsId, applicationId, null);
        }
        
        /// <remarks/>
        public void getRequestStateAsync(string tsId, long applicationId, object userState) {
            if ((this.getRequestStateOperationCompleted == null)) {
                this.getRequestStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetRequestStateOperationCompleted);
            }
            this.InvokeAsync("getRequestState", new object[] {
                        tsId,
                        applicationId}, this.getRequestStateOperationCompleted, userState);
        }
        
        private void OngetRequestStateOperationCompleted(object arg) {
            if ((this.getRequestStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getRequestStateCompleted(this, new getRequestStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public int ping(int arg0) {
            object[] results = this.Invoke("ping", new object[] {
                        arg0});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void pingAsync(int arg0) {
            this.pingAsync(arg0, null);
        }
        
        /// <remarks/>
        public void pingAsync(int arg0, object userState) {
            if ((this.pingOperationCompleted == null)) {
                this.pingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpingOperationCompleted);
            }
            this.InvokeAsync("ping", new object[] {
                        arg0}, this.pingOperationCompleted, userState);
        }
        
        private void OnpingOperationCompleted(object arg) {
            if ((this.pingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.pingCompleted(this, new pingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("requestForProtection")]
        public long requestForProtection(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string wsUser, string wsPassword) {
            object[] results = this.Invoke("requestForProtection", new object[] {
                        tsId,
                        tsProductId,
                        amount,
                        currency,
                        paymentType,
                        buyerEmail,
                        shopCustomerID,
                        shopOrderID,
                        orderDate,
                        wsUser,
                        wsPassword});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void requestForProtectionAsync(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string wsUser, string wsPassword) {
            this.requestForProtectionAsync(tsId, tsProductId, amount, currency, paymentType, buyerEmail, shopCustomerID, shopOrderID, orderDate, wsUser, wsPassword, null);
        }
        
        /// <remarks/>
        public void requestForProtectionAsync(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string wsUser, string wsPassword, object userState) {
            if ((this.requestForProtectionOperationCompleted == null)) {
                this.requestForProtectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrequestForProtectionOperationCompleted);
            }
            this.InvokeAsync("requestForProtection", new object[] {
                        tsId,
                        tsProductId,
                        amount,
                        currency,
                        paymentType,
                        buyerEmail,
                        shopCustomerID,
                        shopOrderID,
                        orderDate,
                        wsUser,
                        wsPassword}, this.requestForProtectionOperationCompleted, userState);
        }
        
        private void OnrequestForProtectionOperationCompleted(object arg) {
            if ((this.requestForProtectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.requestForProtectionCompleted(this, new requestForProtectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("requestForProtectionV2")]
        public long requestForProtectionV2(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string shopSystemVersion, string wsUser, string wsPassword) {
            object[] results = this.Invoke("requestForProtectionV2", new object[] {
                        tsId,
                        tsProductId,
                        amount,
                        currency,
                        paymentType,
                        buyerEmail,
                        shopCustomerID,
                        shopOrderID,
                        orderDate,
                        shopSystemVersion,
                        wsUser,
                        wsPassword});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void requestForProtectionV2Async(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string shopSystemVersion, string wsUser, string wsPassword) {
            this.requestForProtectionV2Async(tsId, tsProductId, amount, currency, paymentType, buyerEmail, shopCustomerID, shopOrderID, orderDate, shopSystemVersion, wsUser, wsPassword, null);
        }
        
        /// <remarks/>
        public void requestForProtectionV2Async(string tsId, string tsProductId, decimal amount, string currency, string paymentType, string buyerEmail, string shopCustomerID, string shopOrderID, System.DateTime orderDate, string shopSystemVersion, string wsUser, string wsPassword, object userState) {
            if ((this.requestForProtectionV2OperationCompleted == null)) {
                this.requestForProtectionV2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnrequestForProtectionV2OperationCompleted);
            }
            this.InvokeAsync("requestForProtectionV2", new object[] {
                        tsId,
                        tsProductId,
                        amount,
                        currency,
                        paymentType,
                        buyerEmail,
                        shopCustomerID,
                        shopOrderID,
                        orderDate,
                        shopSystemVersion,
                        wsUser,
                        wsPassword}, this.requestForProtectionV2OperationCompleted, userState);
        }
        
        private void OnrequestForProtectionV2OperationCompleted(object arg) {
            if ((this.requestForProtectionV2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.requestForProtectionV2Completed(this, new requestForProtectionV2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://tsp.ts.nhp.com/", ResponseNamespace="http://tsp.ts.nhp.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("setShippingInformation")]
        public long setShippingInformation(string tsId, string orderId, string customerId, string logisticsCompany, System.DateTime shippingDate, string trackingNumber) {
            object[] results = this.Invoke("setShippingInformation", new object[] {
                        tsId,
                        orderId,
                        customerId,
                        logisticsCompany,
                        shippingDate,
                        trackingNumber});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void setShippingInformationAsync(string tsId, string orderId, string customerId, string logisticsCompany, System.DateTime shippingDate, string trackingNumber) {
            this.setShippingInformationAsync(tsId, orderId, customerId, logisticsCompany, shippingDate, trackingNumber, null);
        }
        
        /// <remarks/>
        public void setShippingInformationAsync(string tsId, string orderId, string customerId, string logisticsCompany, System.DateTime shippingDate, string trackingNumber, object userState) {
            if ((this.setShippingInformationOperationCompleted == null)) {
                this.setShippingInformationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetShippingInformationOperationCompleted);
            }
            this.InvokeAsync("setShippingInformation", new object[] {
                        tsId,
                        orderId,
                        customerId,
                        logisticsCompany,
                        shippingDate,
                        trackingNumber}, this.setShippingInformationOperationCompleted, userState);
        }
        
        private void OnsetShippingInformationOperationCompleted(object arg) {
            if ((this.setShippingInformationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setShippingInformationCompleted(this, new setShippingInformationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void aliveCompletedEventHandler(object sender, aliveCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class aliveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal aliveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void getRequestStateCompletedEventHandler(object sender, getRequestStateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getRequestStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getRequestStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void pingCompletedEventHandler(object sender, pingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class pingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal pingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void requestForProtectionCompletedEventHandler(object sender, requestForProtectionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class requestForProtectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal requestForProtectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void requestForProtectionV2CompletedEventHandler(object sender, requestForProtectionV2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class requestForProtectionV2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal requestForProtectionV2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void setShippingInformationCompletedEventHandler(object sender, setShippingInformationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class setShippingInformationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal setShippingInformationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591