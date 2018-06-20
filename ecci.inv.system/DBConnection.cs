using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ecci.inv.system
{
    public class DBConnection
    {
        public SqlConnection _con;
        public SqlCommand Cmd;
        public SqlDataAdapter _da;
        public SqlDataReader _dr;
        public DataTable _dt;
        string cs = WebConfigurationManager.ConnectionStrings["getdatabase"].ConnectionString;
            
        public void OpenConection()
        {
            _con = new SqlConnection(cs);
            _con.Open();
        }


        public void CloseConnection()
        {
            _con.Close();
        }


        public void ExecSqlQuery(string Query_)
        {
            Cmd = new SqlCommand(Query_, _con);
        }

        public void ExecNonQuery()
        {
            Cmd.ExecuteNonQuery();
        }
        public DataTable DataQueryExec()
        {
            _da = new SqlDataAdapter(Cmd);
            _dt = new DataTable();
            _da.Fill(_dt);
            return _dt;
        }
        public SqlDataReader DataReader(string Query_)
        {
            Cmd = new SqlCommand(Query_, _con);
            _dr = Cmd.ExecuteReader();
            return _dr;
        }


        public object ShowDataInGridView(string Query_)
        {
            _da = new SqlDataAdapter(Query_, cs);
            DataSet ds = new DataSet();
            _da.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}