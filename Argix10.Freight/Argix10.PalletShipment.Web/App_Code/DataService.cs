using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

public class DataService {
    //Members

    //Interface
    public DataService() { }
    public DataSet FillDataset(string sqlConnection,string spName,string table,object[] paramValues) {
        //
        DataSet ds = new DataSet();
        Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>(sqlConnection);
        DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
        db.LoadDataSet(cmd,ds,table);
        return ds;
    }
    public IDataReader ExecuteReader(string sqlConnection,string spName,object[] paramValues) {
        //
        Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>(sqlConnection);
        IDataReader reader = db.ExecuteReader(spName,paramValues);
        return reader;
    }
    public bool ExecuteNonQuery(string sqlConnection,string spName,object[] paramValues) {
        //
        bool ret=false;
        Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>(sqlConnection);
        int i = db.ExecuteNonQuery(spName,paramValues);
        ret = i > 0;
        return ret;
    }
    public object ExecuteNonQueryWithReturn(string sqlConnection,string spName,object[] paramValues) {
        //
        object ret=null;
        if((paramValues != null) && (paramValues.Length > 0)) {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>(sqlConnection);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            ret = db.ExecuteNonQuery(cmd);

            //Find the output parameter and return its value
            foreach(DbParameter param in cmd.Parameters) {
                if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                    ret = param.Value;
                    break;
                }
            }
        }
        return ret;
    }
}
