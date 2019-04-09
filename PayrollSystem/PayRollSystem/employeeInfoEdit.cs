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
    public partial class employeeInfoEdit : Form
    {
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");

        public int idtouse = 0;
        public payrollMain payrollmain;
        public bool employeeEditMode = false;
        public bool employeeAddMode = false;
        public bool haspicture = false;

        public employeeInfoEdit(payrollMain payrollMain)
        {
            InitializeComponent();
            this.payrollmain = payrollMain;
        }

        private void employeeInfoEdit_Load(object sender, EventArgs e)
        {
            //employee Postion
            String postionQuery = "Select positionName from employeeposition_table ORDER BY positionName";
            MySqlCommand poscmd = new MySqlCommand(postionQuery, conn);
            conn.Open();
            MySqlDataReader posrdr = poscmd.ExecuteReader();
            if (posrdr.HasRows)
            {
                while (posrdr.Read())
                {
                    employeePosition.Items.Add(posrdr[0].ToString());   
                }
            }
            conn.Close();

            if (employeeEditMode==true)
            {
                //query all employee info by employee info
                Attendance attenance = new Attendance();
                updateBtn.Text = "Update Employee";
                String employeeInfoQuery = "select * from employeeInfo where employeeId = @employeeId";
                MySqlCommand employeeInfoCommand = new MySqlCommand(employeeInfoQuery, conn);
                conn.Open();
                employeeInfoCommand.Parameters.AddWithValue("@employeeId", idtouse.ToString());
                MySqlDataReader employeeInfoReader = employeeInfoCommand.ExecuteReader();
                if (employeeInfoReader.HasRows)
                {
                    while (employeeInfoReader.Read())
                    {
                        employeePicture.Image = new Bitmap(attenance.ByteArrayToImage((byte[])(employeeInfoReader["employeePicture"])));
                        employeeId.Text = employeeInfoReader["employeeId"].ToString();
                        employeePassword.Text = employeeInfoReader["employeePassword"].ToString();
                        employeeFirstName.Text = employeeInfoReader["employeeFirstName"].ToString();
                        employeeLastName.Text = employeeInfoReader["employeeLastName"].ToString();
                        if (employeeInfoReader["employeeGender"].ToString() == "Male")
                        {
                            employeeGender.SelectedIndex = 0;
                        }
                        else
                        {
                            employeeGender.SelectedIndex = 1;
                        }
                        employeeType.SelectedItem = employeeInfoReader["employeeType"].ToString();
                        employeePosition.SelectedItem = employeeInfoReader["employeePosition"].ToString();
                        employeeRate.Text = employeeInfoReader["employeeRate"].ToString();

                    }
                }
                conn.Close();
            }
            if (employeeAddMode==true)
            {
                updateBtn.Text = "Add Employee";
                employeeGender.SelectedIndex = 0;
                employeeType.SelectedIndex = 0;
                employeePosition.SelectedIndex = 0;
                //generate Id 
                String idquery = "select employeeId FROM ( SELECT employeeId as employeeId FROM employeeinfo UNION SELECT employeeId as employeeId from delete_employeeinfo ) as result GROUP by employeeId ORDER BY employeeId DESC LIMIT 1";
                MySqlCommand genadd = new MySqlCommand(idquery, conn);
                conn.Open();
                MySqlDataReader genreader = genadd.ExecuteReader();
                while (genreader.Read())
                {
                    employeeId.Text = (int.Parse(genreader[0].ToString())+1).ToString();
                }
                conn.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to upload.";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                employeePicture.Image = new Bitmap(dialog.FileName);
                haspicture = true;
            }
        }

        private void employeePosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public byte[] ImageToByteArray(Image img)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (employeeEditMode == true)
            {
                String Warning = "";
                if (employeePassword.Text == string.Empty) { Warning += "*Password Field is Empty\n"; }
                if (employeeFirstName.Text == string.Empty) { Warning += "*First Name Field is Empty\n"; }
                if (employeeLastName.Text == string.Empty) { Warning += "*Last Name Field is Empty\n"; }
                if (employeeRate.Text == string.Empty) { Warning += "*Employee Rate Field is Empty\n"; }
                if (employeePassword.Text == string.Empty || employeeFirstName.Text == string.Empty || employeeLastName.Text == string.Empty || employeeRate.Text == string.Empty)
                {
                    MessageBox.Show(Warning);
                }
                else
                {
                    //update the employee info
                    String updatequery = "update employeeInfo set employeePassword=@pass, employeeFirstName=@Fname, employeeLastName=@Lname, employeeGender=@gender, employeeType=@eType, employeePosition=@ePosition, employeeRate=@eRate, employeePicture=@ePicture where employeeId=@id";
                    MySqlCommand updatecommand = new MySqlCommand(updatequery, conn);
                    conn.Open();

                    updatecommand.Parameters.AddWithValue("@id", employeeId.Text);
                    updatecommand.Parameters.AddWithValue("@pass", employeePassword.Text);
                    updatecommand.Parameters.AddWithValue("@Fname", employeeFirstName.Text);
                    updatecommand.Parameters.AddWithValue("@Lname", employeeLastName.Text);
                    updatecommand.Parameters.AddWithValue("@gender", employeeGender.Text);
                    updatecommand.Parameters.AddWithValue("@eType", employeeType.Text);
                    updatecommand.Parameters.AddWithValue("@ePosition", employeePosition.Text);
                    updatecommand.Parameters.AddWithValue("@eRate", employeeRate.Text);
                    updatecommand.Parameters.AddWithValue("@ePicture", ImageToByteArray(employeePicture.Image));
                    int value = updatecommand.ExecuteNonQuery();
                    conn.Close();
                    if (value == 1)
                    { 
                    MessageBox.Show("Update Successfully");
                    payrollmain.refreshEmployeeTable();
                        this.Close();
                     }
                    else
                        MessageBox.Show("Update Failed");
                }
            }
            if (employeeAddMode == true)
            {
                //validation
                String Warning = "";
                if (employeePassword.Text == string.Empty) { Warning += "*Password Field is Empty\n"; }
                if (employeeFirstName.Text == string.Empty) { Warning += "*First Name Field is Empty\n"; }
                if (employeeLastName.Text == string.Empty) { Warning += "*Last Name Field is Empty\n"; }
                if (employeeRate.Text == string.Empty) { Warning += "*Employee Rate Field is Empty\n"; }
                if (employeePassword.Text == string.Empty || employeeFirstName.Text == string.Empty || employeeLastName.Text == string.Empty || employeeRate.Text == string.Empty)
                {
                    MessageBox.Show(Warning);
                }

                else
                {
                    //add employee query
                    String Addquery = "Insert Into employeeinfo values(@id, @pass, @fname, @lname, @type, @pos, @rate, @gender, @pic, curdate())";
                    MySqlCommand addCommand = new MySqlCommand(Addquery, conn);
                    conn.Open();
                    addCommand.Parameters.AddWithValue("@id", employeeId.Text);
                    addCommand.Parameters.AddWithValue("@pass", employeePassword.Text);
                    addCommand.Parameters.AddWithValue("@fname", employeeFirstName.Text);
                    addCommand.Parameters.AddWithValue("@lname", employeeLastName.Text);
                    addCommand.Parameters.AddWithValue("@type", employeeType.Text);
                    addCommand.Parameters.AddWithValue("@pos", employeePosition.Text);
                    addCommand.Parameters.AddWithValue("@rate", employeeRate.Text);
                    addCommand.Parameters.AddWithValue("@gender", employeeGender.Text);
                    addCommand.Parameters.AddWithValue("@pic", ImageToByteArray(employeePicture.Image));
                    int confirm = addCommand.ExecuteNonQuery();
                    if (confirm == 1)
                    {
                        MessageBox.Show("Added Successfully!");
                        this.Close();
                        payrollmain.refreshEmployeeTable();
                    }
                    else
                    {
                        MessageBox.Show("Fail!");
                    }
                }
            }
        }

        private void employeeRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void employeeFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void employeeLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void employeeGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeeAddMode==true)
            {
                if (employeeGender.SelectedIndex == 0 && haspicture == false)
                {
                    employeePicture.Image = new Bitmap(PayRollSystem.Properties.Resources.male);
                }
                else if (employeeGender.SelectedIndex == 1 && haspicture == false)
                {
                    employeePicture.Image = new Bitmap(PayRollSystem.Properties.Resources.female);
                }
            }
        }
    }
}
