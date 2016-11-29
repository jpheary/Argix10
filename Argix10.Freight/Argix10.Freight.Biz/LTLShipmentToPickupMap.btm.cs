namespace Argix {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSchemas+LTLShipment", typeof(global::Argix.Freight.FreightSchemas.LTLShipment))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Argix.Freight.FreightSystemServiceSchemas+SchedulePickupRequest", typeof(global::Argix.Freight.FreightSystemServiceSchemas.SchedulePickupRequest))]
    public sealed class LTLShipmentToPickupMap : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var"" version=""1.0"" xmlns:ns1=""http://schemas.datacontract.org/2004/07/Argix.Freight"" xmlns:ns0=""http://Argix.Freight"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/ns1:LTLShipment"" />
  </xsl:template>
  <xsl:template match=""/ns1:LTLShipment"">
    <ns0:SchedulePickupRequest>
      <ns0:request>
        <xsl:if test=""ns1:Pallets"">
          <ns1:Amount>
            <xsl:value-of select=""ns1:Pallets/text()"" />
          </ns1:Amount>
        </xsl:if>
        <ns1:AmountType>
          <xsl:text>Pallets</xsl:text>
        </ns1:AmountType>
        <ns1:CallerName>
          <xsl:text>Online</xsl:text>
        </ns1:CallerName>
        <xsl:if test=""ns1:ClientName"">
          <xsl:variable name=""var:v1"" select=""string(ns1:ClientName/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v1)='true'"">
            <ns1:Client>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:Client>
          </xsl:if>
          <xsl:if test=""string($var:v1)='false'"">
            <ns1:Client>
              <xsl:value-of select=""ns1:ClientName/text()"" />
            </ns1:Client>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:ClientNumber"">
          <xsl:variable name=""var:v2"" select=""string(ns1:ClientNumber/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v2)='true'"">
            <ns1:ClientNumber>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ClientNumber>
          </xsl:if>
          <xsl:if test=""string($var:v2)='false'"">
            <ns1:ClientNumber>
              <xsl:value-of select=""ns1:ClientNumber/text()"" />
            </ns1:ClientNumber>
          </xsl:if>
        </xsl:if>
        <ns1:Comments>
          <xsl:text>Submitted by BizTalk</xsl:text>
        </ns1:Comments>
        <ns1:CreateUserID>
          <xsl:text>PSP</xsl:text>
        </ns1:CreateUserID>
        <xsl:if test=""ns1:Created"">
          <ns1:Created>
            <xsl:value-of select=""ns1:Created/text()"" />
          </ns1:Created>
        </xsl:if>
        <ns1:FreightType>
          <xsl:text>Tsort</xsl:text>
        </ns1:FreightType>
        <ns1:IsTemplate>
          <xsl:text>0</xsl:text>
        </ns1:IsTemplate>
        <xsl:if test=""ns1:LastUpdated"">
          <ns1:LastUpdated>
            <xsl:value-of select=""ns1:LastUpdated/text()"" />
          </ns1:LastUpdated>
        </xsl:if>
        <ns1:OrderType>
          <xsl:text>B</xsl:text>
        </ns1:OrderType>
        <xsl:if test=""ns1:ShipDate"">
          <ns1:ScheduleDate>
            <xsl:value-of select=""ns1:ShipDate/text()"" />
          </ns1:ScheduleDate>
        </xsl:if>
        <xsl:if test=""ns1:ShipperName"">
          <xsl:variable name=""var:v3"" select=""string(ns1:ShipperName/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v3)='true'"">
            <ns1:Shipper>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:Shipper>
          </xsl:if>
          <xsl:if test=""string($var:v3)='false'"">
            <ns1:Shipper>
              <xsl:value-of select=""ns1:ShipperName/text()"" />
            </ns1:Shipper>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:ShipperAddress"">
          <xsl:variable name=""var:v4"" select=""string(ns1:ShipperAddress/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v4)='true'"">
            <ns1:ShipperAddress>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ShipperAddress>
          </xsl:if>
          <xsl:if test=""string($var:v4)='false'"">
            <ns1:ShipperAddress>
              <xsl:value-of select=""ns1:ShipperAddress/text()"" />
            </ns1:ShipperAddress>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:ShipperNumber"">
          <xsl:variable name=""var:v5"" select=""string(ns1:ShipperNumber/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v5)='true'"">
            <ns1:ShipperNumber>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ShipperNumber>
          </xsl:if>
          <xsl:if test=""string($var:v5)='false'"">
            <ns1:ShipperNumber>
              <xsl:value-of select=""ns1:ShipperNumber/text()"" />
            </ns1:ShipperNumber>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:ShipperContactPhone"">
          <xsl:variable name=""var:v6"" select=""string(ns1:ShipperContactPhone/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v6)='true'"">
            <ns1:ShipperPhone>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:ShipperPhone>
          </xsl:if>
          <xsl:if test=""string($var:v6)='false'"">
            <ns1:ShipperPhone>
              <xsl:value-of select=""ns1:ShipperContactPhone/text()"" />
            </ns1:ShipperPhone>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:UserID"">
          <xsl:variable name=""var:v7"" select=""string(ns1:UserID/@xsi:nil) = 'true'"" />
          <xsl:if test=""string($var:v7)='true'"">
            <ns1:UserID>
              <xsl:attribute name=""xsi:nil"">
                <xsl:value-of select=""'true'"" />
              </xsl:attribute>
            </ns1:UserID>
          </xsl:if>
          <xsl:if test=""string($var:v7)='false'"">
            <ns1:UserID>
              <xsl:value-of select=""ns1:UserID/text()"" />
            </ns1:UserID>
          </xsl:if>
        </xsl:if>
        <xsl:if test=""ns1:Weight"">
          <ns1:Weight>
            <xsl:value-of select=""ns1:Weight/text()"" />
          </ns1:Weight>
        </xsl:if>
        <ns1:WindowClose>
          <xsl:text>1700</xsl:text>
        </ns1:WindowClose>
        <ns1:WindowOpen>
          <xsl:text>900</xsl:text>
        </ns1:WindowOpen>
      </ns0:request>
    </ns0:SchedulePickupRequest>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Argix.Freight.FreightSchemas+LTLShipment";
        
        private const string _strTrgSchemasList0 = @"Argix.Freight.FreightSystemServiceSchemas+SchedulePickupRequest";
        
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
                _TrgSchemas[0] = @"Argix.Freight.FreightSystemServiceSchemas+SchedulePickupRequest";
                return _TrgSchemas;
            }
        }
    }
}
