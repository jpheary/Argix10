namespace Argix {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSchemas+PickupRequest", typeof(global::Argix.Freight.FreightSchemas.PickupRequest))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSystemServiceSchemas+ArriveShipment", typeof(global::Argix.Freight.FreightSystemServiceSchemas.ArriveShipment))]
    public sealed class PickupRequestToLTLShipmentMap : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var"" version=""1.0"" xmlns:ns1=""http://schemas.datacontract.org/2004/07/Argix.Freight"" xmlns:ns0=""http://Argix.Freight"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/ns1:PickupRequest"" />
  </xsl:template>
  <xsl:template match=""/ns1:PickupRequest"">
    <ns0:ArriveShipment>
      <ns0:shipment>
        <ns1:ID>
          <xsl:text>0</xsl:text>
        </ns1:ID>
        <xsl:if test=""ns1:ActualPickup"">
          <ns1:PickupDate>
            <xsl:value-of select=""ns1:ActualPickup/text()"" />
          </ns1:PickupDate>
        </xsl:if>
        <xsl:if test=""ns1:RequestID"">
          <ns1:PickupID>
            <xsl:value-of select=""ns1:RequestID/text()"" />
          </ns1:PickupID>
        </xsl:if>
      </ns0:shipment>
    </ns0:ArriveShipment>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Argix.Freight.FreightSchemas+PickupRequest";
        
        private const string _strTrgSchemasList0 = @"Argix.Freight.FreightSystemServiceSchemas+ArriveShipment";
        
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
                _SrcSchemas[0] = @"Argix.Freight.FreightSchemas+PickupRequest";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"Argix.Freight.FreightSystemServiceSchemas+ArriveShipment";
                return _TrgSchemas;
            }
        }
    }
}
