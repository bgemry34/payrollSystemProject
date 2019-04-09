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
    public partial class payrollSummary : Form
    {
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public int payrollid = 0;
        public payrollSummary()
        {
            InitializeComponent();
        }

        private void payrollSummary_Load(object sender, EventArgs e)
        {
            String payrollquery = "Select * from payrollsummary where id=@id";
            MySqlCommand cmd = new MySqlCommand(payrollquery, conn);
            cmd.Parameters.AddWithValue("@id", payrollid);
            conn.Open();
            MySqlDataReader pread = cmd.ExecuteReader();
            if (pread.HasRows)
            {
                while (pread.Read())
                {
                    employeeId.Text = pread["employeeId"].ToString();
                    payrollDate.Text = pread["payrollDate"].ToString();
                    cutoffDate.Text = pread["fromDate"].ToString() + "-" + pread["toDate"].ToString();
                    presentDay.Text = pread["presentDay"].ToString();
                    workingHour.Text = pread["presentDay"].ToString();
                    otHourtxt.Text = pread["overtimeHour"].ToString();
                    workingHour.Text = pread["workingHour"].ToString();
                    latetxt.Text = pread["lateSummary"].ToString();
                    utimetxt.Text = pread["undertime"].ToString();
                    sss.Text = pread["sss"].ToString();
                    pagibig.Text = pread["pagibig"].ToString();
                    philHealth.Text = pread["philhealth"].ToString();
                    taxtxt.Text = pread["tax"].ToString();
                    cashAdvance.Text = pread["cashAdvance"].ToString();
                    other.Text = pread["other"].ToString();
                    grossPay.Text = pread["grossPay"].ToString();
                    netPay.Text = pread["netPay"].ToString();
                }
            }
            conn.Close();
            String empQuery = "Select employeeId, concat(employeeFirstName,' ',employeeLastName) as employeeName from employeeInfo where employeeId=@id";
            MySqlCommand cmd1 =new MySqlCommand(empQuery, conn);
            conn.Open();
            cmd1.Parameters.AddWithValue("@id", employeeId.Text);
            MySqlDataReader rd = cmd1.ExecuteReader();
            while (rd.Read())
            {
                employeeName.Text = rd["employeeName"].ToString();
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
