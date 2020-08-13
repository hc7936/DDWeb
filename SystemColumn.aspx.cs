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
    public partial class SystemColumn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSQL;
            System.Data.DataView UserDv;
            UserDv = new System.Data.DataView();
            DataTable dataTable = new DataTable();
            string strmilisurveyCon = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
            MySqlConnection conmilisurvey = new MySqlConnection(strmilisurveyCon);

            DataSet ds = new DataSet();

            strSQL = string.Concat("Select table_name, column_name from information_schema.columns WHERE TABLE_NAME='", Request.QueryString["table"], "'");
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conmilisurvey);
            da.Fill(dataTable);
            //da.Fill(ds);
            if (dataTable.Rows.Count > 0)
            {
                UserDv = new DataView(dataTable);
                dvSys.AutoGenerateColumns = true;
                dvSys.DataSource = UserDv;
                dvSys.DataBind();
                dvSys.Rows[0].Cells[2].Width = 200;
                dvSys.Rows[0].Cells[3].Width = 200;
            }
        }
    }
}