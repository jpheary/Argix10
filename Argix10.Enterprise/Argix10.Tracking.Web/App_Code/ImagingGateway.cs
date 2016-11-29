using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Argix.Imaging;

namespace Argix.Enterprise {
	//
	public class ImagingGateway {
		//Members

		//Interface
        public ImagingGateway() { }
        public CommunicationState ServiceState { get { return new ImagingServiceClient().State; } }
        public string ServiceAddress { get { return new ImagingServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public DataSet SearchSharePointImageStore(string docClass,string propertyName,string searchText) {
            //
            DataSet results = null;
            Argix.Imaging.ImagingServiceClient client = null;
            try {
                client = new ImagingServiceClient();
                SearchRequest request = new SearchRequest();
                request.ScopeName = "All Sites";
                request.MaxResults = 100;
                request.DocumentClass = docClass;
                request.PropertyName = propertyName;
                request.PropertyValue = searchText;
                results = client.SearchSharePointImageStore(request);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> ef) { client.Abort(); throw new ApplicationException(ef.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return results;
        }
        public byte[] GetSharePointImageStream(string uri) {
            //
            byte[] image = null;
            Argix.Imaging.ImagingServiceClient client = null;
            try {
                client = new ImagingServiceClient();
                image = client.GetSharePointImageStreamByUri(uri);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> ef) { client.Abort(); throw new ApplicationException(ef.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return image;
        }
    }
}