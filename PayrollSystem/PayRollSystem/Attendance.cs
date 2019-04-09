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
    public partial class Attendance : Form
    {
        public MySqlConnection conn= new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public Boolean loginlogoutBtn = false;
        public String employeeLogin = "";
       
        public Attendance()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timeTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            timeDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
                "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate=@date";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            conn.Open();
            command2.Parameters.AddWithValue("@id", employeeId.Text);
            command2.Parameters.AddWithValue("@date", DateTime.Today);
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(new String[] { reader[0].ToString(), reader[2].ToString() + " " + reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            timeDate.Text = DateTime.Now.ToString("MMMM-dd-yyyy");
        }

        //making binary BLOB in database to IMAGE TYPE
        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                        Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        //LOG IN LOG OUT BUTTON DISPAYING PICTURE AND NAME 
        private void button4_Click(object sender, EventArgs e)
        {
            if (loginlogoutBtn == false)
            {
                String myquery = "select * from employeeinfo where employeeId=@id";
                MySqlCommand command = new MySqlCommand(myquery, conn);
                conn.Open();
                command.Parameters.AddWithValue("@id", employeeId.Text);
                int value = Convert.ToInt32(command.ExecuteScalar());
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //making employee firstname and last name SESSION as login
                        employeeLogin = reader["employeeFirstName"].ToString()+" "+ reader["employeeLastName"].ToString();
                        //end off session

                        //Getting the BLOB picture in data base converting the binary blob to IMages
                        employeePicture.Image = ByteArrayToImage((byte[])(reader["employeePicture"]));
                    }
                }
                conn.Close();
                employeeName.Text = employeeLogin;
                if (value > 0)
                {
                    loginBtn.Text = "Log-out";
                    employeeId.Enabled = false;
                    loginlogoutBtn = true;
                    //show employee picture and name
                    employeeInfo.Visible = true;
                    //end of showing employee info and name

                    MessageBox.Show("Log-in Success!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    String myquery2 = "select count(*), employeeOut from employeeattendance where attendanceDate=@date and employeeId=@id";
                    MySqlCommand command2 = new MySqlCommand(myquery2, conn);
                    conn.Open();
                    command2.Parameters.AddWithValue("@date", DateTime.Today);
                    command2.Parameters.AddWithValue("@id", employeeId.Text);
                    int value2=0;
                    String hasValue="";
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            value2 = int.Parse(reader2[0].ToString()) ;
                            hasValue = reader2["employeeOut"].ToString();
                        }
                    }
                    conn.Close();
                    if (value2 == 0)
                    {
                        timeinbtn.Enabled = true;
                        timeoutbtn.Enabled = false;
                    }
                    else if (value2 >= 1)
                    {
                        
                        if (hasValue != "")
                        {
                            timeinbtn.Enabled = false;
                            timeoutbtn.Enabled = false;
                        }
                        else
                        {
                            timeinbtn.Enabled = false;
                            timeoutbtn.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Come Back Again Tomorrow!");
                    }
                }

                else
                {
                    MessageBox.Show("Incorrect Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                loginlogoutBtn = false;
                employeeId.Enabled = true;
                loginBtn.Enabled = true;
                loginBtn.Text = "Log-in";
                employeeId.Text = "";
                employeeLogin = "";
                timeinbtn.Enabled = false;
                timeoutbtn.Enabled = false;
                employeeInfo.Visible = false;
            }
        }

        private void timeinbtn_Click(object sender, EventArgs e)
        {
            String myquery = "INSERT INTO employeeattendance (employeeId, employeeIn, attendanceDate) VALUES(@id, @in, @date)";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@id", employeeId.Text);
            command.Parameters.AddWithValue("@in", timeTime.Text);
            command.Parameters.AddWithValue("@date", DateTime.Today);
            if (command.ExecuteNonQuery()==1)
            {
                conn.Close();
                //Display or Update The table of Attendance 
                dataGridView2.Rows.Clear();
                String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, "+
                    "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance "+
                    "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate=@date";
                MySqlCommand command2 = new MySqlCommand(myquery2, conn);
                conn.Open();
                command2.Parameters.AddWithValue("@id", employeeId.Text);
                command2.Parameters.AddWithValue("@date", DateTime.Today);
                MySqlDataReader reader = command2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(new String[] {reader[0].ToString(), reader[2].ToString() +" "+reader[3].ToString() , reader[4].ToString(), reader[5].ToString(), reader[6].ToString()});
                    }
                }
                timeinbtn.Enabled = false;
                timeoutbtn.Enabled = true;
                conn.Close();

            }


        }

        private void timeoutbtn_Click(object sender, EventArgs e)
        {
            String myquery = "update employeeattendance set employeeOut=@out where attendanceDate=@date and employeeId=@id";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@out", timeTime.Text);
            command.Parameters.AddWithValue("@id", employeeId.Text);
            command.Parameters.AddWithValue("@date", DateTime.Today);
            if (command.ExecuteNonQuery() == 1)
            {
                conn.Close();
                timeinbtn.Enabled = false;
                timeoutbtn.Enabled = false;
            }
            conn.Close();
            //refresh table
            dataGridView2.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName," +
                "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate=@date";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            conn.Open();
            command2.Parameters.AddWithValue("@id", employeeId.Text);
            command2.Parameters.AddWithValue("@date", DateTime.Today);
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(new String[] { reader[0].ToString(), reader[2].ToString()+" "+reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminLogin1 adminNow = new adminLogin1();
            this.Hide();
            adminNow.ShowDialog();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void employeeId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
    }
}
/*
            DateTime dateTo = DateTime.Parse("3:04:25 AM");
            DateTime etona = DateTime.Parse(DateTime.Now.ToString("hh:mm:ss tt"));
            TimeSpan afterTime = etona - dateTo;
            MessageBox.Show(afterTime.Hours.ToString());
*/
