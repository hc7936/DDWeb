using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace DDWeb
{
    public partial class ReportWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSQL;
            DataTable dataTable = new DataTable();
            string strmilisurveyCon = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
            MySqlConnection conmilisurvey = new MySqlConnection(strmilisurveyCon);
            strSQL = strSQL = "select TABLE_NAME FROM information_schema.TABLES;";
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conmilisurvey);
            da.Fill(dataTable);
            table_name.Items.Clear();
            //da.Fill(ds);
            if (dataTable.Rows.Count > 0)
            {
                for(int i=0;i< dataTable.Rows.Count; i++)
                {
                    ListItem Items  = new ListItem();
                    Items.Value = dataTable.Rows[i][0].ToString();
                    table_name.Items.Add(Items);
                }
            }
        }
    }
}