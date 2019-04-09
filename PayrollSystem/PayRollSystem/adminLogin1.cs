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
    public partial class adminLogin1 : Form
    {
        int withkoto = 0;
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public adminLogin1()
        {
            InitializeComponent();
        }

        private void adminLogin1_Load(object sender, EventArgs e)
        {
            loginSelect.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            String myquery = "select employeeId, employeePassword from employeeinfo where employeeId=@id and employeePassword=@pw";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@id", userUsername.Text);
            command.Parameters.AddWithValue("@pw", userPassword.Text);
            int value = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            if (value > 0)
            {
                this.Hide();
                employeeForm eForm = new employeeForm();
                eForm.idtouse = int.Parse(userUsername.Text);
                eForm.ShowDialog();
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Incorrect Id or Password!");
            }
        }

        private void loginSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loginSelect.SelectedIndex==1)
            {
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (loginSelect.SelectedIndex==1)
            {
                withkoto += 10;
                adminPanel.Width += 10;
                if (withkoto==440)
                {
                    adminSelectlogin.SelectedIndex = 1;
                    loginSelect.SelectedIndex = 0;
                    timer1.Stop();
                }
            }
            if (adminSelectlogin.SelectedIndex == 0)
            {
                withkoto -= 10;
                adminPanel.Width -= 10;
                if (withkoto == 0)
                {
                    adminSelectlogin.SelectedIndex = 1;
                    loginSelect.SelectedIndex = 0;
                    timer1.Stop();
                }
            }
        }

        private void adminSelectlogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adminSelectlogin.SelectedIndex == 0)
            {
                timer1.Start();
            }
        }

        private void userUsername_KeyPress(object sender, KeyPressEventArgs e)
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

        private void userUsername_Enter(object sender, EventArgs e)
        {
            if (userUsername.Text== "Employee ID")
            {
                userUsername.Text = "";
            }
        }

        private void userUsername_Leave(object sender, EventArgs e)
        {
            if (userUsername.Text == "")
            {
                userUsername.Text = "Employee ID";
            }
        }

        private void userPassword_Enter(object sender, EventArgs e)
        {
            if (userPassword.Text=="Password")
            {
                userPassword.Text = "";
                userPassword.PasswordChar = '*';
            }
        }

        private void userPassword_Leave(object sender, EventArgs e)
        {
            if (userPassword.Text == "")
            {
                userPassword.Text = "Password";
                userPassword.PasswordChar = '\0';
            }
        }

        private void adminUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void adminUsername_Enter(object sender, EventArgs e)
        {
            if (adminUsername.Text == "Employee ID")
            {
                adminUsername.Text = "";
            }
        }

        private void adminUsername_Leave(object sender, EventArgs e)
        {
            if (adminUsername.Text == "")
            {
                adminUsername.Text = "Employee ID";
            }
        }

        private void adminPassword_Enter(object sender, EventArgs e)
        {
            if (adminPassword.Text == "Password")
            {
                adminPassword.Text = "";
                adminPassword.PasswordChar = '*';
            }
        }

        private void adminPassword_Leave(object sender, EventArgs e)
        {
            if (adminPassword.Text == "")
            {
                adminPassword.Text = "Password";
                adminPassword.PasswordChar = '\0';
            }
        }

        private void attendanceBtn_Click(object sender, EventArgs e)
        {
            using (var attendance = new Attendance())
            {
                this.Hide();
                attendance.ShowDialog();
                this.Close();
                this.Dispose();
            }
        }

        private void adminLogin_Click(object sender, EventArgs e)
        {
            String myquery = "select employeeId, employeePassword from employeeinfo where employeeId=@id and employeePassword=@pw and employeePosition='Admin'";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@id", adminUsername.Text);
            command.Parameters.AddWithValue("@pw", adminPassword.Text);
            int value = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            if (value > 0)
            {
                this.Hide();
                payrollMain pmain = new payrollMain();
                pmain.ShowDialog();
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Incorrect Id or Password!");
            }
        }
    }
}
