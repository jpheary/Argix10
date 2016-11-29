namespace Argix.Customers {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"CRMIssue", @"CustomersFault"})]
    public sealed class CRMSystemService_schemas_datacontract_org_2004_07_Argix_Customers : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:tns=""http://schemas.datacontract.org/2004/07/Argix.Customers"" elementFormDefault=""qualified"" targetNamespace=""http://schemas.datacontract.org/2004/07/Argix.Customers"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:complexType name=""CRMIssue"">
    <xs:sequence>
      <xs:element minOccurs=""0"" name=""ActionTypeID"" type=""xs:unsignedByte"" />
      <xs:element minOccurs=""0"" name=""AgentNumber"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""Comment"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""CompanyID"" type=""xs:int"" />
      <xs:element minOccurs=""0"" name=""CompanyName"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""Contact"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""DistrictNumber"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""RegionNumber"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""StoreNumber"" type=""xs:int"" />
      <xs:element minOccurs=""0"" name=""Subject"" nillable=""true"" type=""xs:string"" />
      <xs:element minOccurs=""0"" name=""TypeID"" type=""xs:int"" />
      <xs:element minOccurs=""0"" name=""UserID"" nillable=""true"" type=""xs:string"" />
    </xs:sequence>
  </xs:complexType>
  <xs:element name=""CRMIssue"" nillable=""true"" type=""tns:CRMIssue"" />
  <xs:complexType name=""CustomersFault"">
    <xs:sequence>
      <xs:element minOccurs=""0"" name=""Message"" nillable=""true"" type=""xs:string"" />
    </xs:sequence>
  </xs:complexType>
  <xs:element name=""CustomersFault"" nillable=""true"" type=""tns:CustomersFault"" />
</xs:schema>";
        
        public CRMSystemService_schemas_datacontract_org_2004_07_Argix_Customers() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [2];
                _RootElements[0] = "CRMIssue";
                _RootElements[1] = "CustomersFault";
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
        
        [Schema(@"http://schemas.datacontract.org/2004/07/Argix.Customers",@"CRMIssue")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"CRMIssue"})]
        public sealed class CRMIssue : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public CRMIssue() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "CRMIssue";
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
        
        [Schema(@"http://schemas.datacontract.org/2004/07/Argix.Customers",@"CustomersFault")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"CustomersFault"})]
        public sealed class CustomersFault : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public CustomersFault() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "CustomersFault";
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
