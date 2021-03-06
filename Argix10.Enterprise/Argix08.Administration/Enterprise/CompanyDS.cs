﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Tsort.Enterprise {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class CompanyDS : DataSet {
        
        private CompanyListTableDataTable tableCompanyListTable;
        
        private CompanyPaymentServiceTableDataTable tableCompanyPaymentServiceTable;
        
        public CompanyDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected CompanyDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["CompanyListTable"] != null)) {
                    this.Tables.Add(new CompanyListTableDataTable(ds.Tables["CompanyListTable"]));
                }
                if ((ds.Tables["CompanyPaymentServiceTable"] != null)) {
                    this.Tables.Add(new CompanyPaymentServiceTableDataTable(ds.Tables["CompanyPaymentServiceTable"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public CompanyListTableDataTable CompanyListTable {
            get {
                return this.tableCompanyListTable;
            }
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public CompanyPaymentServiceTableDataTable CompanyPaymentServiceTable {
            get {
                return this.tableCompanyPaymentServiceTable;
            }
        }
        
        public override DataSet Clone() {
            CompanyDS cln = ((CompanyDS)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["CompanyListTable"] != null)) {
                this.Tables.Add(new CompanyListTableDataTable(ds.Tables["CompanyListTable"]));
            }
            if ((ds.Tables["CompanyPaymentServiceTable"] != null)) {
                this.Tables.Add(new CompanyPaymentServiceTableDataTable(ds.Tables["CompanyPaymentServiceTable"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableCompanyListTable = ((CompanyListTableDataTable)(this.Tables["CompanyListTable"]));
            if ((this.tableCompanyListTable != null)) {
                this.tableCompanyListTable.InitVars();
            }
            this.tableCompanyPaymentServiceTable = ((CompanyPaymentServiceTableDataTable)(this.Tables["CompanyPaymentServiceTable"]));
            if ((this.tableCompanyPaymentServiceTable != null)) {
                this.tableCompanyPaymentServiceTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "CompanyDS";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/CompanyDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableCompanyListTable = new CompanyListTableDataTable();
            this.Tables.Add(this.tableCompanyListTable);
            this.tableCompanyPaymentServiceTable = new CompanyPaymentServiceTableDataTable();
            this.Tables.Add(this.tableCompanyPaymentServiceTable);
        }
        
        private bool ShouldSerializeCompanyListTable() {
            return false;
        }
        
        private bool ShouldSerializeCompanyPaymentServiceTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void CompanyListTableRowChangeEventHandler(object sender, CompanyListTableRowChangeEvent e);
        
        public delegate void CompanyPaymentServiceTableRowChangeEventHandler(object sender, CompanyPaymentServiceTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyListTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnCompanyName;
            
            private DataColumn columnCompanyID;
            
            private DataColumn columnNumber;
            
            internal CompanyListTableDataTable() : 
                    base("CompanyListTable") {
                this.InitClass();
            }
            
            internal CompanyListTableDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn CompanyNameColumn {
                get {
                    return this.columnCompanyName;
                }
            }
            
            internal DataColumn CompanyIDColumn {
                get {
                    return this.columnCompanyID;
                }
            }
            
            internal DataColumn NumberColumn {
                get {
                    return this.columnNumber;
                }
            }
            
            public CompanyListTableRow this[int index] {
                get {
                    return ((CompanyListTableRow)(this.Rows[index]));
                }
            }
            
            public event CompanyListTableRowChangeEventHandler CompanyListTableRowChanged;
            
            public event CompanyListTableRowChangeEventHandler CompanyListTableRowChanging;
            
            public event CompanyListTableRowChangeEventHandler CompanyListTableRowDeleted;
            
            public event CompanyListTableRowChangeEventHandler CompanyListTableRowDeleting;
            
            public void AddCompanyListTableRow(CompanyListTableRow row) {
                this.Rows.Add(row);
            }
            
            public CompanyListTableRow AddCompanyListTableRow(string CompanyName, int CompanyID, string Number) {
                CompanyListTableRow rowCompanyListTableRow = ((CompanyListTableRow)(this.NewRow()));
                rowCompanyListTableRow.ItemArray = new object[] {
                        CompanyName,
                        CompanyID,
                        Number};
                this.Rows.Add(rowCompanyListTableRow);
                return rowCompanyListTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                CompanyListTableDataTable cln = ((CompanyListTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new CompanyListTableDataTable();
            }
            
            internal void InitVars() {
                this.columnCompanyName = this.Columns["CompanyName"];
                this.columnCompanyID = this.Columns["CompanyID"];
                this.columnNumber = this.Columns["Number"];
            }
            
            private void InitClass() {
                this.columnCompanyName = new DataColumn("CompanyName", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCompanyName);
                this.columnCompanyID = new DataColumn("CompanyID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCompanyID);
                this.columnNumber = new DataColumn("Number", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnNumber);
            }
            
            public CompanyListTableRow NewCompanyListTableRow() {
                return ((CompanyListTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new CompanyListTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(CompanyListTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.CompanyListTableRowChanged != null)) {
                    this.CompanyListTableRowChanged(this, new CompanyListTableRowChangeEvent(((CompanyListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.CompanyListTableRowChanging != null)) {
                    this.CompanyListTableRowChanging(this, new CompanyListTableRowChangeEvent(((CompanyListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.CompanyListTableRowDeleted != null)) {
                    this.CompanyListTableRowDeleted(this, new CompanyListTableRowChangeEvent(((CompanyListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.CompanyListTableRowDeleting != null)) {
                    this.CompanyListTableRowDeleting(this, new CompanyListTableRowChangeEvent(((CompanyListTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveCompanyListTableRow(CompanyListTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyListTableRow : DataRow {
            
            private CompanyListTableDataTable tableCompanyListTable;
            
            internal CompanyListTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableCompanyListTable = ((CompanyListTableDataTable)(this.Table));
            }
            
            public string CompanyName {
                get {
                    try {
                        return ((string)(this[this.tableCompanyListTable.CompanyNameColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyListTable.CompanyNameColumn] = value;
                }
            }
            
            public int CompanyID {
                get {
                    try {
                        return ((int)(this[this.tableCompanyListTable.CompanyIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyListTable.CompanyIDColumn] = value;
                }
            }
            
            public string Number {
                get {
                    try {
                        return ((string)(this[this.tableCompanyListTable.NumberColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyListTable.NumberColumn] = value;
                }
            }
            
            public bool IsCompanyNameNull() {
                return this.IsNull(this.tableCompanyListTable.CompanyNameColumn);
            }
            
            public void SetCompanyNameNull() {
                this[this.tableCompanyListTable.CompanyNameColumn] = System.Convert.DBNull;
            }
            
            public bool IsCompanyIDNull() {
                return this.IsNull(this.tableCompanyListTable.CompanyIDColumn);
            }
            
            public void SetCompanyIDNull() {
                this[this.tableCompanyListTable.CompanyIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsNumberNull() {
                return this.IsNull(this.tableCompanyListTable.NumberColumn);
            }
            
            public void SetNumberNull() {
                this[this.tableCompanyListTable.NumberColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyListTableRowChangeEvent : EventArgs {
            
            private CompanyListTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public CompanyListTableRowChangeEvent(CompanyListTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public CompanyListTableRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyPaymentServiceTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnCompanyID;
            
            private DataColumn columnPaymentServiceID;
            
            private DataColumn columnPaymentServiceName;
            
            private DataColumn columnPaymentServiceNumber;
            
            private DataColumn columnIsActive;
            
            private DataColumn columnComments;
            
            private DataColumn columnLastUpdated;
            
            private DataColumn columnUserID;
            
            private DataColumn columnRowVersion;
            
            internal CompanyPaymentServiceTableDataTable() : 
                    base("CompanyPaymentServiceTable") {
                this.InitClass();
            }
            
            internal CompanyPaymentServiceTableDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn CompanyIDColumn {
                get {
                    return this.columnCompanyID;
                }
            }
            
            internal DataColumn PaymentServiceIDColumn {
                get {
                    return this.columnPaymentServiceID;
                }
            }
            
            internal DataColumn PaymentServiceNameColumn {
                get {
                    return this.columnPaymentServiceName;
                }
            }
            
            internal DataColumn PaymentServiceNumberColumn {
                get {
                    return this.columnPaymentServiceNumber;
                }
            }
            
            internal DataColumn IsActiveColumn {
                get {
                    return this.columnIsActive;
                }
            }
            
            internal DataColumn CommentsColumn {
                get {
                    return this.columnComments;
                }
            }
            
            internal DataColumn LastUpdatedColumn {
                get {
                    return this.columnLastUpdated;
                }
            }
            
            internal DataColumn UserIDColumn {
                get {
                    return this.columnUserID;
                }
            }
            
            internal DataColumn RowVersionColumn {
                get {
                    return this.columnRowVersion;
                }
            }
            
            public CompanyPaymentServiceTableRow this[int index] {
                get {
                    return ((CompanyPaymentServiceTableRow)(this.Rows[index]));
                }
            }
            
            public event CompanyPaymentServiceTableRowChangeEventHandler CompanyPaymentServiceTableRowChanged;
            
            public event CompanyPaymentServiceTableRowChangeEventHandler CompanyPaymentServiceTableRowChanging;
            
            public event CompanyPaymentServiceTableRowChangeEventHandler CompanyPaymentServiceTableRowDeleted;
            
            public event CompanyPaymentServiceTableRowChangeEventHandler CompanyPaymentServiceTableRowDeleting;
            
            public void AddCompanyPaymentServiceTableRow(CompanyPaymentServiceTableRow row) {
                this.Rows.Add(row);
            }
            
            public CompanyPaymentServiceTableRow AddCompanyPaymentServiceTableRow(int CompanyID, int PaymentServiceID, string PaymentServiceName, string PaymentServiceNumber, bool IsActive, string Comments, System.DateTime LastUpdated, string UserID, string RowVersion) {
                CompanyPaymentServiceTableRow rowCompanyPaymentServiceTableRow = ((CompanyPaymentServiceTableRow)(this.NewRow()));
                rowCompanyPaymentServiceTableRow.ItemArray = new object[] {
                        CompanyID,
                        PaymentServiceID,
                        PaymentServiceName,
                        PaymentServiceNumber,
                        IsActive,
                        Comments,
                        LastUpdated,
                        UserID,
                        RowVersion};
                this.Rows.Add(rowCompanyPaymentServiceTableRow);
                return rowCompanyPaymentServiceTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                CompanyPaymentServiceTableDataTable cln = ((CompanyPaymentServiceTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new CompanyPaymentServiceTableDataTable();
            }
            
            internal void InitVars() {
                this.columnCompanyID = this.Columns["CompanyID"];
                this.columnPaymentServiceID = this.Columns["PaymentServiceID"];
                this.columnPaymentServiceName = this.Columns["PaymentServiceName"];
                this.columnPaymentServiceNumber = this.Columns["PaymentServiceNumber"];
                this.columnIsActive = this.Columns["IsActive"];
                this.columnComments = this.Columns["Comments"];
                this.columnLastUpdated = this.Columns["LastUpdated"];
                this.columnUserID = this.Columns["UserID"];
                this.columnRowVersion = this.Columns["RowVersion"];
            }
            
            private void InitClass() {
                this.columnCompanyID = new DataColumn("CompanyID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCompanyID);
                this.columnPaymentServiceID = new DataColumn("PaymentServiceID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPaymentServiceID);
                this.columnPaymentServiceName = new DataColumn("PaymentServiceName", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPaymentServiceName);
                this.columnPaymentServiceNumber = new DataColumn("PaymentServiceNumber", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPaymentServiceNumber);
                this.columnIsActive = new DataColumn("IsActive", typeof(bool), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnIsActive);
                this.columnComments = new DataColumn("Comments", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnComments);
                this.columnLastUpdated = new DataColumn("LastUpdated", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLastUpdated);
                this.columnUserID = new DataColumn("UserID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUserID);
                this.columnRowVersion = new DataColumn("RowVersion", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRowVersion);
            }
            
            public CompanyPaymentServiceTableRow NewCompanyPaymentServiceTableRow() {
                return ((CompanyPaymentServiceTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new CompanyPaymentServiceTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(CompanyPaymentServiceTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.CompanyPaymentServiceTableRowChanged != null)) {
                    this.CompanyPaymentServiceTableRowChanged(this, new CompanyPaymentServiceTableRowChangeEvent(((CompanyPaymentServiceTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.CompanyPaymentServiceTableRowChanging != null)) {
                    this.CompanyPaymentServiceTableRowChanging(this, new CompanyPaymentServiceTableRowChangeEvent(((CompanyPaymentServiceTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.CompanyPaymentServiceTableRowDeleted != null)) {
                    this.CompanyPaymentServiceTableRowDeleted(this, new CompanyPaymentServiceTableRowChangeEvent(((CompanyPaymentServiceTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.CompanyPaymentServiceTableRowDeleting != null)) {
                    this.CompanyPaymentServiceTableRowDeleting(this, new CompanyPaymentServiceTableRowChangeEvent(((CompanyPaymentServiceTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveCompanyPaymentServiceTableRow(CompanyPaymentServiceTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyPaymentServiceTableRow : DataRow {
            
            private CompanyPaymentServiceTableDataTable tableCompanyPaymentServiceTable;
            
            internal CompanyPaymentServiceTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableCompanyPaymentServiceTable = ((CompanyPaymentServiceTableDataTable)(this.Table));
            }
            
            public int CompanyID {
                get {
                    try {
                        return ((int)(this[this.tableCompanyPaymentServiceTable.CompanyIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.CompanyIDColumn] = value;
                }
            }
            
            public int PaymentServiceID {
                get {
                    try {
                        return ((int)(this[this.tableCompanyPaymentServiceTable.PaymentServiceIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.PaymentServiceIDColumn] = value;
                }
            }
            
            public string PaymentServiceName {
                get {
                    try {
                        return ((string)(this[this.tableCompanyPaymentServiceTable.PaymentServiceNameColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.PaymentServiceNameColumn] = value;
                }
            }
            
            public string PaymentServiceNumber {
                get {
                    try {
                        return ((string)(this[this.tableCompanyPaymentServiceTable.PaymentServiceNumberColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.PaymentServiceNumberColumn] = value;
                }
            }
            
            public bool IsActive {
                get {
                    try {
                        return ((bool)(this[this.tableCompanyPaymentServiceTable.IsActiveColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.IsActiveColumn] = value;
                }
            }
            
            public string Comments {
                get {
                    try {
                        return ((string)(this[this.tableCompanyPaymentServiceTable.CommentsColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.CommentsColumn] = value;
                }
            }
            
            public System.DateTime LastUpdated {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableCompanyPaymentServiceTable.LastUpdatedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.LastUpdatedColumn] = value;
                }
            }
            
            public string UserID {
                get {
                    try {
                        return ((string)(this[this.tableCompanyPaymentServiceTable.UserIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.UserIDColumn] = value;
                }
            }
            
            public string RowVersion {
                get {
                    try {
                        return ((string)(this[this.tableCompanyPaymentServiceTable.RowVersionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCompanyPaymentServiceTable.RowVersionColumn] = value;
                }
            }
            
            public bool IsCompanyIDNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.CompanyIDColumn);
            }
            
            public void SetCompanyIDNull() {
                this[this.tableCompanyPaymentServiceTable.CompanyIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsPaymentServiceIDNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.PaymentServiceIDColumn);
            }
            
            public void SetPaymentServiceIDNull() {
                this[this.tableCompanyPaymentServiceTable.PaymentServiceIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsPaymentServiceNameNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.PaymentServiceNameColumn);
            }
            
            public void SetPaymentServiceNameNull() {
                this[this.tableCompanyPaymentServiceTable.PaymentServiceNameColumn] = System.Convert.DBNull;
            }
            
            public bool IsPaymentServiceNumberNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.PaymentServiceNumberColumn);
            }
            
            public void SetPaymentServiceNumberNull() {
                this[this.tableCompanyPaymentServiceTable.PaymentServiceNumberColumn] = System.Convert.DBNull;
            }
            
            public bool IsIsActiveNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.IsActiveColumn);
            }
            
            public void SetIsActiveNull() {
                this[this.tableCompanyPaymentServiceTable.IsActiveColumn] = System.Convert.DBNull;
            }
            
            public bool IsCommentsNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.CommentsColumn);
            }
            
            public void SetCommentsNull() {
                this[this.tableCompanyPaymentServiceTable.CommentsColumn] = System.Convert.DBNull;
            }
            
            public bool IsLastUpdatedNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.LastUpdatedColumn);
            }
            
            public void SetLastUpdatedNull() {
                this[this.tableCompanyPaymentServiceTable.LastUpdatedColumn] = System.Convert.DBNull;
            }
            
            public bool IsUserIDNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.UserIDColumn);
            }
            
            public void SetUserIDNull() {
                this[this.tableCompanyPaymentServiceTable.UserIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsRowVersionNull() {
                return this.IsNull(this.tableCompanyPaymentServiceTable.RowVersionColumn);
            }
            
            public void SetRowVersionNull() {
                this[this.tableCompanyPaymentServiceTable.RowVersionColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CompanyPaymentServiceTableRowChangeEvent : EventArgs {
            
            private CompanyPaymentServiceTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public CompanyPaymentServiceTableRowChangeEvent(CompanyPaymentServiceTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public CompanyPaymentServiceTableRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
