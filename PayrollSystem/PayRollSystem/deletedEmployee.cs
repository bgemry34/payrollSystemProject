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
    public partial class deletedEmployee : Form
    {
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        payrollMain payrollMain;

        public deletedEmployee(payrollMain form1)
        {
            InitializeComponent();
            this.payrollMain = form1;
        }

        private void deletedEmployee_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "";
            btnEdit.Text = "Recover";
            btnEdit.Name = "btnEdit";
            btnEdit.CellTemplate.Style.BackColor = Color.FromArgb(33, 136, 56);
            btnEdit.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnEdit.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnEdit.CellTemplate.Style.Font = new Font("Verdana", 10, FontStyle.Regular);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnEdit.UseColumnTextForButtonValue = true;

            employeeMngtable.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "";
            btnDelete.Text = "Delete";
            btnDelete.Name = "btnDelete";
            btnDelete.CellTemplate.Style.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnDelete.CellTemplate.Style.Font = new Font("Verdana", 10, FontStyle.Regular);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.CellTemplate.Style.SelectionBackColor = Color.FromArgb(220, 53, 69);
            btnDelete.UseColumnTextForButtonValue = true;
            employeeMngtable.Columns.Add(btnDelete);

            employeeMngtable.Rows.Clear();
            String showemployeequery = "SELECT * from delete_employeeinfo";
            MySqlCommand showemployeecommand = new MySqlCommand(showemployeequery, conn);
            conn.Open();
            MySqlDataReader showemployeereader = showemployeecommand.ExecuteReader();
            if (showemployeereader.HasRows)
            {
                while (showemployeereader.Read())
                {
                    employeeMngtable.Rows.Add(new String[] { showemployeereader["employeeId"].ToString(), showemployeereader["employeeFirstName"].ToString(), showemployeereader["employeeLastName"].ToString(), showemployeereader["employeeType"].ToString(), showemployeereader["employeePosition"].ToString(), showemployeereader["employeeRate"].ToString() });
                }
            }
            conn.Close();
        }

        private void employeeMngtable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = employeeMngtable.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = employeeMngtable.Rows[selectedrowindex];
            if (e.ColumnIndex == 6)
            {
                DialogResult diagRes = MessageBox.Show("Are You Sure You wanna Recover This Employee?", "Recover", MessageBoxButtons.YesNo);
                if (diagRes == DialogResult.Yes)
                {
                    //Insert into delete employee archive
                    String employeeRet = "Insert into employeeinfo select * from delete_employeeinfo where employeeId=@id";
                    MySqlCommand retcommand = new MySqlCommand(employeeRet, conn);
                    conn.Open();
                    retcommand.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    retcommand.ExecuteNonQuery();

                    //deleting employee info 
                    String employeeDelete = "Delete From delete_employeeinfo Where employeeId=@id";
                    MySqlCommand deletecommand = new MySqlCommand(employeeDelete, conn);
                    deletecommand.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int x = deletecommand.ExecuteNonQuery();

                    if (x == 1) { MessageBox.Show("Delete Successfully!"); payrollMain.refreshEmployeeTable(); }
                    else { MessageBox.Show("Delete Fail!"); }
                    conn.Close();

                    //refresh table again
                    employeeMngtable.Rows.Clear();
                    String showemployeequery = "SELECT * from delete_employeeinfo";
                    MySqlCommand showemployeecommand = new MySqlCommand(showemployeequery, conn);
                    conn.Open();
                    MySqlDataReader showemployeereader = showemployeecommand.ExecuteReader();
                    if (showemployeereader.HasRows)
                    {
                        while (showemployeereader.Read())
                        {
                            employeeMngtable.Rows.Add(new String[] { showemployeereader["employeeId"].ToString(), showemployeereader["employeeFirstName"].ToString(), showemployeereader["employeeLastName"].ToString(), showemployeereader["employeeType"].ToString(), showemployeereader["employeePosition"].ToString(), showemployeereader["employeeRate"].ToString() });
                        }
                    }
                    conn.Close();
                }
            }

            //delete perma
            if (e.ColumnIndex == 7)
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You wanna Delete This Employee Permanently?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //deleting employee info 
                    String employeeDelete = "Delete From delete_employeeinfo Where employeeId=@id";
                    MySqlCommand deletecommand = new MySqlCommand(employeeDelete, conn);
                    deletecommand.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int x = deletecommand.ExecuteNonQuery();

                    if (x == 1) { MessageBox.Show("Delete Successfully!");  payrollMain.refreshEmployeeTable(); }
                    else { MessageBox.Show("Delete Fail!"); }
                    conn.Close();
                }
            }
        }
    }
}
