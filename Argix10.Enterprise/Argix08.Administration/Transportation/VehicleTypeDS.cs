﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Tsort.Transportation {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class VehicleTypeDS : DataSet {
        
        private VehicleTypeListTableDataTable tableVehicleTypeListTable;
        
        public VehicleTypeDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected VehicleTypeDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["VehicleTypeListTable"] != null)) {
                    this.Tables.Add(new VehicleTypeListTableDataTable(ds.Tables["VehicleTypeListTable"]));
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
        public VehicleTypeListTableDataTable VehicleTypeListTable {
            get {
                return this.tableVehicleTypeListTable;
            }
        }
        
        public override DataSet Clone() {
            VehicleTypeDS cln = ((VehicleTypeDS)(base.Clone()));
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
            if ((ds.Tables["VehicleTypeListTable"] != null)) {
                this.Tables.Add(new VehicleTypeListTableDataTable(ds.Tables["VehicleTypeListTable"]));
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
            this.tableVehicleTypeListTable = ((VehicleTypeListTableDataTable)(this.Tables["VehicleTypeListTable"]));
            if ((this.tableVehicleTypeListTable != null)) {
                this.tableVehicleTypeListTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "VehicleTypeDS";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/VehicleTypeDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableVehicleTypeListTable = new VehicleTypeListTableDataTable();
            this.Tables.Add(this.tableVehicleTypeListTable);
        }
        
        private bool ShouldSerializeVehicleTypeListTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void VehicleTypeListTableRowChangeEventHandler(object sender, VehicleTypeListTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VehicleTypeListTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnVehicleType;
            
            internal VehicleTypeListTableDataTable() : 
                    base("VehicleTypeListTable") {
                this.InitClass();
            }
            
            internal VehicleTypeListTableDataTable(DataTable table) : 
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
            
            internal DataColumn VehicleTypeColumn {
                get {
                    return this.columnVehicleType;
                }
            }
            
            public VehicleTypeListTableRow this[int index] {
                get {
                    return ((VehicleTypeListTableRow)(this.Rows[index]));
                }
            }
            
            public event VehicleTypeListTableRowChangeEventHandler VehicleTypeListTableRowChanged;
            
            public event VehicleTypeListTableRowChangeEventHandler VehicleTypeListTableRowChanging;
            
            public event VehicleTypeListTableRowChangeEventHandler VehicleTypeListTableRowDeleted;
            
            public event VehicleTypeListTableRowChangeEventHandler VehicleTypeListTableRowDeleting;
            
            public void AddVehicleTypeListTableRow(VehicleTypeListTableRow row) {
                this.Rows.Add(row);
            }
            
            public VehicleTypeListTableRow AddVehicleTypeListTableRow(string VehicleType) {
                VehicleTypeListTableRow rowVehicleTypeListTableRow = ((VehicleTypeListTableRow)(this.NewRow()));
                rowVehicleTypeListTableRow.ItemArray = new object[] {
                        VehicleType};
                this.Rows.Add(rowVehicleTypeListTableRow);
                return rowVehicleTypeListTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                VehicleTypeListTableDataTable cln = ((VehicleTypeListTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new VehicleTypeListTableDataTable();
            }
            
            internal void InitVars() {
                this.columnVehicleType = this.Columns["VehicleType"];
            }
            
            private void InitClass() {
                this.columnVehicleType = new DataColumn("VehicleType", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnVehicleType);
                this.columnVehicleType.AllowDBNull = false;
            }
            
            public VehicleTypeListTableRow NewVehicleTypeListTableRow() {
                return ((VehicleTypeListTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new VehicleTypeListTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(VehicleTypeListTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.VehicleTypeListTableRowChanged != null)) {
                    this.VehicleTypeListTableRowChanged(this, new VehicleTypeListTableRowChangeEvent(((VehicleTypeListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.VehicleTypeListTableRowChanging != null)) {
                    this.VehicleTypeListTableRowChanging(this, new VehicleTypeListTableRowChangeEvent(((VehicleTypeListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.VehicleTypeListTableRowDeleted != null)) {
                    this.VehicleTypeListTableRowDeleted(this, new VehicleTypeListTableRowChangeEvent(((VehicleTypeListTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.VehicleTypeListTableRowDeleting != null)) {
                    this.VehicleTypeListTableRowDeleting(this, new VehicleTypeListTableRowChangeEvent(((VehicleTypeListTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveVehicleTypeListTableRow(VehicleTypeListTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VehicleTypeListTableRow : DataRow {
            
            private VehicleTypeListTableDataTable tableVehicleTypeListTable;
            
            internal VehicleTypeListTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableVehicleTypeListTable = ((VehicleTypeListTableDataTable)(this.Table));
            }
            
            public string VehicleType {
                get {
                    return ((string)(this[this.tableVehicleTypeListTable.VehicleTypeColumn]));
                }
                set {
                    this[this.tableVehicleTypeListTable.VehicleTypeColumn] = value;
                }
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VehicleTypeListTableRowChangeEvent : EventArgs {
            
            private VehicleTypeListTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public VehicleTypeListTableRowChangeEvent(VehicleTypeListTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public VehicleTypeListTableRow Row {
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
