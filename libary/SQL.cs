using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Windows.Forms;

namespace libary
{
    
    class SQL
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        public void myconnectors()
        {
            string myConnectionString = "server=50.62.209.147;uid=killermillergb;pwd=Towcester1;database=killermillergb_;";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
