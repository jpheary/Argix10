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
    public interface ISchedule {
        //Interface
        event StatusEventHandler StatusMessage;
        event EventHandler ServiceStatesChanged;

        bool CanNew { get; }
        void New();
        bool CanOpen { get; }
        void Open();
        bool CanClone { get; }
        void Clone();
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
        bool CanNewTemplate { get; }
        void NewTemplate();
        bool CanOpenTemplate { get; }
        void OpenTemplate();
        bool CanCancelTemplate { get; }
        void CancelTemplate();
        bool CanLoadTemplates { get; }
        void LoadTemplates();
        bool TemplatesVisible { get; set; }
    }
}
