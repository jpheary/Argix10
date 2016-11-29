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
    public class TerminalLaneDS : DataSet {
        
        private TerminalLaneTableDataTable tableTerminalLaneTable;
        
        public TerminalLaneDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected TerminalLaneDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["TerminalLaneTable"] != null)) {
                    this.Tables.Add(new TerminalLaneTableDataTable(ds.Tables["TerminalLaneTable"]));
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
        public TerminalLaneTableDataTable TerminalLaneTable {
            get {
                return this.tableTerminalLaneTable;
            }
        }
        
        public override DataSet Clone() {
            TerminalLaneDS cln = ((TerminalLaneDS)(base.Clone()));
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
            if ((ds.Tables["TerminalLaneTable"] != null)) {
                this.Tables.Add(new TerminalLaneTableDataTable(ds.Tables["TerminalLaneTable"]));
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
            this.tableTerminalLaneTable = ((TerminalLaneTableDataTable)(this.Tables["TerminalLaneTable"]));
            if ((this.tableTerminalLaneTable != null)) {
                this.tableTerminalLaneTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "TerminalLaneDS";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/TerminalLaneDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableTerminalLaneTable = new TerminalLaneTableDataTable();
            this.Tables.Add(this.tableTerminalLaneTable);
        }
        
        private bool ShouldSerializeTerminalLaneTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void TerminalLaneTableRowChangeEventHandler(object sender, TerminalLaneTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TerminalLaneTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnLaneID;
            
            private DataColumn columnLaneNumber;
            
            private DataColumn columnLaneType;
            
            private DataColumn columnDescription;
            
            private DataColumn columnTerminalID;
            
            private DataColumn columnTerminal;
            
            private DataColumn columnLastUpdated;
            
            private DataColumn columnUserID;
            
            private DataColumn columnRowVersion;
            
            internal TerminalLaneTableDataTable() : 
                    base("TerminalLaneTable") {
                this.InitClass();
            }
            
            internal TerminalLaneTableDataTable(DataTable table) : 
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
            
            internal DataColumn LaneIDColumn {
                get {
                    return this.columnLaneID;
                }
            }
            
            internal DataColumn LaneNumberColumn {
                get {
                    return this.columnLaneNumber;
                }
            }
            
            internal DataColumn LaneTypeColumn {
                get {
                    return this.columnLaneType;
                }
            }
            
            internal DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
                }
            }
            
            internal DataColumn TerminalIDColumn {
                get {
                    return this.columnTerminalID;
                }
            }
            
            internal DataColumn TerminalColumn {
                get {
                    return this.columnTerminal;
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
            
            public TerminalLaneTableRow this[int index] {
                get {
                    return ((TerminalLaneTableRow)(this.Rows[index]));
                }
            }
            
            public event TerminalLaneTableRowChangeEventHandler TerminalLaneTableRowChanged;
            
            public event TerminalLaneTableRowChangeEventHandler TerminalLaneTableRowChanging;
            
            public event TerminalLaneTableRowChangeEventHandler TerminalLaneTableRowDeleted;
            
            public event TerminalLaneTableRowChangeEventHandler TerminalLaneTableRowDeleting;
            
            public void AddTerminalLaneTableRow(TerminalLaneTableRow row) {
                this.Rows.Add(row);
            }
            
            public TerminalLaneTableRow AddTerminalLaneTableRow(string LaneID, string LaneNumber, string LaneType, string Description, int TerminalID, string Terminal, System.DateTime LastUpdated, string UserID, string RowVersion) {
                TerminalLaneTableRow rowTerminalLaneTableRow = ((TerminalLaneTableRow)(this.NewRow()));
                rowTerminalLaneTableRow.ItemArray = new object[] {
                        LaneID,
                        LaneNumber,
                        LaneType,
                        Description,
                        TerminalID,
                        Terminal,
                        LastUpdated,
                        UserID,
                        RowVersion};
                this.Rows.Add(rowTerminalLaneTableRow);
                return rowTerminalLaneTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                TerminalLaneTableDataTable cln = ((TerminalLaneTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new TerminalLaneTableDataTable();
            }
            
            internal void InitVars() {
                this.columnLaneID = this.Columns["LaneID"];
                this.columnLaneNumber = this.Columns["LaneNumber"];
                this.columnLaneType = this.Columns["LaneType"];
                this.columnDescription = this.Columns["Description"];
                this.columnTerminalID = this.Columns["TerminalID"];
                this.columnTerminal = this.Columns["Terminal"];
                this.columnLastUpdated = this.Columns["LastUpdated"];
                this.columnUserID = this.Columns["UserID"];
                this.columnRowVersion = this.Columns["RowVersion"];
            }
            
            private void InitClass() {
                this.columnLaneID = new DataColumn("LaneID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLaneID);
                this.columnLaneNumber = new DataColumn("LaneNumber", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLaneNumber);
                this.columnLaneType = new DataColumn("LaneType", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLaneType);
                this.columnDescription = new DataColumn("Description", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDescription);
                this.columnTerminalID = new DataColumn("TerminalID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTerminalID);
                this.columnTerminal = new DataColumn("Terminal", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTerminal);
                this.columnLastUpdated = new DataColumn("LastUpdated", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLastUpdated);
                this.columnUserID = new DataColumn("UserID", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUserID);
                this.columnRowVersion = new DataColumn("RowVersion", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRowVersion);
            }
            
            public TerminalLaneTableRow NewTerminalLaneTableRow() {
                return ((TerminalLaneTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new TerminalLaneTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(TerminalLaneTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TerminalLaneTableRowChanged != null)) {
                    this.TerminalLaneTableRowChanged(this, new TerminalLaneTableRowChangeEvent(((TerminalLaneTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TerminalLaneTableRowChanging != null)) {
                    this.TerminalLaneTableRowChanging(this, new TerminalLaneTableRowChangeEvent(((TerminalLaneTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TerminalLaneTableRowDeleted != null)) {
                    this.TerminalLaneTableRowDeleted(this, new TerminalLaneTableRowChangeEvent(((TerminalLaneTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TerminalLaneTableRowDeleting != null)) {
                    this.TerminalLaneTableRowDeleting(this, new TerminalLaneTableRowChangeEvent(((TerminalLaneTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveTerminalLaneTableRow(TerminalLaneTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TerminalLaneTableRow : DataRow {
            
            private TerminalLaneTableDataTable tableTerminalLaneTable;
            
            internal TerminalLaneTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableTerminalLaneTable = ((TerminalLaneTableDataTable)(this.Table));
            }
            
            public string LaneID {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.LaneIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.LaneIDColumn] = value;
                }
            }
            
            public string LaneNumber {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.LaneNumberColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.LaneNumberColumn] = value;
                }
            }
            
            public string LaneType {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.LaneTypeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.LaneTypeColumn] = value;
                }
            }
            
            public string Description {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.DescriptionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.DescriptionColumn] = value;
                }
            }
            
            public int TerminalID {
                get {
                    try {
                        return ((int)(this[this.tableTerminalLaneTable.TerminalIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.TerminalIDColumn] = value;
                }
            }
            
            public string Terminal {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.TerminalColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.TerminalColumn] = value;
                }
            }
            
            public System.DateTime LastUpdated {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableTerminalLaneTable.LastUpdatedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.LastUpdatedColumn] = value;
                }
            }
            
            public string UserID {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.UserIDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.UserIDColumn] = value;
                }
            }
            
            public string RowVersion {
                get {
                    try {
                        return ((string)(this[this.tableTerminalLaneTable.RowVersionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTerminalLaneTable.RowVersionColumn] = value;
                }
            }
            
            public bool IsLaneIDNull() {
                return this.IsNull(this.tableTerminalLaneTable.LaneIDColumn);
            }
            
            public void SetLaneIDNull() {
                this[this.tableTerminalLaneTable.LaneIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsLaneNumberNull() {
                return this.IsNull(this.tableTerminalLaneTable.LaneNumberColumn);
            }
            
            public void SetLaneNumberNull() {
                this[this.tableTerminalLaneTable.LaneNumberColumn] = System.Convert.DBNull;
            }
            
            public bool IsLaneTypeNull() {
                return this.IsNull(this.tableTerminalLaneTable.LaneTypeColumn);
            }
            
            public void SetLaneTypeNull() {
                this[this.tableTerminalLaneTable.LaneTypeColumn] = System.Convert.DBNull;
            }
            
            public bool IsDescriptionNull() {
                return this.IsNull(this.tableTerminalLaneTable.DescriptionColumn);
            }
            
            public void SetDescriptionNull() {
                this[this.tableTerminalLaneTable.DescriptionColumn] = System.Convert.DBNull;
            }
            
            public bool IsTerminalIDNull() {
                return this.IsNull(this.tableTerminalLaneTable.TerminalIDColumn);
            }
            
            public void SetTerminalIDNull() {
                this[this.tableTerminalLaneTable.TerminalIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsTerminalNull() {
                return this.IsNull(this.tableTerminalLaneTable.TerminalColumn);
            }
            
            public void SetTerminalNull() {
                this[this.tableTerminalLaneTable.TerminalColumn] = System.Convert.DBNull;
            }
            
            public bool IsLastUpdatedNull() {
                return this.IsNull(this.tableTerminalLaneTable.LastUpdatedColumn);
            }
            
            public void SetLastUpdatedNull() {
                this[this.tableTerminalLaneTable.LastUpdatedColumn] = System.Convert.DBNull;
            }
            
            public bool IsUserIDNull() {
                return this.IsNull(this.tableTerminalLaneTable.UserIDColumn);
            }
            
            public void SetUserIDNull() {
                this[this.tableTerminalLaneTable.UserIDColumn] = System.Convert.DBNull;
            }
            
            public bool IsRowVersionNull() {
                return this.IsNull(this.tableTerminalLaneTable.RowVersionColumn);
            }
            
            public void SetRowVersionNull() {
                this[this.tableTerminalLaneTable.RowVersionColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TerminalLaneTableRowChangeEvent : EventArgs {
            
            private TerminalLaneTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public TerminalLaneTableRowChangeEvent(TerminalLaneTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public TerminalLaneTableRow Row {
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