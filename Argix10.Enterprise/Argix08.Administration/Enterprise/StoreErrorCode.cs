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
    public class StoreErrorCodeDS : DataSet {
        
        private StoreErrorCodeTableDataTable tableStoreErrorCodeTable;
        
        public StoreErrorCodeDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected StoreErrorCodeDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["StoreErrorCodeTable"] != null)) {
                    this.Tables.Add(new StoreErrorCodeTableDataTable(ds.Tables["StoreErrorCodeTable"]));
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
        public StoreErrorCodeTableDataTable StoreErrorCodeTable {
            get {
                return this.tableStoreErrorCodeTable;
            }
        }
        
        public override DataSet Clone() {
            StoreErrorCodeDS cln = ((StoreErrorCodeDS)(base.Clone()));
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
            if ((ds.Tables["StoreErrorCodeTable"] != null)) {
                this.Tables.Add(new StoreErrorCodeTableDataTable(ds.Tables["StoreErrorCodeTable"]));
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
            this.tableStoreErrorCodeTable = ((StoreErrorCodeTableDataTable)(this.Tables["StoreErrorCodeTable"]));
            if ((this.tableStoreErrorCodeTable != null)) {
                this.tableStoreErrorCodeTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "StoreErrorCodeDS";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/StoreErrorCodeDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableStoreErrorCodeTable = new StoreErrorCodeTableDataTable();
            this.Tables.Add(this.tableStoreErrorCodeTable);
        }
        
        private bool ShouldSerializeStoreErrorCodeTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void StoreErrorCodeTableRowChangeEventHandler(object sender, StoreErrorCodeTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class StoreErrorCodeTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnCodeID;
            
            private DataColumn columnCode;
            
            private DataColumn columnDescription;
            
            private DataColumn columnLastUpdated;
            
            private DataColumn columnUserID;
            
            private DataColumn columnRowVersion;
            
            internal StoreErrorCodeTableDataTable() : 
                    base("StoreErrorCodeTable") {
                this.InitClass();
            }
            
            internal StoreErrorCodeTableDataTable(DataTable table) : 
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
            
            internal DataColumn CodeIDColumn {
                get {
                    return this.columnCodeID;
                }
            }
            
            internal DataColumn CodeColumn {
                get {
                    return this.columnCode;
                }
            }
            
            internal DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
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
            
            public StoreErrorCodeTableRow this[int index] {
                get {
                    return ((StoreErrorCodeTableRow)(this.Rows[index]));
                }
            }
            
            public event StoreErrorCodeTableRowChangeEventHandler StoreErrorCodeTableRowChanged;
            
            public event StoreErrorCodeTableRowChangeEventHandler StoreErrorCodeTableRowChanging;
            
            public event StoreErrorCodeTableRowChangeEventHandler StoreErrorCodeTableRowDeleted;
            
            public event StoreErrorCodeTableRowChangeEventHandler StoreErrorCodeTableRowDeleting;
            
            public void AddStoreErrorCodeTableRow(StoreErrorCodeTableRow row) {
                this.Rows.Add(row);
            }
            
            public StoreErrorCodeTableRow AddStoreErrorCodeTableRow(int CodeID, string Code, string Description, System.DateTime LastUpdated, string UserID, string RowVersion) {
                StoreErrorCodeTableRow rowStoreErrorCodeTableRow = ((StoreErrorCodeTableRow)(this.NewRow()));
                rowStoreErrorCodeTableRow.ItemArray = new object[] {
                        CodeID,
                        Code,
                        Description,
                        LastUpdated,
                        UserID,
                        RowVersion};
                this.Rows.Add(rowStoreErrorCodeTableRow);
                return rowStoreErrorCodeTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                StoreErrorCodeTableDataTable cln = ((StoreErrorCodeTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new StoreErrorCodeTableDataTable();
            }
            
            internal void InitVars() {
                this.columnCodeID = this.Columns["CodeID"];
                this.columnCode = this.Columns["Code"];
                this.columnDescription = this.Columns["Description"];
                this.columnLastUpdated = this.Columns["LastUpdated"];
                this.columnUserID = this.Columns["UserID"];
                this.columnRowVersion = this.Columns["RowVersion"];
            }
            
            private void InitClass() {
                this.columnCodeID = new DataColumn("CodeID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCodeID);
                this.columnCode = new DataColumn("Code", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCode);
                this.columnDescription = new DataColumn("Description", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDescription);
                this.columnLastUpdated = new DataColumn("LastUpdated", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLastUpdated);
                this.columnUserID = new DataColumn("UserID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUserID);
                this.columnRowVersion = new DataColumn("RowVersion", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRowVersion);
            }
            
            public StoreErrorCodeTableRow NewStoreErrorCodeTableRow() {
                return ((StoreErrorCodeTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new StoreErrorCodeTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(StoreErrorCodeTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.StoreErrorCodeTableRowChanged != null)) {
                    this.StoreErrorCodeTableRowChanged(this, new StoreErrorCodeTableRowChangeEvent(((StoreErrorCodeTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.StoreErrorCodeTableRowChanging != null)) {
                    this.StoreErrorCodeTableRowChanging(this, new StoreErrorCodeTableRowChangeEvent(((StoreErrorCodeTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.StoreErrorCodeTableRowDeleted != null)) {
                    this.StoreErrorCodeTableRowDeleted(this, new StoreErrorCodeTableRowChangeEvent(((StoreErrorCodeTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.StoreErrorCodeTableRowDeleting != null)) {
                    this.StoreErrorCodeTableRowDeleting(this, new StoreErrorCodeTableRowChangeEvent(((StoreErrorCodeTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveStoreErrorCodeTableRow(StoreErrorCodeTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class StoreErrorCodeTableRow : DataRow {
            
            private StoreErrorCodeTableDataTable tableStoreErrorCodeTable;
            
            internal StoreErrorCodeTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableStoreErrorCodeTable = ((StoreErrorCodeTableDataTable)(this.Table));
            }
            
            public int CodeID {
                get {
                    try {
                        return ((int)(this[this.tableStoreErrorCodeTable.CodeIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.CodeIDColumn] = value;
                }
            }
            
            public string Code {
                get {
                    try {
                        return ((string)(this[this.tableStoreErrorCodeTable.CodeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.CodeColumn] = value;
                }
            }
            
            public string Description {
                get {
                    try {
                        return ((string)(this[this.tableStoreErrorCodeTable.DescriptionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.DescriptionColumn] = value;
                }
            }
            
            public System.DateTime LastUpdated {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableStoreErrorCodeTable.LastUpdatedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.LastUpdatedColumn] = value;
                }
            }
            
            public string UserID {
                get {
                    try {
                        return ((string)(this[this.tableStoreErrorCodeTable.UserIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.UserIDColumn] = value;
                }
            }
            
            public string RowVersion {
                get {
                    try {
                        return ((string)(this[this.tableStoreErrorCodeTable.RowVersionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableStoreErrorCodeTable.RowVersionColumn] = value;
                }
            }
            
            public bool IsCodeIDNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.CodeIDColumn);
            }
            
            public void SetCodeIDNull() {
                this[this.tableStoreErrorCodeTable.CodeIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsCodeNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.CodeColumn);
            }
            
            public void SetCodeNull() {
                this[this.tableStoreErrorCodeTable.CodeColumn] = System.Convert.DBNull;
            }
            
            public bool IsDescriptionNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.DescriptionColumn);
            }
            
            public void SetDescriptionNull() {
                this[this.tableStoreErrorCodeTable.DescriptionColumn] = System.Convert.DBNull;
            }
            
            public bool IsLastUpdatedNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.LastUpdatedColumn);
            }
            
            public void SetLastUpdatedNull() {
                this[this.tableStoreErrorCodeTable.LastUpdatedColumn] = System.Convert.DBNull;
            }
            
            public bool IsUserIDNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.UserIDColumn);
            }
            
            public void SetUserIDNull() {
                this[this.tableStoreErrorCodeTable.UserIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsRowVersionNull() {
                return this.IsNull(this.tableStoreErrorCodeTable.RowVersionColumn);
            }
            
            public void SetRowVersionNull() {
                this[this.tableStoreErrorCodeTable.RowVersionColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class StoreErrorCodeTableRowChangeEvent : EventArgs {
            
            private StoreErrorCodeTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public StoreErrorCodeTableRowChangeEvent(StoreErrorCodeTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public StoreErrorCodeTableRow Row {
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