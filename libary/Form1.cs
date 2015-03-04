using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            string name = txtName.Text;
            string author = txtAuthor.Text;
            string published = dtpPublished.Value.ToString("yyyy-MM-dd");
            Boolean reference = false;
            if (cbRef.Checked)
            {
                reference = true;
            }
            DbConnect mysql = new DbConnect();


            //string sql2 = "INSERT INTO libary(ref) VALUES(1)";
             mysql.add(name, author, published, reference);


        }
        public static string GetDateFromDateTime(DateTime datevalue)
        {
            return datevalue.ToShortDateString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DbConnect mysql = new DbConnect();
            mysql.connect();
            mysql.close();

        }
        private void deleteAll()
        {
        



        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DbConnect mysql = new DbConnect();
            string name = txtName.Text;
            string author = txtAuthor.Text;
            List<string[]> arrayList = mysql.search(name, author);
            ListViewItem itm;
            foreach (string[] row in arrayList)
            {
                itm = new ListViewItem(row);
                listView1.Items.Add(itm);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            foreach (ListViewItem book in listView1.SelectedItems)
            {
                
                    Console.WriteLine(book.Text);
                DbConnect mysql = new DbConnect();
                string sql = " DELETE FROM libary WHERE book_id = '" + book.Text + "'";
                mysql.quickQuery(sql);
            }
        }
    }
}
