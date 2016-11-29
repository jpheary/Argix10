using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Xml;
using Microsoft.SharePoint.Utilities;

namespace Argix.Enterprise {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class ImagingService:IImagingService {
        //Members

        //Interface
        public ImagingService() { }
        public string GetPortalSearchInfo() {
            //Make sure we have removed the last result from the session
            string psi = "";
            try {
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.search"].ToString();
                //psi = qs.GetPortalSearchInfo();
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return psi;
        }
        public string GetSearchMetadata() {
            //Make sure we have removed the last result from the session
            string xml = "";
            try {
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.search"].ToString();
                DataSet ds = qs.GetSearchMetadata();
                if (ds != null) xml = ds.GetXml();
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return xml;
        }
        public DocumentClasses GetDocumentClasses() {
            //Retrieve document classes
            DocumentClasses docs = null;
            try {
                docs = new DocumentClasses();
                System.Xml.XmlDataDocument xmlDoc = new System.Xml.XmlDataDocument();
                xmlDoc.DataSet.ReadXml(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\documentclass.xml");
                for (int i = 0;i < xmlDoc.DataSet.Tables["DocumentClassTable"].Rows.Count;i++) {
                    DocumentClass dc = new DocumentClass(xmlDoc.DataSet.Tables["DocumentClassTable"].Rows[i]["Department"].ToString(),xmlDoc.DataSet.Tables["DocumentClassTable"].Rows[i]["ClassName"].ToString());
                    docs.Add(dc);
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return docs;
        }
        public DocumentClasses GetDocumentClasses(string department) {
            //Retrieve document classes
            DocumentClasses docs = null;
            try {
                docs = new DocumentClasses();
                DocumentClasses _docs = GetDocumentClasses();
                foreach (DocumentClass dc in _docs) {
                    if (dc.Department == department) docs.Add(dc);
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return docs;
        }
        public MetaDatas GetMetaData() {
            //Retrieve document class metadata
            MetaDatas metas = null;
            try {
                metas = new MetaDatas();
                System.Xml.XmlDataDocument xmlMeta = new System.Xml.XmlDataDocument();
                xmlMeta.DataSet.ReadXml(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\metadata.xml");
                for (int i = 0;i < xmlMeta.DataSet.Tables["MetaDataTable"].Rows.Count;i++) {
                    MetaData md = new MetaData(xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["ClassName"].ToString(),xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["Property"].ToString());
                    metas.Add(md);
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return metas;
        }
        public MetaDatas GetMetaData(string className) {
            //Retrieve document class metadata for the specified className
            MetaDatas metas = null;
            try {
                metas = new MetaDatas();
                MetaDatas _metas = GetMetaData();
                foreach (MetaData md in _metas) {
                    if (md.ClassName == className) metas.Add(md);
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return metas;
        }
        public DataSet SearchSharePointImageStore(SearchRequest request) {
            //
            DataSet ds = null;
            try {
                string SQL = getSQLQuery(request.ScopeName);
                SQL = SQL.Replace("selectStmnt",getSelectClause(request.DocumentClass)).Replace("whereClause",getWhereClause(request)).Replace("MaxSearchResults",request.MaxResults.ToString());
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                //qs.Timeout = 500000;    //Default 100000 msec
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.search"].ToString();
                qs.Credentials = getCredentials();
                ds = qs.QueryEx(SQL);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public byte[] GetSharePointImageStream(SearchRequest request) {
            //Return an image from SharePoint as a byte[]
            byte[] bytes=null;
            try {
                //Search images: return the first one
                DataSet ds = SearchSharePointImageStore(request);
                if (ds.Tables[0].Rows.Count > 0) {
                    //Get image url
                    string url = ds.Tables[0].Rows[0]["DAV:href"].ToString();

                    //Pull the image from a remote client and load into an image object
                    WebClient wc = new WebClient();
                    wc.Credentials = getCredentials();
                    bytes = wc.DownloadData(url);
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return bytes;
        }
        public byte[] GetSharePointImageStream(string uri) {
            //Return an image from SharePoint as a byte[]
            byte[] bytes = null;
            try {
                //Pull the image from a remote client and load into an image object
                WebClient wc = new WebClient();
                wc.Credentials = getCredentials();
                bytes = wc.DownloadData(uri);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return bytes;
        }

        private string getSQLQuery(string scopeName) {
            //<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>
            //<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"scope\"='" + scopeName + "' AND \"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>" +
            //<![CDATA[SELECT DAV:displayname, DAV:href, TBBarCode, TL, CL, DIV, Store, BatchDate FROM SCOPE() WHERE (isdocument=1) AND (TL LIKE '%') ]]>
            string query = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                "<QueryPacket xmlns=\"urn:Microsoft.Search.Query\" Revision=\"1000\">" +
                   "<Query domain=\"QDomain\">" +
                       "<QueryId>Client Query ID</QueryId>" +
                       "<OriginatorId>Tsort Imaging Search v3.5</OriginatorId>" +
                       "<SupportedFormats>" +
                           "<Format>urn:Microsoft.Search.Response.Document.Document</Format>" +
                       "</SupportedFormats>" +
                       "<Context>" +
                           "<QueryText language=\"en-US\" type=\"MSSQLFT\">" +
                                   "<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>" +
                           "</QueryText>" +
                           "<OriginatorContext>" +
                               "<Argix xmlns=\"urn:www.ArgixDirect.com\">SearchReturn</Argix>" +
                           "</OriginatorContext>" +
                       "</Context>" +
                       "<Range>" +
                           "<StartAt>1</StartAt>" +
                           "<Count>MaxSearchResults</Count>" +
                       "</Range>" +
                   "</Query>" +
               "</QueryPacket>";
            return query;
        }
        private string getSelectClause(string docClass) {
            //Build a SELECT clause that includes all properties (fields) for the specified Document Class
            StringBuilder sb = new StringBuilder("\"DAV:displayname\", \"DAV:href\"");
            MetaDatas metas = getMetaData(docClass);
            foreach(MetaData md in metas) {
                sb.Append(", \"" + XmlConvert.EncodeName(md.Property) + "\"");
            }
            return sb.ToString();
        }
        private string getWhereClause(SearchRequest request) {
            //Build a WHERE clause
            string propertyName;
            StringBuilder sb = new StringBuilder();
            if (request.PropertyValue.Trim().Length > 0) {
                propertyName = XmlConvert.EncodeName(request.PropertyName);
                if (propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,request.PropertyValue.Trim()));
                else
                    sb.Append("Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",request.PropertyValue.Trim()));
            }
            if (request.PropertyName1 != null && request.PropertyValue1 != null && request.Operand1 != null && request.PropertyValue1.Trim().Length > 0) {
                sb.Append(" " + request.Operand1);
                propertyName = XmlConvert.EncodeName(request.PropertyName1);
                if (propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,request.PropertyValue1.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",request.PropertyValue1.Trim()));
            }
            if (request.PropertyName2 != null && request.PropertyValue2 != null && request.Operand2 != null && request.PropertyValue2.Trim().Length > 0) {
                sb.Append(" " + request.Operand2);
                propertyName = XmlConvert.EncodeName(request.PropertyName2);
                if (propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,request.PropertyValue2.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",request.PropertyValue2.Trim()));
            }
            if (request.PropertyName3 != null && request.PropertyValue3 != null && request.Operand3 != null && request.PropertyValue3.Trim().Length > 0) {
                sb.Append(" " + request.Operand3);
                propertyName = XmlConvert.EncodeName(request.PropertyName3);
                if (propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,request.PropertyValue3.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",request.PropertyValue3.Trim()));
            }
            return sb.ToString();
        }
        private string getDateClause(string propertyName,string propertyValue) {
            //Build a WHERE clause for a datetime field
            string from = SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(propertyValue));
            string to = SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(propertyValue).AddDays(1).AddSeconds(-1));
            return " ((" + propertyName + " >= '" + from + "') AND (" + propertyName + " <= '" + to + "'))";
        }
        private ICredentials getCredentials() {
            //Determine credentials for web service access
            ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            if(WebConfigurationManager.AppSettings["UseSpecificCredentials"] == "1") {
                string username = WebConfigurationManager.AppSettings["username"];
                string password = WebConfigurationManager.AppSettings["password"];
                string domain = WebConfigurationManager.AppSettings["domain"];
                credentials = new NetworkCredential(username,password,domain);
            }
            return credentials;
        }
        private MetaDatas getMetaData() {
            //Retrieve document class metadata
            MetaDatas metas = null;
            try {
                metas = new MetaDatas();
                System.Xml.XmlDataDocument xmlMeta = new System.Xml.XmlDataDocument();
                xmlMeta.DataSet.ReadXml(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\metadata.xml");
                for (int i = 0;i < xmlMeta.DataSet.Tables["MetaDataTable"].Rows.Count;i++) {
                    MetaData md = new MetaData(xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["ClassName"].ToString(),xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["Property"].ToString());
                    metas.Add(md);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return metas;
        }
        private MetaDatas getMetaData(string className) {
            //Retrieve document class metadata for the specified className
            MetaDatas metas = null;
            try {
                metas = new MetaDatas();
                MetaDatas _metas = getMetaData();
                foreach (MetaData md in _metas) {
                    if (md.ClassName == className) metas.Add(md);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return metas;
        }
    }
}
