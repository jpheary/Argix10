namespace Argix {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSchemas+LTLShipment", typeof(global::Argix.Freight.FreightSchemas.LTLShipment))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.CRMSystemServiceSchemas+CreateIssue", typeof(global::Argix.CRMSystemServiceSchemas.CreateIssue))]
    public sealed class LTLShipmentToCRMIssueMap : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0"" version=""1.0"" xmlns:s0=""http://schemas.datacontract.org/2004/07/Argix.Freight"" xmlns:ns1=""http://schemas.datacontract.org/2004/07/Argix.Customers"" xmlns:ns0=""http://Argix.Customers"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:LTLShipment"" />
  </xsl:template>
  <xsl:template match=""/s0:LTLShipment"">
    <ns0:CreateIssue>
      <ns0:issue>
        <ns1:ActionTypeID>
          <xsl:text>0</xsl:text>
        </ns1:ActionTypeID>
        <ns1:AgentNumber>
          <xsl:text>0001</xsl:text>
        </ns1:AgentNumber>
        <ns1:Comment>
          <xsl:text>Comment</xsl:text>
        </ns1:Comment>
        <ns1:CompanyID>
          <xsl:text>0</xsl:text>
        </ns1:CompanyID>
        <ns1:CompanyName>
          <xsl:text>CompanyName</xsl:text>
        </ns1:CompanyName>
        <ns1:Contact>
          <xsl:text>Contact</xsl:text>
        </ns1:Contact>
        <ns1:DistrictNumber>
          <xsl:text>0</xsl:text>
        </ns1:DistrictNumber>
        <ns1:RegionNumber>
          <xsl:text>0</xsl:text>
        </ns1:RegionNumber>
        <ns1:StoreNumber>
          <xsl:text>0</xsl:text>
        </ns1:StoreNumber>
        <ns1:Subject>
          <xsl:text>LTL Pickup Not Arrived</xsl:text>
        </ns1:Subject>
        <ns1:TypeID>
          <xsl:text>0</xsl:text>
        </ns1:TypeID>
        <xsl:if test=""s0:UserID"">
          <xsl:variable name=""var:v1"" select=""string(s0:UserID/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v1)='true'"">
            <ns1:UserID>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:UserID>
          </xsl:if>
          <xsl:if test=""string($var:v1)='false'"">
            <ns1:UserID>
              <xsl:value-of select=""s0:UserID/text()"" />
            </ns1:UserID>
          </xsl:if>
        </xsl:if>
      </ns0:issue>
    </ns0:CreateIssue>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Argix.Freight.FreightSchemas+LTLShipment";
        
        private const string _strTrgSchemasList0 = @"Argix.CRMSystemServiceSchemas+CreateIssue";
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"Argix.Freight.FreightSchemas+LTLShipment";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"Argix.CRMSystemServiceSchemas+CreateIssue";
                return _TrgSchemas;
            }
        }
    }
}
