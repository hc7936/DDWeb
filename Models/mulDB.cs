using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using DDWeb.Models;
using System.Windows.Input;
using System.Web.UI.WebControls;

namespace DDWeb.Models
{
    public class mulDB
    {
        static string strmilisurveyCon = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
        public const int intConmilisurvey = 1;
        private MySqlConnection conmilisurvey = new MySqlConnection(strmilisurveyCon);

        public MySqlCommand myCommand;
        private string strSQL;
        private int conItem;

        public DataView getDBtoDV(int iconItem, string sSQL)
        {
            MySqlDataAdapter da;
            DataTable dt = new DataTable();
            MySqlConnection myConnection;
            conItem = iconItem;
            myConnection = getConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            da = new MySqlDataAdapter(sSQL, myConnection);
            da.Fill(dt);
            myConnection.Close();
            return dt.DefaultView;
        }

        private MySqlConnection getConnection()
        {
            switch (conItem)
            {
                case 1:
                    return conmilisurvey;
                default:
                    return conmilisurvey;
            }
        }

        public bool exeSqlNonQuery(int iconItem, string sSQL)
        {
            strSQL = sSQL;
            conItem = iconItem;
            return SqlNonQuery();
        }

        private bool SqlNonQuery()
        {
            MySqlConnection myConnection;
            bool blnState;
            myConnection = getConnection();
            myConnection.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(strSQL, myConnection);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
                blnState = true;
            }
            catch (Exception EX)
            {
                blnState = false;
            }
            myConnection.Close();
            return blnState;
        }

        string MyformatDateTime(string datetime)
        {
            string sYear, sMonth, sDay;
            sYear = (DateTime.Parse(datetime).Year - 1911).ToString("000");
            sMonth = DateTime.Parse(datetime).Month.ToString("00");
            sDay = DateTime.Parse(datetime).Day.ToString("00");
            return string.Concat(sYear, sMonth, sDay);
        }

        string displayData(string oldStr)
        {
            oldStr = oldStr.Replace(Environment.NewLine, "<BR>");
            oldStr = oldStr.Replace(" ", "&nbsp;");
            return oldStr;
        }

        string editData(string oldStr)
        {
            oldStr = oldStr.Replace("<br>", Environment.NewLine);
            oldStr = oldStr.Replace("<BR>", Environment.NewLine);
            oldStr = oldStr.Replace("&nbsp;", " ");
            return oldStr;
        }

    }
}
