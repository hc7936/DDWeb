using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;

namespace DDWeb.Models
{
    public class pub_Procees : System.ComponentModel.Component
    {
        private System.ComponentModel.Container components;
        private String strMsg;

        public pub_Procees(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public pub_Procees()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        public string jsMsg(string strMSG)
        {
            this.strMsg = strMSG;
            string strM = Msg();
            return strM;
        }

        public string jsBack()
        {
            string strBack = Back();
            return strBack;
        }

        public string jsSaveMsg(string strMSG, string strPage)
        {
            this.strMsg = strMSG;
            string strM = saveMsg(strPage);
            return strM;

        }

        private string saveMsg(string strPage)
        {
            string tempStr = "";
            tempStr = "<script language='javascript'>";
            tempStr = tempStr + "alert('" + this.strMsg + "');";
            tempStr = tempStr + "window.location.href='" + strPage + "';";
            tempStr = tempStr + "</script>";
            return tempStr;
        }
        public string Back()
        {
            string tempStr = "";
            tempStr = "<script language='javascript'>";
            tempStr = tempStr + "history.back();";
            tempStr = tempStr + "</script>";
            return tempStr;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components == null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool isNumeric(string str)
        {
            Regex r = new Regex("[^0-99]");
            if (r.IsMatch(str))
                return false;
            else
                return true;
        }

        public bool isReg(string str, string reg)
        {
            Regex r = new Regex("[^" + reg.Trim() + "]");
            if (r.IsMatch(str))
                return false;
            else
                return true;
        }

        public bool isNumeric2(string str)
        {
            int i = 0;
            while (i < str.Length)
            {
                if (!Char.IsDigit(Char.Parse(str.Substring(i, 1))))
                    return false;
                System.Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
            }
            return true;
        }

        public string Msg()
        {
            string tempStr = "";
            tempStr = "<script language='javascript'>";
            tempStr = tempStr + "alert('" + this.strMsg + "');";
            tempStr = tempStr + "</script>";
            return tempStr;
        }

    }
}


