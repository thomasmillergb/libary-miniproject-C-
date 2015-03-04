using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace libary
{
    class DbConnect
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        public void connect()
        {
            string myConnectionString = "server=50.62.209.147;uid=killermillergb;pwd=Towcester1;database=killermillergb_;convert zero datetime=True";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void close()
        {
            if(conn.State == ConnectionState.Open)
            {
                
                conn.Close();
            }
         

        }
        public void quickQuery(string sql)
        {
            try
            {
                connect();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Prepare();
                
       
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    close();
                }
            }

        }
        public List<string[]> search(string name, string author)
        {
            
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr = null;
            List<string[]> arrayList = new List<string[]>();
            try
            {
                connect();
                
                cmd.Connection = conn;
                if (name != "" || author != "")
                {
                    if (name != "")
                        if (author != "")
                        {
                            cmd.CommandText = "SELECT  * FROM libary WHERE(name = '" + name + "' and  author =  '" + author + "')";
                        }
                        else
                        {
                            cmd.CommandText = "SELECT  * FROM libary WHERE(name = '"+name + "')";
                        }
                    else
                    {
                        cmd.CommandText = "SELECT  * FROM libary WHERE(author = '" + author + "')";
              
                    }


                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string[] cols = new string[5];
                        cols[0] = rdr.GetString(0);
                        cols[1] = rdr.GetString(1);
                        cols[2] = rdr.GetString(2);
                        cols[3] = rdr.GetDateTime(3).ToString();
                        cols[4] = rdr.GetString(4);
                        arrayList.Add(cols);
                    
                        
                    }
                    
                }
                else
                {
                    //emptey else
                }
            }
            catch (MySqlException ex)
            {
             
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    close();
                }
            }
            return arrayList;
        
         }
        public void add(string name, string author, string published, Boolean refrence)
        {
            try
            {
                connect();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO libary(name, author, published, ref) VALUES(@name, @author,@pub,@refrence)";
                cmd.Prepare();
                //protects from SQL injections 
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@pub", published);
                cmd.Parameters.AddWithValue("@refrence", refrence);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    close();
                }
            }
        

                
              
        }

    }
}
