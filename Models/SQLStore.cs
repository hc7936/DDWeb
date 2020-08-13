using System;
using MySql.Data.MySqlClient;

namespace DDWeb.Models
{

    public class SQLStore
    {
        protected MySqlConnection cn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString);
        protected MySqlDataAdapter adapter;
        protected MySqlCommand com;

        public SQLStore()
        {
            adapter = new MySqlDataAdapter(String.Empty, cn);
            com = new MySqlCommand(String.Empty, cn);
        }

        public SQLStore(MySqlTransaction tra)
        {
            tra.Connection.Open();
            com = new MySqlCommand("", tra.Connection);
            adapter = new MySqlDataAdapter(com);
        }

        public MySqlCommand GetCom()
        {
            return com;
        }

    }
}
