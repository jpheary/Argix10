//	File:	terminallabelstore.cs
//	Author:	J. Heary
//	Date:	08/17/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelStore class;
//			implements a label store that represents a database table containing 
//			rows of label template files.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Labels {
	//
	public class TerminalLabelStore: LabelStore { 
		//Members
		private Mediator mMediator=null;
		
		//Interface
		public TerminalLabelStore(string dbConnection) { 
			//Constructor
			try {
				//Set custom attributes
				this.mMediator = new SQLMediator(dbConnection);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Terminal Label Store instance.", ex); }
		}
		#region Accessors\Modifiers: ToDataSet()
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			DataSet ds=null;
			try {
				ds = new DataSet();
				DataTable table = ds.Tables.Add("TerminalDetailTable");
				table.Columns.Add("DBConnection");
				table.Rows.Add(new object[]{this.mMediator.Connection});
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public override LabelTemplate NewLabelTemplate() { return new TerminalLabelTemplate(null, this.mMediator); }
        public override LabelTemplate NewLabelTemplate(LabelDataset.LabelDetailTableRow row) { return new TerminalLabelTemplate(row,this.mMediator); }
		public override void Refresh() { 
			try {				
				//Clear existing entries and refresh
				base.mLabels.Clear();
				base.mLabels.Merge(this.mMediator.FillDataset(App.USP_LABELSTORE_VIEW, App.TBL_LABELSTORE_VIEW, null));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing labels in the Terminal store.", ex); }
			finally { OnStoreChanged(this, EventArgs.Empty); }
		}
	}
}