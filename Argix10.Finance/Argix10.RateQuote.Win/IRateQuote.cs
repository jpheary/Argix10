using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Argix.Finance {
	//
    public interface IRateQuote {
        //Interface
        event StatusEventHandler StatusMessage;
        event EventHandler ServiceStatesChanged;

        string RequestType { get; }
        object Request { get; }
        bool CanSave { get; }
        void Save(string filename);
        bool CanExport { get; }
        void Export(string filename);
        bool CanPrint { get; }
        void Print(bool showDialog);
        bool CanPreview { get; }
        void PrintPreview();
    }
}
