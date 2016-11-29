using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Tools.Applications;

namespace Argix {
    //
    class Program {
        static void Main(string[] args) {
            ServerDocument sd = null;
            try {
                //Update the documents' internal manifest to point to the new location of 
                //the customization library (dll)
                //1. Open the office document/template
                //Console.WriteLine("Enter the fullpath to the Office document or template:");
                //string doc = Console.ReadLine();
                sd = new ServerDocument("c:\\Argix.InvoiceHugoBoss.xltx");
                
                //2. Get the new path of the .Net dll (local, UNC, http, ftp)
                //Console.WriteLine("Enter the fullpath to the customization library:");
                //string lib = Console.ReadLine();
                string uri = sd.DeploymentManifestUrl.ToString(); // = lib;
                string id = sd.SolutionId.ToString(); // = lib;

                //3. Save the assembly path into the document/template internal manifest
                sd.DeploymentManifestUrl = new Uri(sd.DeploymentManifestUrl.AbsoluteUri.Replace("rgxweb", "rgxvmweb"));
                sd.Save();
                Console.Read();
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); Console.Read(); }
            finally { if(sd != null) { sd.Close(); }  }
        }
    }
}
