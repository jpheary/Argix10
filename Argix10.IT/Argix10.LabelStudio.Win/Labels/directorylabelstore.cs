//	File:	directorylabelstore.cs
//	Author:	J. Heary
//	Date:	08/18/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelStore class;
//			implements a label store that represents a file system directory 
//			containing a group of label template files (*.zpl).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Argix.Labels {
	//
	public class DirectoryLabelStore: LabelStore {
		//Members
		private FileInfo mDirectory=null;
		
		//Constants
		private const string LABELTEMPLATE_EXT = "zpl";
		//Events
		//Interface
		public DirectoryLabelStore(string directory) { 
			//Constructor
			try {
				//Set custom attributes
				this.mDirectory = new FileInfo(directory);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Directory Label Store instance.", ex); }
		}
		#region Accessors\Modifiers: Directory, ToDataSet()
		public FileInfo Directory { get { return this.mDirectory; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			DataSet ds=null;
			try {
				ds = new DataSet();
				DataTable table = ds.Tables.Add("DirectoryDetailTable");
				table.Columns.Add("Directory");
				table.Rows.Add(new object[]{this.mDirectory.FullName});
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public override LabelTemplate NewLabelTemplate() { return new DirectoryLabelTemplate(null); }
        public override LabelTemplate NewLabelTemplate(LabelDataset.LabelDetailTableRow row) { return new DirectoryLabelTemplate(row); }
		public override void Refresh() { 
			try {				
				//Clear existing entries
				base.mLabels.Clear();
				FileInfo[] oLabelTemplates = this.mDirectory.Directory.GetFiles("*." + LABELTEMPLATE_EXT);
				for(int i=0; i<oLabelTemplates.Length; i++) { 
					base.mLabels.ReadXml(oLabelTemplates[i].FullName, XmlReadMode.Fragment);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing labels in the Directory store.", ex); }
			finally { OnStoreChanged(this, new EventArgs()); }
		}
	}
}