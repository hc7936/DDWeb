using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDWeb.Models
{

    public class DV : System.Data.DataView
    {
        public string Connection_String = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
        private string dtname = "";
        private string cmdstr = "";
        public System.Data.SqlClient.SqlConnection XConn = new System.Data.SqlClient.SqlConnection();


        public DV()
        {
            System.Data.DataView DV = new System.Data.DataView();
            XConn.ConnectionString = Connection_String;
        }

        private void CloseObjects()
        {
            if (XConn.State != System.Data.ConnectionState.Closed)
            {
                XConn.Close();
                XConn.Dispose();
            }
        }
        public DV(string DT_Name, string Cmd){
            dtname = DT_Name;
            cmdstr = Cmd;
            XConn.ConnectionString = Connection_String;
            Table = rtnDV(DT_Name, Cmd).Table;
        }

        public string  DT_Name()
        {
             return DT_Name(); 
        }

        public string Cmd()
        {
            return Cmd();
        }

        private System.Data.DataView rtnDV(string dt_name, string cmd)
        {
            XConn.Open();
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd, XConn);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds, dt_name);
            da.Dispose();
            XConn.Close();
            return ds.Tables[dt_name].DefaultView;
        }

        public string CommdScalar(string CmdTxt)
        {
            cmdstr = CmdTxt;
            XConn.ConnectionString = Connection_String;
            return CommdResult(CmdTxt);
        }

        public int CommdQuery(string CmdTxt )
        {
            cmdstr = CmdTxt;
            XConn.ConnectionString = Connection_String;
            return CommdResult();
        }

        private string CommdResult(string cmd)
        {
            string Result = "";
            System.Data.SqlClient.SqlCommand  com  = new System.Data.SqlClient.SqlCommand(cmd, XConn);
            XConn.Open();
            try
            {
                Result = com.ExecuteScalar().ToString().Trim();
            }
            catch
            {
                Result = "";
            }
            finally
            {
                com.Dispose();
                CloseObjects();
            }
            return Result;
        }

        private int CommdResult()
        {
            System.Data.SqlClient.SqlCommand com  = new  System.Data.SqlClient.SqlCommand(Cmd(), XConn);
            XConn.Open();
            int Result = com.ExecuteNonQuery();
            com.Dispose();
            CloseObjects();
            return Result;
        }
    }
}