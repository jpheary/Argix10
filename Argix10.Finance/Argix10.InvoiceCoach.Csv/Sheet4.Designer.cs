﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 414
namespace Argix.Finance {
    
    
    /// 
    [Microsoft.VisualStudio.Tools.Applications.Runtime.StartupObjectAttribute(3)]
    [global::System.Security.Permissions.PermissionSetAttribute(global::System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
    public sealed partial class Sheet4 : Microsoft.Office.Tools.Excel.WorksheetBase {
        
        internal Microsoft.Office.Tools.Excel.NamedRange Action;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Code;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Code1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Code2;
        
        internal Microsoft.Office.Tools.Excel.NamedRange CodeQualifier;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Headerqualifier;
        
        internal Microsoft.Office.Tools.Excel.NamedRange HeaderRefQualifier;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Sheet4_Print_Titles;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Qualifier;
        
        internal Microsoft.Office.Tools.Excel.NamedRange States;
        
        internal Microsoft.Office.Tools.Excel.NamedRange TaxExempt;
        
        internal Microsoft.Office.Tools.Excel.NamedRange UOM;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        private global::System.Object missing = global::System.Type.Missing;
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Sheet4(global::Microsoft.Office.Tools.Excel.Factory factory, global::System.IServiceProvider serviceProvider) : 
                base(factory, serviceProvider, "Sheet4", "Sheet4") {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void Initialize() {
            base.Initialize();
            Globals.Sheet4 = this;
            global::System.Windows.Forms.Application.EnableVisualStyles();
            this.InitializeCachedData();
            this.InitializeControls();
            this.InitializeComponents();
            this.InitializeData();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void FinishInitialization() {
            this.InternalStartup();
            this.OnStartup();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void InitializeDataBindings() {
            this.BeginInitialization();
            this.BindToData();
            this.EndInitialization();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeCachedData() {
            if ((this.DataHost == null)) {
                return;
            }
            if (this.DataHost.IsCacheInitialized) {
                this.DataHost.FillCachedData(this);
            }
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeData() {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void BindToData() {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StartCaching(string MemberName) {
            this.DataHost.StartCaching(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StopCaching(string MemberName) {
            this.DataHost.StopCaching(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private bool IsCached(string MemberName) {
            return this.DataHost.IsCached(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void BeginInitialization() {
            this.BeginInit();
            this.Action.BeginInit();
            this.Code.BeginInit();
            this.Code1.BeginInit();
            this.Code2.BeginInit();
            this.CodeQualifier.BeginInit();
            this.Headerqualifier.BeginInit();
            this.HeaderRefQualifier.BeginInit();
            this.Sheet4_Print_Titles.BeginInit();
            this.Qualifier.BeginInit();
            this.States.BeginInit();
            this.TaxExempt.BeginInit();
            this.UOM.BeginInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void EndInitialization() {
            this.UOM.EndInit();
            this.TaxExempt.EndInit();
            this.States.EndInit();
            this.Qualifier.EndInit();
            this.Sheet4_Print_Titles.EndInit();
            this.HeaderRefQualifier.EndInit();
            this.Headerqualifier.EndInit();
            this.CodeQualifier.EndInit();
            this.Code2.EndInit();
            this.Code1.EndInit();
            this.Code.EndInit();
            this.Action.EndInit();
            this.EndInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeControls() {
            this.Action = Globals.Factory.CreateNamedRange(null, null, "Action", "Action", this);
            this.Code = Globals.Factory.CreateNamedRange(null, null, "Code", "Code", this);
            this.Code1 = Globals.Factory.CreateNamedRange(null, null, "Code1", "Code1", this);
            this.Code2 = Globals.Factory.CreateNamedRange(null, null, "Code2", "Code2", this);
            this.CodeQualifier = Globals.Factory.CreateNamedRange(null, null, "CodeQualifier", "CodeQualifier", this);
            this.Headerqualifier = Globals.Factory.CreateNamedRange(null, null, "Headerqualifier", "Headerqualifier", this);
            this.HeaderRefQualifier = Globals.Factory.CreateNamedRange(null, null, "HeaderRefQualifier", "HeaderRefQualifier", this);
            this.Sheet4_Print_Titles = Globals.Factory.CreateNamedRange(null, null, "Sheet4!Print_Titles", "Sheet4_Print_Titles", this);
            this.Qualifier = Globals.Factory.CreateNamedRange(null, null, "Qualifier", "Qualifier", this);
            this.States = Globals.Factory.CreateNamedRange(null, null, "States", "States", this);
            this.TaxExempt = Globals.Factory.CreateNamedRange(null, null, "TaxExempt", "TaxExempt", this);
            this.UOM = Globals.Factory.CreateNamedRange(null, null, "UOM", "UOM", this);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeComponents() {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private bool NeedsFill(string MemberName) {
            return this.DataHost.NeedsFill(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "12.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void OnShutdown() {
            this.UOM.Dispose();
            this.TaxExempt.Dispose();
            this.States.Dispose();
            this.Qualifier.Dispose();
            this.Sheet4_Print_Titles.Dispose();
            this.HeaderRefQualifier.Dispose();
            this.Headerqualifier.Dispose();
            this.CodeQualifier.Dispose();
            this.Code2.Dispose();
            this.Code1.Dispose();
            this.Code.Dispose();
            this.Action.Dispose();
            base.OnShutdown();
        }
    }
    
    internal sealed partial class Globals {
        
        private static Sheet4 _Sheet4;
        
        internal static Sheet4 Sheet4 {
            get {
                return _Sheet4;
            }
            set {
                if ((_Sheet4 == null)) {
                    _Sheet4 = value;
                }
                else {
                    throw new System.NotSupportedException();
                }
            }
        }
    }
}
