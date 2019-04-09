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
using System.IO;

namespace PayRollSystem
{
    public partial class employeeForm : Form
    {
        public int idtouse = 0;
        Boolean notif = false;
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public employeeForm()
        {
            InitializeComponent();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private static int GetNumberOfWorkingDays(DateTime startingDate, DateTime endingDate)
        {
            int num1 = 0;
            while (startingDate <= endingDate)
            {
                if (startingDate.DayOfWeek != DayOfWeek.Saturday && startingDate.DayOfWeek != DayOfWeek.Sunday)
                { num1++; }
                startingDate = startingDate.AddDays(1);
            }
            return num1;
        }

        private void notifBtn_Click(object sender, EventArgs e)
        {
            if (notif == false)
            {
                label2.Visible = false;
                groupBox1.Visible = true;
                String myquery2 = "SELECT employeeattendance.employeeId as id,employeeinfo.dateCreated as tocreate, COUNT(employeeattendance.attendanceDate) as presentDay FROM employeeattendance INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where employeeattendance.employeeid=@id and attendanceDate between @date1 and @date2 group by employeeattendance.employeeId";
                MySqlCommand command = new MySqlCommand(myquery2, conn);
                conn.Open();
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                DateTime firstDay = new DateTime(year, month, 1);
                command.Parameters.AddWithValue("@date1", firstDay);
                command.Parameters.AddWithValue("@date2", DateTime.Now);
                command.Parameters.AddWithValue("@id", idtouse.ToString());
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int numWorkingdays = GetNumberOfWorkingDays(firstDay, DateTime.Now.AddDays(-1));
                        DateTime createDate = DateTime.Parse(reader["tocreate"].ToString());
                        if (firstDay <= createDate)
                        {
                            numWorkingdays = GetNumberOfWorkingDays(createDate, DateTime.Now);
                        }
                        int noabsent = 0;
                        noabsent = numWorkingdays - int.Parse(reader["presentDay"].ToString());
                        if (noabsent < 0) { noabsent = 0; }

                        notiftxt.Text = "**Total Absent for this month is " + noabsent.ToString();
                    }
                }
                else
                {
                   int numWorkingdays = GetNumberOfWorkingDays(firstDay, DateTime.Now);
                   notiftxt.Text = "**Total Absent for this month is " + numWorkingdays.ToString();
                }
                conn.Close();
                notif = true;
            }
            else
            {
                groupBox1.Visible = false;
                notif = false;
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void employeeForm_Load(object sender, EventArgs e)
        {
            String loadinfo = "Select * from employeeinfo where employeeId=@id";
            MySqlCommand infocmd = new MySqlCommand(loadinfo, conn);
            infocmd.Parameters.AddWithValue("@id", idtouse.ToString());
            conn.Open();
            MySqlDataReader inforead = infocmd.ExecuteReader();
            if (inforead.HasRows)
            {
                while (inforead.Read())
                {
                    employeeName.Text += inforead["employeeFirstName"].ToString() + " " + inforead["employeeLastName"].ToString();
                    employeeGender.Text += inforead["employeeGender"].ToString();
                    employeeType.Text += inforead["employeeType"].ToString();
                    employeePostion.Text += inforead["employeePosition"].ToString();
                    maxrequest.Text = ((double.Parse(inforead["employeeRate"].ToString()) * 8) * 10).ToString();
                    employeePicture.Image= ByteArrayToImage((byte[])(inforead["employeePicture"]));
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtrPanel.Width = 865;
            homePanel.Width = 0;
            dtrTable.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
               "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
               "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where employeeattendance.employeeId=@id";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            command2.Parameters.AddWithValue("@id", idtouse.ToString());
            conn.Open();
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dtrTable.Rows.Add(new String[] { reader[0].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();
        }

        private void absentSearch_Click(object sender, EventArgs e)
        {
            dtrTable.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
               "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
               "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where employeeattendance.employeeId=@id and employeeattendance.attendanceDate between @date1 and @date2";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            command2.Parameters.AddWithValue("@id", idtouse.ToString());
            command2.Parameters.AddWithValue("@date1", dtrFrom.Value);
            command2.Parameters.AddWithValue("@date2", dtrTo.Value);
            conn.Open();
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dtrTable.Rows.Add(new String[] { reader[0].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();
        }

        private void backHome_Click(object sender, EventArgs e)
        {
            dtrPanel.Width = 0;
            homePanel.Width = 865;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cashRequestPanel.Width = 865;
            homePanel.Width = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.Parse(amountTxt.Text)>double.Parse(maxrequest.Text))
            {
                MessageBox.Show("Invalid Amount!");
            }
            else
            {
                String rqst = "insert into cashadvancerequest values(NULL, @employeeId, @reason, @amount)";
                MySqlCommand cmd = new MySqlCommand(rqst, conn);
                cmd.Parameters.AddWithValue("@employeeId", idtouse.ToString());
                cmd.Parameters.AddWithValue("@reason", reasonTxt.Text);
                cmd.Parameters.AddWithValue("@amount", amountTxt.Text);
                conn.Open();
                int value = cmd.ExecuteNonQuery();
                if (value == 1)
                {
                    MessageBox.Show("Request Successfully!");
                    reasonTxt.Text = "";
                    amountTxt.Text = "";
                }
                else
                {
                    MessageBox.Show("Request Fail!");
                }
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cashRequestPanel.Width = 0;
            homePanel.Width = 865;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminLogin1 loginform = new adminLogin1();
            loginform.ShowDialog();
            this.Close();
            this.Dispose();
        }
    }
}
