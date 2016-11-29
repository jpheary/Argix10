using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Argix.Data;
using Argix.Windows;

namespace Argix.Tools {
	//
	public class Enterprise: TsortNode {
		//Members
		private const string KEY_TERMINALS = "enterprise/terminals";
		
		//Interface
		public Enterprise(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		#region Accessors\Modifiers: ToDataSet()
		public DataSet ToDataSet() {
			//Return a dataset containing values for this object
			DataSet ds=null;
			try {
				ds = new DataSet();
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		#region TsortNode Implementations: LoadChildNodes(), Refresh()
		public override void LoadChildNodes() {
			//Load child nodes of this node (data)
			try {
				//Clear existing nodes
				base.Nodes.Clear();
				
				//Read terminals from config file (key=terminalName, value=web server url)
				Hashtable oDict = (Hashtable)ConfigurationManager.GetSection(KEY_TERMINALS);
				IDictionaryEnumerator oEnum = oDict.GetEnumerator();
				base.mChildNodes = new TreeNode[oDict.Count];
				for(int i=0; i<base.mChildNodes.Length; i++) {
					oEnum.MoveNext();
					DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
					Terminal terminal = new Terminal(oEntry.Key.ToString(), App.ICON_TERMINAL, App.ICON_TERMINAL, oEntry.Value.ToString());
					base.mChildNodes[i] = terminal;
					terminal.Expand();
					
					//Cascade loading child nodes if this node is expanded (to get the + sign)
					if(base.IsExpanded) terminal.LoadChildNodes();
				}
				base.Nodes.AddRange(base.mChildNodes);
			} 
			catch(Exception ex) { throw ex; }
		}
		public override void Refresh() { LoadChildNodes(); }
		#endregion
	}

    public class Terminal:TsortNode {
        //Members
        private string mName="";
        private string mDBConnection="";
        private ScannerDS mScannerDS=null;
        private Mediator mMediator=null;

        //Events
        public event EventHandler ScannersRefreshed=null;

        //Interface
        public Terminal(string name,int imageIndex,int selectedImageIndex,string dbConnection)
            : base(name,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                this.mName = name;
                this.mDBConnection = dbConnection;
                this.mMediator = new SQLMediator(dbConnection);
                this.mScannerDS = new ScannerDS();
                Refresh();
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: Name, Scanners, ToDataSet()
        public string Name { get { return this.mName; } }
        public ScannerDS Scanners { get { return this.mScannerDS; } }
        public TerminalDS ToDataSet() {
            //Return a dataset containing values for this terminal
            TerminalDS ds=null;
            try {
                ds = new TerminalDS();
                TerminalDS.LocalTerminalTableRow terminal = ds.LocalTerminalTable.NewLocalTerminalTableRow();
                terminal.Description = this.mName;
                ds.LocalTerminalTable.AddLocalTerminalTableRow(terminal);
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        public Scanner GetScanner(string sourceName) {
            //Return an existing scanner for this terminal
            Scanner scanner=null;
            try {
                //Read from the collection (dataset)
                ScannerDS ds = new ScannerDS();
                ds.Merge(this.mScannerDS.ScannerTable.Select("SourceName='" + sourceName + "'"));
                scanner = new Scanner(ds.ScannerTable[0].SourceName,App.ICON_SCANNER,App.ICON_SCANNER,ds.ScannerTable[0],1,this.mMediator);
            }
            catch(Exception ex) { throw ex; }
            return scanner;
        }
        #region TsortNode Implementations: LoadChildNodes(), Refresh()
        public override void LoadChildNodes() {
            //Load child nodes of this node
            try {
                //CLear existing nodes
                base.Nodes.Clear();
                base.mChildNodes = new TreeNode[this.mScannerDS.ScannerTable.Rows.Count];
                for(int i=0;i<this.mScannerDS.ScannerTable.Rows.Count;i++) {
                    Scanner scanner = new Scanner(this.mScannerDS.ScannerTable[i].SourceName,App.ICON_SCANNER,App.ICON_SCANNER,this.mScannerDS.ScannerTable[i],1,this.mMediator);
                    base.mChildNodes[i] = scanner;

                    //Cascade loading child nodes if this node is expanded (to get the + sign)
                    if(base.IsExpanded) scanner.LoadChildNodes();
                }
                base.Nodes.AddRange(base.mChildNodes);
            }
            catch(Exception ex) { throw ex; }
        }
        public override void Refresh() {
            try {
                //Clear existing entries and refresh collection
                this.mScannerDS.Clear();
                IDictionary oDict = (IDictionary)ConfigurationManager.GetSection(this.mName.ToLower() + "/scanners");
                int scanners = Convert.ToInt32(oDict["count"]);
                for(int i=1;i<=scanners;i++) {
                    oDict = (IDictionary)ConfigurationManager.GetSection(this.mName.ToLower() + "/scanner" + i.ToString());
                    string src = oDict["source"].ToString();
                    string pc = oDict["eventmachine"].ToString();
                    string log = oDict["eventlog"].ToString();
                    this.mScannerDS.ScannerTable.AddScannerTableRow(this.mName,src,pc,log);
                }
                this.mScannerDS.AcceptChanges();
                LoadChildNodes();
                if(this.ScannersRefreshed != null) this.ScannersRefreshed(this,new EventArgs());


            }
            catch(Exception ex) { throw ex; }
        }
        #endregion
    }
}