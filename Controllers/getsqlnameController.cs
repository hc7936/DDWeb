using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.UI;
using System.Data;
using System.Web;
using Ubiety.Dns.Core;
using System.Web.Http.Description;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using System.Data;
using Org.BouncyCastle.Utilities;
using System.Net.Mime;

namespace DDWeb.Controllers
{
    public class getsqlnameController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post()
        {
            string cValue, cTable, cFields, cPkey, cFieldsnum;
            int nChoice;
            string[] cResult;
            //byte[] result = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), Encoding.UTF8.GetBytes(Request.Properties["v"].ToString()));
            var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            cValue = GetParam(nvc, "v"); 
            cTable = GetParam(nvc, "t"); 
            cFields = GetParam(nvc, "s");
            cPkey = GetParam(nvc, "k");  
            cFieldsnum = GetParam(nvc, "n");
            cResult = readdata(cValue, cTable, cPkey, cFields, cFieldsnum);
            //Json(cResult);
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, cResult);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, cResult);
            return response;
            //return cResult;
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        string[] readdata(string  pvalue , string  ptable,  string  pkey , string  pfields , string pnum)
        {
            string cScript = "";
            string strSQL="";
                    string cSqlcmd = "";
                string[] cRdata;
                int nCount=0;
                string cFldvalue;
                decimal nFld_num;
                string dFld_date;
                string cFld_str= null;
                string cTitleno;
                int RowIndex=0;
                int nCchgcpymk;
            string[] jsonStr= new string[2];

            if (pvalue != "" && ptable != "" && pkey != "" && pfields != ""  && pnum != ""){
                DataTable dataTable = new DataTable();
                string strmilisurveyCon = System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
                MySqlConnection conmilisurvey = new MySqlConnection(strmilisurveyCon);

                cScript += "{";
                cSqlcmd += " select " + pfields + " from  " + ptable + " where " + pkey + "='" + pvalue + "' ";
                MySqlDataAdapter da = new MySqlDataAdapter(cSqlcmd, conmilisurvey);
                da.Fill(dataTable);
                if(dataTable.Rows.Count>0 )
                {
                    //'先用欄位數拆字串
                    cRdata = atChar(int.Parse(pnum), ",", pfields);
                    while (nCount < int.Parse(pnum)) 
                    {
                        cFldvalue = cRdata[nCount].ToString();
                        
                        cScript +=  "fld" + (nCount + 1).ToString().Trim() + "\"" + "\"" + ":" + "\"" + "\"";
                        if (dataTable.Rows[0][cFldvalue].GetType().Name.Equals("String"))
                        {
                            for(int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                cFld_str +=  dataTable.Rows[i][cFldvalue].ToString().Trim() + ",";
                            }
                            cFld_str = cFld_str.Substring(0, cFld_str.Length - 1);
                            cScript += cFld_str.Trim()  + "";
                            jsonStr[0] = "fld" + (nCount + 1).ToString().Trim() + ":" + cFld_str.Trim();
                        }
                        else if (dataTable.Rows[0][cFldvalue].GetType().Name.Equals("Decimal"))
                        {
                            nFld_num = decimal.Parse(dataTable.Rows[0][cFldvalue].ToString());
                            cScript += nFld_num.ToString().Trim();
                        }
                        else if (dataTable.Rows[0][cFldvalue].GetType().Name.Equals("DateTime"))
                        {
                            dFld_date = dataTable.Rows[0][cFldvalue] + "";
                            cScript += (dFld_date + "").Trim();
                        }
                        else
                        {
                            cFld_str = (dataTable.Rows[0][cFldvalue] + "").Trim();
                            cScript += cFld_str;
                        }
                        cScript += "\"" + "\"" + ",";
                        nCount += 1;
                    };
                    jsonStr[1] = "ok:" + "T";
                    cScript += "\"" + "\"" + "ok" + "\"" + "\"" +  ":" + "\"" + "\"" + "T" + "\"" + "\"";
                }
                else
                {
                    cScript += "\"" + "\"" + "fld1" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld2" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld3" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld4" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld5" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld6" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "fld7" + ":" + "\"" + "\"" + ",";
                    cScript += "\"" + "\"" + "ok" + ":" + "F" + "\"" + "\"";
                }
                cScript += "}";
            }
            else
            {
                cScript += "{";
                cScript += "\"" + "\"" + "fld1" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld2" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld3" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld4" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld5" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld6" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "fld7" + ":" + "\"" + "\"" + ",";
                cScript += "\"" + "\"" + "ok" + ":" + "F" + "\"" + "\"";
                cScript += "}";
            }
            return jsonStr;
            //return cScript;
        }

        public string[] atChar(int nFields ,string  cSubchar ,string cSstring )
        {
            //nFields:共幾欄,cSubchar:截取字元,cSstring:來源字串
            string cT = cSstring;
            string[] f=new string[nFields];
            //'索引值從0開始
            int nCount=0;
            int nI;
            string cShow;
            while (cT.IndexOf(cSubchar) != -1) {
                    nI = cT.IndexOf(cSubchar);
                    if (nI == 0) {
                        //表沒有內容可截取,再取下一個
                        cT = cT.Substring(cT.IndexOf(cSubchar) + 1);
                        f[nCount] = "";
                    }
                    else
                    {
                        //表有內容可截取
                        cShow = cT.Substring(0, cT.IndexOf(cSubchar));
                        cT = cT.Substring(cT.IndexOf(cSubchar) + 1);
                        f[nCount] = cShow;
                    }
                    nCount = nCount + 1;
            }; 
            if (cT.Trim().Length   != 0) {
                f[nCount] = cT;
            }
            return f;
        }

        static string GetParam(NameValueCollection nvc, string key)
        {
            return  nvc.Get(key);
        }
    }
}