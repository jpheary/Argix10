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
    public class CountryDS : DataSet {
        
        private CountryDetailTableDataTable tableCountryDetailTable;
        
        public CountryDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected CountryDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["CountryDetailTable"] != null)) {
                    this.Tables.Add(new CountryDetailTableDataTable(ds.Tables["CountryDetailTable"]));
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
        public CountryDetailTableDataTable CountryDetailTable {
            get {
                return this.tableCountryDetailTable;
            }
        }
        
        public override DataSet Clone() {
            CountryDS cln = ((CountryDS)(base.Clone()));
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
            if ((ds.Tables["CountryDetailTable"] != null)) {
                this.Tables.Add(new CountryDetailTableDataTable(ds.Tables["CountryDetailTable"]));
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
            this.tableCountryDetailTable = ((CountryDetailTableDataTable)(this.Tables["CountryDetailTable"]));
            if ((this.tableCountryDetailTable != null)) {
                this.tableCountryDetailTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "CountryDS";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/CountryDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableCountryDetailTable = new CountryDetailTableDataTable();
            this.Tables.Add(this.tableCountryDetailTable);
        }
        
        private bool ShouldSerializeCountryDetailTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void CountryDetailTableRowChangeEventHandler(object sender, CountryDetailTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CountryDetailTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnCountryID;
            
            private DataColumn columnCountry;
            
            private DataColumn columnLastUpdated;
            
            private DataColumn columnUserID;
            
            private DataColumn columnRowVersionID;
            
            internal CountryDetailTableDataTable() : 
                    base("CountryDetailTable") {
                this.InitClass();
            }
            
            internal CountryDetailTableDataTable(DataTable table) : 
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
            
            internal DataColumn CountryIDColumn {
                get {
                    return this.columnCountryID;
                }
            }
            
            internal DataColumn CountryColumn {
                get {
                    return this.columnCountry;
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
            
            internal DataColumn RowVersionIDColumn {
                get {
                    return this.columnRowVersionID;
                }
            }
            
            public CountryDetailTableRow this[int index] {
                get {
                    return ((CountryDetailTableRow)(this.Rows[index]));
                }
            }
            
            public event CountryDetailTableRowChangeEventHandler CountryDetailTableRowChanged;
            
            public event CountryDetailTableRowChangeEventHandler CountryDetailTableRowChanging;
            
            public event CountryDetailTableRowChangeEventHandler CountryDetailTableRowDeleted;
            
            public event CountryDetailTableRowChangeEventHandler CountryDetailTableRowDeleting;
            
            public void AddCountryDetailTableRow(CountryDetailTableRow row) {
                this.Rows.Add(row);
            }
            
            public CountryDetailTableRow AddCountryDetailTableRow(int CountryID, string Country, System.DateTime LastUpdated, string UserID, System.Byte[] RowVersionID) {
                CountryDetailTableRow rowCountryDetailTableRow = ((CountryDetailTableRow)(this.NewRow()));
                rowCountryDetailTableRow.ItemArray = new object[] {
                        CountryID,
                        Country,
                        LastUpdated,
                        UserID,
                        RowVersionID};
                this.Rows.Add(rowCountryDetailTableRow);
                return rowCountryDetailTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                CountryDetailTableDataTable cln = ((CountryDetailTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new CountryDetailTableDataTable();
            }
            
            internal void InitVars() {
                this.columnCountryID = this.Columns["CountryID"];
                this.columnCountry = this.Columns["Country"];
                this.columnLastUpdated = this.Columns["LastUpdated"];
                this.columnUserID = this.Columns["UserID"];
                this.columnRowVersionID = this.Columns["RowVersionID"];
            }
            
            private void InitClass() {
                this.columnCountryID = new DataColumn("CountryID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCountryID);
                this.columnCountry = new DataColumn("Country", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCountry);
                this.columnLastUpdated = new DataColumn("LastUpdated", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLastUpdated);
                this.columnUserID = new DataColumn("UserID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUserID);
                this.columnRowVersionID = new DataColumn("RowVersionID", typeof(System.Byte[]), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRowVersionID);
                this.columnCountryID.AllowDBNull = false;
                this.columnCountry.AllowDBNull = false;
            }
            
            public CountryDetailTableRow NewCountryDetailTableRow() {
                return ((CountryDetailTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new CountryDetailTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(CountryDetailTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.CountryDetailTableRowChanged != null)) {
                    this.CountryDetailTableRowChanged(this, new CountryDetailTableRowChangeEvent(((CountryDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.CountryDetailTableRowChanging != null)) {
                    this.CountryDetailTableRowChanging(this, new CountryDetailTableRowChangeEvent(((CountryDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.CountryDetailTableRowDeleted != null)) {
                    this.CountryDetailTableRowDeleted(this, new CountryDetailTableRowChangeEvent(((CountryDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.CountryDetailTableRowDeleting != null)) {
                    this.CountryDetailTableRowDeleting(this, new CountryDetailTableRowChangeEvent(((CountryDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveCountryDetailTableRow(CountryDetailTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CountryDetailTableRow : DataRow {
            
            private CountryDetailTableDataTable tableCountryDetailTable;
            
            internal CountryDetailTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableCountryDetailTable = ((CountryDetailTableDataTable)(this.Table));
            }
            
            public int CountryID {
                get {
                    return ((int)(this[this.tableCountryDetailTable.CountryIDColumn]));
                }
                set {
                    this[this.tableCountryDetailTable.CountryIDColumn] = value;
                }
            }
            
            public string Country {
                get {
                    return ((string)(this[this.tableCountryDetailTable.CountryColumn]));
                }
                set {
                    this[this.tableCountryDetailTable.CountryColumn] = value;
                }
            }
            
            public System.DateTime LastUpdated {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableCountryDetailTable.LastUpdatedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCountryDetailTable.LastUpdatedColumn] = value;
                }
            }
            
            public string UserID {
                get {
                    try {
                        return ((string)(this[this.tableCountryDetailTable.UserIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCountryDetailTable.UserIDColumn] = value;
                }
            }
            
            public System.Byte[] RowVersionID {
                get {
                    try {
                        return ((System.Byte[])(this[this.tableCountryDetailTable.RowVersionIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableCountryDetailTable.RowVersionIDColumn] = value;
                }
            }
            
            public bool IsLastUpdatedNull() {
                return this.IsNull(this.tableCountryDetailTable.LastUpdatedColumn);
            }
            
            public void SetLastUpdatedNull() {
                this[this.tableCountryDetailTable.LastUpdatedColumn] = System.Convert.DBNull;
            }
            
            public bool IsUserIDNull() {
                return this.IsNull(this.tableCountryDetailTable.UserIDColumn);
            }
            
            public void SetUserIDNull() {
                this[this.tableCountryDetailTable.UserIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsRowVersionIDNull() {
                return this.IsNull(this.tableCountryDetailTable.RowVersionIDColumn);
            }
            
            public void SetRowVersionIDNull() {
                this[this.tableCountryDetailTable.RowVersionIDColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class CountryDetailTableRowChangeEvent : EventArgs {
            
            private CountryDetailTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public CountryDetailTableRowChangeEvent(CountryDetailTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public CountryDetailTableRow Row {
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