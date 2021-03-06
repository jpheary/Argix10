//	File:	directorylabeltemplate.cs
//	Author:	J. Heary
//	Date:	08/17/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelTemplate 
//			class; implements a label template that represents a single file in 
//			the file system directory containing a group of label template files.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using Argix.Data;

namespace Argix.Labels {
	//
	public class DirectoryLabelTemplate: LabelTemplate { 
		//Members
		//Constants
		//Events
		//Interface
        public DirectoryLabelTemplate(LabelDataset.LabelDetailTableRow labelTemplate) : base(labelTemplate) { }
		public override bool Create() { return false; }
		public override bool Update() { return false; }
		public override bool Delete() { return false; }
        public override LabelTemplate Copy() { return new DirectoryLabelTemplate((LabelDataset.LabelDetailTableRow)this.ToDataSet().Tables["LabelDetailTable"].Rows[0]); }
	}
}