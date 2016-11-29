namespace Argix {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSchemas+LTLShipment", typeof(global::Argix.Freight.FreightSchemas.LTLShipment))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSystemServiceSchemas+DispatchShipment", typeof(global::Argix.Freight.FreightSystemServiceSchemas.DispatchShipment))]
    public sealed class LTLShipmentToLTLShipmentRequestMap : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var"" version=""1.0"" xmlns:ns1=""http://schemas.datacontract.org/2004/07/Argix.Freight"" xmlns:ns0=""http://Argix.Freight"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/ns1:LTLShipment"" />
  </xsl:template>
  <xsl:template match=""/ns1:LTLShipment"">
    <ns0:DispatchShipment>
      <ns0:shipment>
        <xsl:if test=""ns1:ClientID"">
          <ns1:ClientID>
            <xsl:value-of select=""ns1:ClientID/text()"" />
          </ns1:ClientID>
        </xsl:if>
        <xsl:if test=""ns1:ConsigneeID"">
          <ns1:ConsigneeID>
            <xsl:value-of select=""ns1:ConsigneeID/text()"" />
          </ns1:ConsigneeID>
        </xsl:if>
        <xsl:if test=""ns1:Created"">
          <ns1:Created>
            <xsl:value-of select=""ns1:Created/text()"" />
          </ns1:Created>
        </xsl:if>
        <xsl:if test=""ns1:ID"">
          <ns1:ID>
            <xsl:value-of select=""ns1:ID/text()"" />
          </ns1:ID>
        </xsl:if>
        <xsl:if test=""ns1:LastUpdated"">
          <ns1:LastUpdated>
            <xsl:value-of select=""ns1:LastUpdated/text()"" />
          </ns1:LastUpdated>
        </xsl:if>
        <xsl:if test=""ns1:PickupDate"">
          <ns1:PickupDate>
            <xsl:value-of select=""ns1:PickupDate/text()"" />
          </ns1:PickupDate>
        </xsl:if>
        <xsl:if test=""ns1:PickupID"">
          <ns1:PickupID>
            <xsl:value-of select=""ns1:PickupID/text()"" />
          </ns1:PickupID>
        </xsl:if>
        <xsl:if test=""ns1:ShipDate"">
          <ns1:ShipDate>
            <xsl:value-of select=""ns1:ShipDate/text()"" />
          </ns1:ShipDate>
        </xsl:if>
        <xsl:if test=""ns1:ShipmentNumber"">
          <xsl:variable name=""var:v1"" select=""string(ns1:ShipmentNumber/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v1)='true'"">
            <ns1:ShipmentNumber>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ShipmentNumber>
          </xsl:if>
          <xsl:if test=""string($var:v1)='false'"">
            <ns1:ShipmentNumber>
              <xsl:value-of select=""ns1:ShipmentNumber/text()"" />
            </ns1:ShipmentNumber>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:ShipperID"">
          <ns1:ShipperID>
            <xsl:value-of select=""ns1:ShipperID/text()"" />
          </ns1:ShipperID>
        </xsl:if>
        <xsl:if test=""ns1:ShipperNumber"">
          <xsl:variable name=""var:v2"" select=""string(ns1:ShipperNumber/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v2)='true'"">
            <ns1:ShipperNumber>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ShipperNumber>
          </xsl:if>
          <xsl:if test=""string($var:v2)='false'"">
            <ns1:ShipperNumber>
              <xsl:value-of select=""ns1:ShipperNumber/text()"" />
            </ns1:ShipperNumber>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:UserID"">
          <xsl:variable name=""var:v3"" select=""string(ns1:UserID/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v3)='true'"">
            <ns1:UserID>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:UserID>
          </xsl:if>
          <xsl:if test=""string($var:v3)='false'"">
            <ns1:UserID>
              <xsl:value-of select=""ns1:UserID/text()"" />
            </ns1:UserID>
          </xsl:if>
        </xsl:if>
      </ns0:shipment>
    </ns0:DispatchShipment>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Argix.Freight.FreightSchemas+LTLShipment";
        
        private const string _strTrgSchemasList0 = @"Argix.Freight.FreightSystemServiceSchemas+DispatchShipment";
        
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
                _TrgSchemas[0] = @"Argix.Freight.FreightSystemServiceSchemas+DispatchShipment";
                return _TrgSchemas;
            }
        }
    }
}
