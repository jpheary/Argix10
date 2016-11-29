using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Argix.Freight {
	//
    public interface IToolbar {
        //Interface
        event StatusEventHandler StatusMessage;
        event EventHandler ServiceStatesChanged;

        bool CanNew { get; }
        void New();
        bool CanOpen { get; }
        void Open();
        bool CanCancel { get; }
        void Cancel();
        bool CanSave { get; }
        void Save(string filename);
        bool CanExport { get; }
        void Export();
        void Export(string filename);
        bool CanPrint { get; }
        void Print(bool showDialog);
        bool CanPreview { get; }
        void PrintPreview();
    }

    public interface IPSPToolbar {
        //Interface
        bool CanApproveClient { get; }
        void ApproveClient();
        bool CanDenyClient { get; }
        void DenyClient();
        bool CanPrintLabels { get; }
        void PrintLabels();
        bool CanPrintPaperwork { get; }
        void PrintPaperwork();
    }

    public interface IQuoteToolbar {
        //Interface
        bool CanApproveQuote { get; }
        void ApproveQuote();
        bool CanTenderQuote { get; }
        void TenderQuote();
        bool CanViewTender { get; }
        void ViewTender();
        bool CanBookQuote { get; }
        void BookQuote();
    }

}
