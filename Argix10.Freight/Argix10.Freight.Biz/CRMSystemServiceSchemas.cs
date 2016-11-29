namespace Argix.Customers {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Byte), "issue.ActionTypeID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='ActionTypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"unsignedByte")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.AgentNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='AgentNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Comment", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Comment' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.CompanyID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.CompanyName", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyName' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Contact", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Contact' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.DistrictNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='DistrictNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.RegionNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='RegionNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.StoreNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='StoreNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Subject", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Subject' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.TypeID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='TypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.UserID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='UserID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"CreateIssue", @"CreateIssueResponse"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Customers.CRMSystemServiceSchemas", typeof(global::Argix.Customers.CRMSystemServiceSchema))]
    public sealed class CRMSystemService_argix_customers : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:tns=""http://schemas.datacontract.org/2004/07/Argix.Customers"" elementFormDefault=""qualified"" targetNamespace=""http://schemas.datacontract.org/2004/07/Argix.Customers"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""Argix.Customers.CRMSystemServiceSchema"" namespace=""http://schemas.datacontract.org/2004/07/Argix.Customers"" />
  <xs:annotation>
    <xs:appinfo>
      <b:references>
        <b:reference targetNamespace=""http://schemas.datacontract.org/2004/07/Argix.Customers"" />
      </b:references>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""CreateIssue"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='ActionTypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='AgentNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Comment' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyName' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Contact' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='DistrictNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='RegionNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='StoreNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Subject' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='TypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='UserID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q1=""http://schemas.datacontract.org/2004/07/Argix.Customers"" minOccurs=""0"" name=""issue"" nillable=""true"" type=""q1:CRMIssue"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""CreateIssueResponse"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" name=""CreateIssueResult"" type=""xs:long"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public CRMSystemService_argix_customers() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [2];
                _RootElements[0] = "CreateIssue";
                _RootElements[1] = "CreateIssueResponse";
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
        
        [Schema(@"http://schemas.datacontract.org/2004/07/Argix.Customers",@"CreateIssue")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Byte), "issue.ActionTypeID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='ActionTypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"unsignedByte")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.AgentNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='AgentNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Comment", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Comment' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.CompanyID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.CompanyName", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='CompanyName' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Contact", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Contact' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.DistrictNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='DistrictNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.RegionNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='RegionNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.StoreNumber", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='StoreNumber' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.Subject", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='Subject' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Int32), "issue.TypeID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='TypeID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"int")]
        [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "issue.UserID", XPath = @"/*[local-name()='CreateIssue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='issue' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']/*[local-name()='UserID' and namespace-uri()='http://schemas.datacontract.org/2004/07/Argix.Customers']", XsdType = @"string")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"CreateIssue"})]
        public sealed class CreateIssue : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public CreateIssue() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "CreateIssue";
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
        
        [Schema(@"http://schemas.datacontract.org/2004/07/Argix.Customers",@"CreateIssueResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"CreateIssueResponse"})]
        public sealed class CreateIssueResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public CreateIssueResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "CreateIssueResponse";
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
