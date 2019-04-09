using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace PayRollSystem
{
    public partial class dbInsert : Form
    {
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public dbInsert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String myquery = "INSERT INTO sss_table(minRange, maxRange, contribution) VALUES(@a, @b, @c)";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            command.Parameters.AddWithValue("@a", textBox1.Text);
            command.Parameters.AddWithValue("@b", textBox2.Text);
            command.Parameters.AddWithValue("@c", textBox3.Text);
		conn.Open();
            int value = command.ExecuteNonQuery();
            if (value == 1)
            {
                MessageBox.Show("UPDATED");
            }
            conn.Close();
        }
    }
}
