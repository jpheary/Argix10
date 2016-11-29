using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace Argix {
	//
	public class ExcelFormat {
		//Members
		
        //Interface
		public ExcelFormat() { }
		public void Transform(DataSet ds) { Transform(ds, null); }
        public void Transform(DataSet ds,string fileName) { Transform(ds, "", fileName); }
		public void Transform(DataSet ds, string tableName, string fileName) {
			//Declare the application, workbook and spreadsheet variables
			Application app=null; 
			Workbook workbook=null;
			Worksheet worksheet=null;
			int rowNum=1, colNum=1;
            try {
                //Add a blank workbook with blank spreadsheet to the Excel application
                app = new Application();
                workbook = app.Workbooks.Add(Type.Missing);
                worksheet = (Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                System.Data.DataTable table = tableName.Trim().Length > 0 ? ds.Tables[tableName] : ds.Tables[0];

                //Add the formatting necessary and add the column headers.
                for(int i = 0; i < table.Columns.Count; i++)
                    worksheet.Cells[rowNum, i + 1] = table.Columns[i].ColumnName;

                foreach(DataColumn col in table.Columns) {
                    System.Diagnostics.Debug.WriteLine(col.ColumnName);
                    if((col.DataType.Equals(typeof(System.String)))) {
                        ((Range)worksheet.Cells[rowNum, colNum]).EntireColumn.NumberFormat = "@";
                    }
                    else if((col.DataType.Equals(typeof(System.DateTime)))) {
                        //We need to differentiate between Date and Time fields
                        if(table.Rows.Count > 0) {
                            if(table.Rows[0][col] == System.DBNull.Value) {
                                //If field is null then we can figure out the format based on the column name
                                if(col.ColumnName.ToLower().IndexOf("date", 0) > -1)
                                    ((Range)worksheet.Cells[rowNum, colNum]).EntireColumn.NumberFormat = "MM/dd/yyyy h:mm";
                                else
                                    ((Range)worksheet.Cells[rowNum, colNum]).EntireColumn.NumberFormat = "h:mm";
                            }
                            else {
                                if(((DateTime)table.Rows[0][col]).Year <= 1900)
                                    ((Range)worksheet.Cells[rowNum, colNum]).EntireColumn.NumberFormat = "h:mm";
                                else
                                    ((Range)worksheet.Cells[rowNum, colNum]).EntireColumn.NumberFormat = "MM/dd/yyyy";
                            }
                        }
                    }
                    colNum++;
                }

                //Insert the data into multi-dimentional array
                int rowCount = table.Rows.Count;
                int colCount = table.Columns.Count;
                object[,] valArray = new object[rowCount, colCount];
                for(int i = 0; i < rowCount; i++) {
                    for(int j = 0; j < colCount; j++) {
                        if(table.Rows[i][j].GetType().Equals(typeof(System.String)))
                            valArray[i, j] = "'" + table.Rows[i][j].ToString();
                        else
                            valArray[i, j] = table.Rows[i][j];
                    }
                }
                worksheet.Visible = XlSheetVisibility.xlSheetVisible;
                worksheet.get_Range(worksheet.Cells[rowNum + 1, 1], worksheet.Cells[rowNum + rowCount, colCount]).Value2 = valArray;
                worksheet.get_Range(worksheet.Cells[rowNum + 1, 1], worksheet.Cells[rowNum + rowCount, colCount]).EntireColumn.AutoFit();

                //Save the spreadsheet as a report
                if(fileName == null)
                    app.Visible = true;
                else {
                    //Delete any existing so Excel doesn't prompt user again
                    System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                    if(fi.Exists) fi.Delete();

                    app.ActiveWorkbook.SaveAs(fileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.Workbooks.Close();
                    app.Quit();
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
	}
}
