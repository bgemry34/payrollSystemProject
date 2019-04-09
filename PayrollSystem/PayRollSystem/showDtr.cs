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
    public partial class showDtr : Form
    {
        public String dateFrom = "";
        public String dateTo="";
        public String idTouse = "";
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");

        public showDtr()
        {
            InitializeComponent();

        }

        private void showDtr_Load(object sender, EventArgs e)
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
                    "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                    "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where employeeinfo.employeeId=@id and attendanceDate between @date1 and @date2";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            command2.Parameters.AddWithValue("@date1", dateFrom);
            command2.Parameters.AddWithValue("@date2", dateTo);
            command2.Parameters.AddWithValue("@id", idTouse);
            conn.Open();
            MySqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    dataGridView2.Rows.Add(new String[] { reader2[0].ToString(), reader2[2].ToString() + " " + reader2[3].ToString(), reader2[4].ToString(), reader2[5].ToString(), reader2[6].ToString() });
                }
            }
            conn.Close();
        }
    }
}
