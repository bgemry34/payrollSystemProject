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
    public partial class employeeInfo : Form
    {
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        Attendance attenance = new Attendance();
        public int employeeIdtouse = 0;
        public employeeInfo()
        {
            InitializeComponent();
        }

        private void employeeInfo_Load(object sender, EventArgs e)
        {
            //query all employee info by employee info
 
            String employeeInfoQuery = "select * from employeeInfo where employeeId = @employeeId";
            MySqlCommand employeeInfoCommand = new MySqlCommand(employeeInfoQuery, conn);
            conn.Open();
            employeeInfoCommand.Parameters.AddWithValue("@employeeId", employeeIdtouse.ToString());
            MySqlDataReader employeeInfoReader = employeeInfoCommand.ExecuteReader();
            if (employeeInfoReader.HasRows)
            {
                while (employeeInfoReader.Read())
                {
                    employeePicture.Image = attenance.ByteArrayToImage((byte[])(employeeInfoReader["employeePicture"]));
                    employeeId.Text = employeeInfoReader["employeeId"].ToString();
                    employeePassword.Text = employeeInfoReader["employeePassword"].ToString();
                    employeeFirstName.Text = employeeInfoReader["employeeFirstName"].ToString();
                    employeeLastName.Text = employeeInfoReader["employeeLastName"].ToString();
                    employeeGender.Text = employeeInfoReader["employeeGender"].ToString();
                    employeeType.Text = employeeInfoReader["employeeType"].ToString();
                    employeePosition.Text = employeeInfoReader["employeePosition"].ToString();
                    employeeRate.Text = employeeInfoReader["employeeRate"].ToString();

                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.employeePassword.PasswordChar = '\0';
            }
            else
            {
                employeePassword.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
