namespace Argix.Freight {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "SchedulePickupRequestResult", XPath = @"/*[local-name()='SchedulePickupRequestResponse' and namespace-uri()='http://Argix.Freight']/*[local-name()='SchedulePickupRequestResult' and namespace-uri()='http://Argix.Freight']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.PickupID", XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::Argix.PickupID), XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.ID", XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.ID", XPath = @"/*[local-name()='ArriveShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"SchedulePickupRequest", @"SchedulePickupRequestResponse", @"DispatchShipment", @"DispatchShipmentResponse", @"ArriveShipment", @"ArriveShipmentResponse"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSystemService_schemas_datacontract_org_2004_07_Argix_Freight", typeof(global::Argix.Freight.FreightSystemService_schemas_datacontract_org_2004_07_Argix_Freight))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.FreightProperties", typeof(global::Argix.FreightProperties))]
    public sealed class FreightSystemService_argix_freight : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:tns=""http://Argix.Freight"" xmlns:ns0=""http://Argix.Freight.Properties"" elementFormDefault=""qualified"" targetNamespace=""http://Argix.Freight"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""Argix.Freight.FreightSystemService_schemas_datacontract_org_2004_07_Argix_Freight"" namespace=""http://schemas.datacontract.org/2004/07/Argix.Freight"" />
  <xs:annotation>
    <xs:appinfo>
      <b:references>
        <b:reference targetNamespace=""http://schemas.datacontract.org/2004/07/Argix.Freight"" />
      </b:references>
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""http://Argix.Freight.Properties"" location=""Argix.FreightProperties"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""SchedulePickupRequest"">
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q1=""http://schemas.datacontract.org/2004/07/Argix.Freight"" minOccurs=""0"" name=""request"" nillable=""true"" type=""q1:PickupRequest"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""SchedulePickupRequestResponse"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='SchedulePickupRequestResponse' and namespace-uri()='http://Argix.Freight']/*[local-name()='SchedulePickupRequestResult' and namespace-uri()='http://Argix.Freight']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" name=""SchedulePickupRequestResult"" type=""xs:int"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""DispatchShipment"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']"" />
          <b:property name=""ns0:PickupID"" xpath=""/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q2=""http://schemas.datacontract.org/2004/07/Argix.Freight"" minOccurs=""0"" name=""shipment"" nillable=""true"" type=""q2:LTLShipment"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""DispatchShipmentResponse"">
    <xs:complexType>
      <xs:sequence />
    </xs:complexType>
  </xs:element>
  <xs:element name=""ArriveShipment"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='ArriveShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q3=""http://schemas.datacontract.org/2004/07/Argix.Freight"" minOccurs=""0"" name=""shipment"" nillable=""true"" type=""q3:LTLShipment"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""ArriveShipmentResponse"">
    <xs:complexType>
      <xs:sequence />
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public FreightSystemService_argix_freight() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [6];
                _RootElements[0] = "SchedulePickupRequest";
                _RootElements[1] = "SchedulePickupRequestResponse";
                _RootElements[2] = "DispatchShipment";
                _RootElements[3] = "DispatchShipmentResponse";
                _RootElements[4] = "ArriveShipment";
                _RootElements[5] = "ArriveShipmentResponse";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
        
        [Schema(@"http://Argix.Freight",@"SchedulePickupRequest")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"SchedulePickupRequest"})]
        public sealed class SchedulePickupRequest : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public SchedulePickupRequest() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "SchedulePickupRequest";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://Argix.Freight",@"SchedulePickupRequestResponse")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "SchedulePickupRequestResult", XPath = @"/*[local-name()='SchedulePickupRequestResponse' and namespace-uri()='http://Argix.Freight']/*[local-name()='SchedulePickupRequestResult' and namespace-uri()='http://Argix.Freight']", XsdType = @"int")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"SchedulePickupRequestResponse"})]
        public sealed class SchedulePickupRequestResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public SchedulePickupRequestResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "SchedulePickupRequestResponse";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://Argix.Freight",@"DispatchShipment")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.PickupID", XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
        [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::Argix.PickupID), XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='PickupID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.ID", XPath = @"/*[local-name()='DispatchShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"DispatchShipment"})]
        public sealed class DispatchShipment : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public DispatchShipment() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "DispatchShipment";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://Argix.Freight",@"DispatchShipmentResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"DispatchShipmentResponse"})]
        public sealed class DispatchShipmentResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public DispatchShipmentResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "DispatchShipmentResponse";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://Argix.Freight",@"ArriveShipment")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "shipment.ID", XPath = @"/*[local-name()='ArriveShipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='shipment' and namespace-uri()='http://Argix.Freight']/*[local-name()='ID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Freight']", XsdType = @"int")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"ArriveShipment"})]
        public sealed class ArriveShipment : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public ArriveShipment() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "ArriveShipment";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
        
        [Schema(@"http://Argix.Freight",@"ArriveShipmentResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"ArriveShipmentResponse"})]
        public sealed class ArriveShipmentResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public ArriveShipmentResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "ArriveShipmentResponse";
                    return _RootElements;
                }
            }
            
            protected override object RawSchema {
                get {
                    return _rawSchema;
                }
                set {
                    _rawSchema = value;
                }
            }
        }
    }
}
