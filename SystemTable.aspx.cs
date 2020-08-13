using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DDWeb.Models;

namespace DDWeb
{
    public partial class SystemTable : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAc();
        }
        private void ChkAc()
        {
            string strSQL;
            System.Data.DataView  UserDv;
            UserDv = new System.Data.DataView();
            DataTable dataTable = new DataTable();
            string strmilisurveyCon = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
            MySqlConnection conmilisurvey = new MySqlConnection(strmilisurveyCon);

            DataSet ds = new DataSet();

            strSQL = "select TABLE_SCHEMA,TABLE_NAME, TABLE_TYPE  FROM information_schema.TABLES;";
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conmilisurvey);
            da.Fill(dataTable);
            //da.Fill(ds);
            if (dataTable.Rows.Count > 0)
            {
                UserDv= new DataView(dataTable);
                dvSys.AutoGenerateColumns = true;
                dvSys.DataSource = UserDv;
                dvSys.DataBind();
                dvSys.Rows[0].Cells[2].Width = 200;
                dvSys.Rows[0].Cells[3].Width = 200;
            }
        }

        protected void dvSys_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvSys.PageIndex = e.NewPageIndex;
            ChkAc();
            //dvSys.DataBind();
        }

        protected void dvSys_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect(string.Concat("SystemColumn.aspx?table=" , dvSys.Rows[e.NewEditIndex].Cells[3].Text));
        }
        
    }
}