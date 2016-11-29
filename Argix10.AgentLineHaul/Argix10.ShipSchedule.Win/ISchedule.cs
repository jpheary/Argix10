using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Argix.AgentLineHaul {
    //
    public interface ISchedule {
        //Interface
        event StatusEventHandler StatusMessage;
        event EventHandler ServiceStatesChanged;

        bool CanNew { get; }
        void New();
        bool CanOpen { get; }
        void Open();
        bool CanSave { get; }
        void Save(string filename);
        bool CanExport { get; }
        void Export();
        void Export(string filename);
        bool CanPrint { get; }
        void Print(bool showDialog);
        bool CanPreview { get; }
        void PrintPreview();

        bool CanCut { get; }
        void Cut();
        bool CanCopy { get; }
        void Copy();
        bool CanPaste { get; }
        void Paste();

        bool CanAddLoads { get; }
        void AddLoads();
        bool CanCancelLoad { get; }
        bool IsLoadCancelled { get; }
        void CancelLoad();

        bool CanEmailCarriers { get; }
        void EmailCarriers(bool showDialog);
        bool CanEmailAgents { get; }
        void EmailAgents(bool showDialog);
    }
}
