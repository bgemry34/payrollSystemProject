using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Word=Microsoft.Office.Interop.Word;



namespace PayRollSystem
{
    public partial class payrollMain : Form
    {
        public String idUsed = "";
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public Boolean payrollPanel = true;
        public Boolean dtrCover =false;
        public payrollMain()
        {
            InitializeComponent();
        }


        private void payrollMain_Load(object sender, EventArgs e)
        {
            employeePayroll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            employeePayroll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            employeeMngtable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            employeeMngtable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            employeeMngtable.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);

            DataGridViewButtonColumn btnShowpr = new DataGridViewButtonColumn();
            btnShowpr.HeaderText = "";
            btnShowpr.Text = "Show";
            btnShowpr.Name = "btnEdit";
            btnShowpr.CellTemplate.Style.BackColor = Color.FromArgb(91, 192, 222);
            btnShowpr.CellTemplate.Style.SelectionBackColor = Color.FromArgb(91, 192, 222);
            btnShowpr.CellTemplate.Style.ForeColor = Color.White;
            btnShowpr.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnShowpr.FlatStyle = FlatStyle.Flat;
            btnShowpr.UseColumnTextForButtonValue = true;
            payrollTable.Columns.Add(btnShowpr);

            DataGridViewButtonColumn btnShow = new DataGridViewButtonColumn();
            btnShow.HeaderText = "";
            btnShow.Text = "Show";
            btnShow.Name = "btnEdit";
            btnShow.CellTemplate.Style.BackColor = Color.FromArgb(91, 192, 222);
            btnShow.CellTemplate.Style.SelectionBackColor = Color.FromArgb(91, 192, 222);
            btnShow.CellTemplate.Style.ForeColor = Color.White;
            btnShow.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnShow.FlatStyle = FlatStyle.Flat;
            btnShow.UseColumnTextForButtonValue = true;
            employeeMngtable.Columns.Add(btnShow);

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "";
            btnEdit.Text = "Edit";
            btnEdit.Name = "btnEdit";
            btnEdit.CellTemplate.Style.BackColor = Color.FromArgb(33, 136, 56);
            btnEdit.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnEdit.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnEdit.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnEdit.UseColumnTextForButtonValue = true;
            employeeMngtable.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnAccept = new DataGridViewButtonColumn();
            btnAccept.HeaderText = "";
            btnAccept.Text = "Accept";
            btnAccept.Name = "AcceptBtn";
            btnAccept.CellTemplate.Style.BackColor = Color.FromArgb(33, 136, 56);
            btnAccept.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnAccept.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnAccept.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.CellTemplate.Style.SelectionBackColor = Color.FromArgb(33, 136, 56);
            btnAccept.UseColumnTextForButtonValue = true;
            requestTable.Columns.Add(btnAccept);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "";
            btnDelete.Text = "Delete";
            btnDelete.Name = "btnDelete";
            btnDelete.CellTemplate.Style.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnDelete.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.CellTemplate.Style.SelectionBackColor = Color.FromArgb(220, 53, 69);
            btnDelete.UseColumnTextForButtonValue = true;
            employeeMngtable.Columns.Add(btnDelete);

            DataGridViewButtonColumn btnDecline = new DataGridViewButtonColumn();
            btnDecline.HeaderText = "";
            btnDecline.Text = "Decline";
            btnDecline.Name = "declineBtn";
            btnDecline.CellTemplate.Style.BackColor = Color.FromArgb(220, 53, 69);
            btnDecline.CellTemplate.Style.ForeColor = Color.WhiteSmoke;
            btnDecline.CellTemplate.Style.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.CellTemplate.Style.SelectionBackColor = Color.FromArgb(220, 53, 69);
            btnDecline.UseColumnTextForButtonValue = true;
            requestTable.Columns.Add(btnDecline);

            calculateBtn.Enabled = false;
            employeePayroll.AllowUserToResizeRows = false;
            dateFrom.Value = DateTime.Now;
            dateTo.MinDate = dateFrom.Value.AddDays(1);
            dateTo.Value = dateFrom.Value.AddDays(1);
            String myquery = "select employeeId, employeeFirstName, employeeLastName from employeeinfo";
            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employeePayroll.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString()+" "+reader[2].ToString() });
                }
            }
            conn.Close();
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTo.MinDate = dateFrom.Value.AddDays(1);
            generatePayroll.Enabled = false;
            calculateBtn.Enabled = false;
            showRecord.Enabled = false;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            String myquery = "select * from employeeinfo where employeeId=@id";
            double totalHour = 0;
            double overTime = 0;
            netPay.Text = "0";
            grossPay.Text = "0";
            allowableAbsences.Text = "3";


            presentDay.Text = "0";
            latetxt.Text = "0";
            utimetxt.Text = "0";

            MySqlCommand command = new MySqlCommand(myquery, conn);
            conn.Open();
            command.Parameters.AddWithValue("@id", employeeId.Text);
            int value = Convert.ToInt32(command.ExecuteScalar());
            if (value > 0)
            {
                idUsed = employeeId.Text;
                //Enable Button Calculate and SHOW DTR
                calculateBtn.Enabled = true;
                showRecord.Enabled = true;

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Output from database getting values
                        employeeName.Text = reader["employeeFirstname"].ToString()+" "+reader["employeeLastName"].ToString();
                        employeePosition.Text = reader["employeePosition"].ToString();
                        employeeType.Text = reader["employeeType"].ToString();
                        rateperhour.Text = reader["employeeRate"].ToString();
                        otRate.Text = (double.Parse(rateperhour.Text) * 1.3).ToString();
                    }
                }
                conn.Close();
                String date1 = dateFrom.Value.ToString("yyyy-MM-dd");
                String date2 = dateTo.Value.ToString("yyyy-MM-dd");
                //for data time and date of employee
                String myquery2 = "select * from employeeattendance where employeeId=@id and  attendanceDate between @date1 and @date2";
                //for number of day attend
                String myquery3 = "select count(*) from employeeattendance where employeeId=@id and  attendanceDate between @date1 and @date2";

                MySqlCommand command2 = new MySqlCommand(myquery2, conn);

                MySqlCommand command3 = new MySqlCommand(myquery3, conn);
            
                command2.Parameters.AddWithValue("@date1", date1);
                command2.Parameters.AddWithValue("@date2", date2);
                command2.Parameters.AddWithValue("@id", idUsed);

                command3.Parameters.AddWithValue("@date1", date1);
                command3.Parameters.AddWithValue("@date2", date2);
                command3.Parameters.AddWithValue("@id", employeeId.Text);
                conn.Open();
                //number of present day & computing no. absent
                int prensentday = Convert.ToInt32(command3.ExecuteScalar());
                int numWorkingdays = GetNumberOfWorkingDays(dateFrom.Value, dateTo.Value);
                int noAbsent = 0;
                double absentComputation = 0;

                if (numWorkingdays > prensentday) {
                    noAbsent = (numWorkingdays - prensentday);
                    employeeAbsences.Text = noAbsent.ToString();

                    if (int.Parse(employeeAbsences.Text) > 3) {
                        absentComputation = (noAbsent - 3) * (double.Parse(rateperhour.Text)*8);
                        absentReduction.Text = absentComputation.ToString(); }

                }
                else { employeeAbsences.Text = "0"; }
                MySqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        
                        presentDay.Text = prensentday.ToString();
                        DateTime theAttendanceDate = DateTime.Parse(reader2["attendanceDate"].ToString());
                        if ((theAttendanceDate.DayOfWeek == DayOfWeek.Saturday) || (theAttendanceDate.DayOfWeek == DayOfWeek.Sunday))
                        {
                            Console.WriteLine("This is a weekend");
                        }
                        else
                        {
                            DateTime dateTo = DateTime.Parse(reader2["employeeIn"].ToString());
                            DateTime dateFrom = DateTime.Parse(reader2["employeeOut"].ToString());
                            DateTime timeTowork = DateTime.Parse("08:00:00 AM");
                            DateTime timetoend = DateTime.Parse("04:00:00 PM");
                            TimeSpan afterTime = dateFrom - dateTo;
                            // MessageBox.Show(afterTime.TotalHours.ToString());
                            if (double.Parse(afterTime.TotalHours.ToString()) <= 8)
                            { totalHour += double.Parse(afterTime.TotalHours.ToString()); }
                            else
                            {
                                if ((double.Parse(afterTime.TotalHours.ToString()) - 8) > .5)
                                { overTime += (double.Parse(afterTime.TotalHours.ToString()) - 8); }

                                totalHour += 8;
                            }

                            //late
                            TimeSpan latelate = dateTo - timeTowork;
                            TimeSpan underTime = timetoend-dateFrom;
                            if (Double.Parse(latelate.TotalHours.ToString("00.00")) >= 0.5)
                            {
                                latetxt.Text = (Double.Parse(rateperhour.Text) * Double.Parse(latelate.TotalHours.ToString("00.00"))).ToString();
                            }

                            if(Double.Parse(underTime.TotalHours.ToString("00.00")) >= 0.5)
                            {
                                utimetxt.Text= (Double.Parse(rateperhour.Text) * Double.Parse(underTime.TotalHours.ToString("00.00"))).ToString();
                            }
                        }
                    }
                }
                workingHour.Text = totalHour.ToString("00.00");
                otHourtxt.Text = overTime.ToString("00.00");
                conn.Close();

                //computing sss/pagibig/
                
                //base values
                Double payrollGross = ((double.Parse(rateperhour.Text) * double.Parse(workingHour.Text)) + (double.Parse(otRate.Text) * double.Parse(otHourtxt.Text)));
                //sss
                String sssQuery = "select * from sss_table";
                MySqlCommand sssCommmand = new MySqlCommand(sssQuery, conn);
                conn.Open();
                MySqlDataReader sssreader = sssCommmand.ExecuteReader();
                 if (sssreader.HasRows)
                {
                    while (sssreader.Read())
                    {
                        if(double.Parse(sssreader[1].ToString())<=payrollGross && payrollGross <= double.Parse(sssreader[2].ToString()))
                        {
                            sss.Text = sssreader[3].ToString();
                            break;
                        }
                    }
                }
                conn.Close();

                //pag-ibig
                String pagIbig = "select * from pagibig_table";
                MySqlCommand pagibigCommand = new MySqlCommand(pagIbig, conn);
                conn.Open();
                MySqlDataReader pagibigreader = pagibigCommand.ExecuteReader();
                if (pagibigreader.HasRows)
                {
                    while (pagibigreader.Read())
                    {
                        pagibig.Text=pagibigreader[0].ToString();
                    }
                }
                conn.Close();


                //phil-health
                String philhealthquery = "select * from philhealth_table";
                MySqlCommand philhealthCommand = new MySqlCommand(philhealthquery, conn);
                conn.Open();
                MySqlDataReader phReader = philhealthCommand.ExecuteReader();
                if (phReader.HasRows)
                {
                    while (phReader.Read())
                    {
                        if (double.Parse(phReader[1].ToString()) <= payrollGross && payrollGross <= double.Parse(phReader[2].ToString()))
                        {
                            philHealth.Text = phReader[3].ToString();
                            break;
                        }
                    }
                }
                conn.Close();


                //tax computation
                double taxableincome = payrollGross-double.Parse(latetxt.Text)-double.Parse(utimetxt.Text)-double.Parse(absentReduction.Text)-double.Parse(sss.Text)-double.Parse(pagibig.Text)-double.Parse(philHealth.Text);
                TimeSpan totalDays = dateTo.Value-dateFrom.Value;
                double numofdays = double.Parse(totalDays.Days.ToString());

                String period = "";
                if (numofdays < 7) { period = "Daily"; }
                else if (numofdays >= 7 && numofdays <= 13) { period = "Weekly"; }
                else if (numofdays >= 14 && 30 > numofdays) { period = "Semi-Monthly"; }
                else { period = "Monthly"; }

                String taxQuery = "select * from bir_table where payPeriod=@day";
                MySqlCommand taxCommand = new MySqlCommand(taxQuery, conn);
                taxCommand.Parameters.AddWithValue("@day", period);
                conn.Open();
                MySqlDataReader taxReader = taxCommand.ExecuteReader();
                if (taxReader.HasRows)
                {
                    while (taxReader.Read())
                    {
                        if (double.Parse(taxReader[2].ToString()) <= taxableincome && taxableincome <= double.Parse(taxReader[3].ToString()))
                        {
                            taxtxt.Text = (((taxableincome - double.Parse(taxReader[2].ToString())) * double.Parse(taxReader[4].ToString())) + double.Parse(taxReader[5].ToString())).ToString();
                            break;
                        }
                    }
                }
                conn.Close();

                //Cash Advance
                String tocashAdvance = "Select * from cashadvance where employeeId=@id and Balance>0 limit 1";
                MySqlCommand advcmd = new MySqlCommand(tocashAdvance, conn);
                advcmd.Parameters.AddWithValue("@id", employeeId.Text);
                conn.Open();
                MySqlDataReader advRead = advcmd.ExecuteReader();
                if (advRead.HasRows)
                {
                    while (advRead.Read())
                    {
                        cashAdvance.Text = (double.Parse(advRead["Debit"].ToString()) * .25).ToString();
                    }
                }
                conn.Close();

                
            }
            else
            {
                MessageBox.Show("Invalid Id!");
            }

            conn.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            PayrollPanel.Width = 0;
            dtrcontainer.Width = 1060;
            manageEmployeePanel.Width = 0;
            deductionPanel.Width = 0;
            payrollSummary.Width = 0;
            cashAdvancePanel.Width = 0;

            dtrTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtrTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtrTable.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
                "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            conn.Open();
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dtrTable.Rows.Add(new String[] { reader[0].ToString(), reader[2].ToString() + " " + reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();

            dtrTo.MinDate = dtrFrom.Value.AddDays(1);


        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void employeePayroll_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            employeeId.Text = employeePayroll.CurrentRow.Cells[0].FormattedValue.ToString();
        }

        private void employeePayroll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            employeeId.Text = employeePayroll.CurrentRow.Cells[0].FormattedValue.ToString();
        }

        private void gotoPayroll_Click(object sender, EventArgs e)
        {
                PayrollPanel.Width = 1060;
                dtrcontainer.Width = 0;
                manageEmployeePanel.Width = 0;
                deductionPanel.Width = 0;
                payrollSummary.Width = 0;
                cashAdvancePanel.Width = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            generatePayroll.Enabled = true;
            grossPay.Text = ((double.Parse(rateperhour.Text) * double.Parse(workingHour.Text)) + (double.Parse(otRate.Text) * double.Parse(otHourtxt.Text))).ToString();
            netPay.Text = (Double.Parse(grossPay.Text) - double.Parse(latetxt.Text) - double.Parse(utimetxt.Text) - double.Parse(absentReduction.Text) - double.Parse(sss.Text) - double.Parse(pagibig.Text) - 
                double.Parse(philHealth.Text) - double.Parse(taxtxt.Text) - double.Parse(cashAdvance.Text) - double.Parse(other.Text)).ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are You Sure You wanna Generate Payroll?", "Payroll", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //cash advance
                String updateCashAdvance = "Update cashadvance set credit=credit+@cash, balance=balance-@cash where employeeId=@id limit 1";
                MySqlCommand cashcmd = new MySqlCommand(updateCashAdvance, conn);
                cashcmd.Parameters.AddWithValue("@cash", cashAdvance.Text);
                cashcmd.Parameters.AddWithValue("@id", employeeId.Text);
                conn.Open();
                cashcmd.ExecuteNonQuery();
                conn.Close();
                
                //payroll summary insert
                String addtosummary = "insert into payrollsummary values(NULL, @paydate, @employeeId, @fromdate, @todate, @presentday ,@workinghour, @overtime, @late, @under, @absences, @sss, @pagibig, @philhealth, @tax, @cashAdvance, @other, @grosspay, @netpay)";
                MySqlCommand addCommand = new MySqlCommand(addtosummary, conn);
                conn.Open();
                addCommand.Parameters.AddWithValue("@employeeId", employeeId.Text);
                addCommand.Parameters.AddWithValue("@paydate", DateTime.Now);
                addCommand.Parameters.AddWithValue("@fromdate", dateFrom.Value);
                addCommand.Parameters.AddWithValue("@todate", dateTo.Value);
                addCommand.Parameters.AddWithValue("@presentday", presentDay.Text);
                addCommand.Parameters.AddWithValue("@workinghour", workingHour.Text);
                addCommand.Parameters.AddWithValue("@overtime", otHourtxt.Text);
                addCommand.Parameters.AddWithValue("@late", latetxt.Text);
                addCommand.Parameters.AddWithValue("@under", utimetxt.Text);
                addCommand.Parameters.AddWithValue("@absences", absentReduction.Text);
                addCommand.Parameters.AddWithValue("@sss", sss.Text);
                addCommand.Parameters.AddWithValue("@pagibig", pagibig.Text);
                addCommand.Parameters.AddWithValue("@philhealth", philHealth.Text);
                addCommand.Parameters.AddWithValue("@tax", taxtxt.Text);
                addCommand.Parameters.AddWithValue("@cashAdvance", cashAdvance.Text);
                addCommand.Parameters.AddWithValue("@other", other.Text);
                addCommand.Parameters.AddWithValue("@grosspay", grossPay.Text);
                addCommand.Parameters.AddWithValue("@netpay", netPay.Text);
                int confirm = addCommand.ExecuteNonQuery();
                if (confirm==1)
                {
                    MessageBox.Show("Added to payroll Summary!");
                    String filename = Path.GetTempFileName();
                    File.WriteAllBytes(filename, Properties.Resources.payslip);

                    Word.Application wordApp = new Word.Application();
                    object missing = System.Reflection.Missing.Value;
                    Word.Document myWordDoc = null;
                    object fname = filename;
                    object readOnly = false;
                    object isVisible = true;
                    wordApp.Visible = false;
                    myWordDoc = wordApp.Documents.Open(ref fname, ref missing, ref readOnly); 
                    
                    if (myWordDoc.Bookmarks.Exists("absencesDeduction"))
                    {
                        object oBookMark = "absencesDeduction";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = absentReduction.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("payrollDate"))
                    {
                        object oBookMark = "payrollDate";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = DateTime.Today.ToShortDateString();
                    }

                    if (myWordDoc.Bookmarks.Exists("cashAdvance"))
                    {
                        object oBookMark = "cashAdvance";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = cashAdvance.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("dateFrom"))
                    {
                        object oBookMark = "dateFrom";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = dateFrom.Value.ToShortDateString();
                    }

                    if (myWordDoc.Bookmarks.Exists("dateTo"))
                    {
                        object oBookMark = "dateTo";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = dateTo.Value.ToShortDateString();
                    }

                    if (myWordDoc.Bookmarks.Exists("employeeAbsences"))
                    {
                        object oBookMark = "employeeAbsences";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = employeeAbsences.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("employeeId"))
                    {
                        object oBookMark = "employeeId";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = employeeId.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("employeeName"))
                    {
                        object oBookMark = "employeeName";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = employeeName.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("employeePosition"))
                    {
                        object oBookMark = "employeePosition";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = employeePosition.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("employeeType"))
                    {
                        object oBookMark = "employeeType";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = employeeType.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("grossPay"))
                    {
                        object oBookMark = "grossPay";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = grossPay.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("late"))
                    {
                        object oBookMark = "late";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = latetxt.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("netPay"))
                    {
                        object oBookMark = "netPay";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = netPay.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("other"))
                    {
                        object oBookMark = "other";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = other.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("overtimePay"))
                    {
                        object oBookMark = "overtimePay";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = (double.Parse(otHourtxt.Text)*double.Parse(otRate.Text)).ToString();
                    }

                    if (myWordDoc.Bookmarks.Exists("pagibig"))
                    {
                        object oBookMark = "pagibig";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = pagibig.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("philHealth"))
                    {
                        object oBookMark = "philHealth";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = philHealth.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("presentDay"))
                    {
                        object oBookMark = "presentDay";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = presentDay.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("sss"))
                    {
                        object oBookMark = "sss";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = sss.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("tax"))
                    {
                        object oBookMark = "tax";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = taxtxt.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("totalDeduction"))
                    {
                        object oBookMark = "totalDeduction";
                        String totaldeduct= (double.Parse(latetxt.Text) + double.Parse(utimetxt.Text) + double.Parse(absentReduction.Text) + double.Parse(sss.Text) + double.Parse(pagibig.Text) +
                double.Parse(philHealth.Text) + double.Parse(taxtxt.Text) + double.Parse(cashAdvance.Text) + double.Parse(other.Text)).ToString();
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = totaldeduct;
                    }

                    if (myWordDoc.Bookmarks.Exists("undertime"))
                    {
                        object oBookMark = "undertime";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = utimetxt.Text;
                    }

                    if (myWordDoc.Bookmarks.Exists("workingHour"))
                    {
                        object oBookMark = "workingHour";
                        myWordDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = (double.Parse(workingHour.Text) * double.Parse(rateperhour.Text)).ToString() ;
                    }

                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "Word document|*.doc";
                        saveFileDialog1.Title = "Save the Word Document";
                        if (DialogResult.OK == saveFileDialog1.ShowDialog())
                        {
                            string docName = saveFileDialog1.FileName;
                        myWordDoc.SaveAs2(docName);
                        MessageBox.Show("File is Successfully saved");
                        presentDay.Text = "0";
                        workingHour.Text = "0";
                        otHourtxt.Text = "0";
                        grossPay.Text = "0";
                        netPay.Text = "0";
                        latetxt.Text = "0";
                        absentReduction.Text = "0";
                        sss.Text = "0";
                        pagibig.Text = "0";
                        philHealth.Text = "0";
                        taxtxt.Text = "0";
                        cashAdvance.Text = "0";
                        other.Text = "0";
                        calculateBtn.Enabled = false;
                        showRecord.Enabled = false;
                        generatePayroll.Enabled = false;
                        


                    }
                        else
                        {
                        myWordDoc.SaveAs2("C:/Windows/Temp/TestDocumentWith1Paragraph.docx");
                        presentDay.Text = "0";
                        workingHour.Text = "0";
                        otHourtxt.Text = "0";
                        grossPay.Text = "0";
                        netPay.Text = "0";
                        latetxt.Text = "0";
                        absentReduction.Text = "0";
                        sss.Text = "0";
                        pagibig.Text = "0";
                        philHealth.Text = "0";
                        taxtxt.Text = "0";
                        cashAdvance.Text = "0";
                        other.Text = "0";
                        calculateBtn.Enabled = false;
                        showRecord.Enabled = false;
                        generatePayroll.Enabled = false;


                    }
                    myWordDoc.Close();
                        wordApp.Quit();
                        File.Delete(filename);
                }
                conn.Close();
            }

        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            generatePayroll.Enabled = false;
            showRecord.Enabled = false;
            calculateBtn.Enabled = false;
        }

        private void PayrollPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void calculateBtn_EnabledChanged_1(object sender, EventArgs e)
        {

        }

        private void showRecord_Click(object sender, EventArgs e)
        {
            showDtr showingDtr = new showDtr();
            showingDtr.idTouse = idUsed;
            showingDtr.dateFrom = dateFrom.Value.ToString("yyyy-MM-dd");
            showingDtr.dateTo = dateTo.Value.ToString("yyyy-MM-dd");
            showingDtr.ShowDialog();

        }

        //geting number of working days
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

        public void refreshEmployeeTable()
        {
            
            //Show Manage Employee 
            employeeMngtable.Rows.Clear();
            String showemployeequery = "SELECT * from employeeInfo";
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

        //searching parameter
        public void searchEmployeeTable(String tosearch)
        {
            String searchParam = tosearch;
            String searchQuery = "";
            //to know which selected to search
            if (searchBy.SelectedIndex == 0) {  searchQuery = "SELECT * from employeeInfo Where employeeId LIKE '%"+tosearch+"%'"; }
            else if (searchBy.SelectedIndex == 1) {  searchQuery = "SELECT * from employeeInfo Where employeeFirstName LIKE '%"+tosearch+"%'"; }
            else if (searchBy.SelectedIndex == 2) {  searchQuery = "SELECT * from employeeInfo Where employeeLastName LIKE '%"+ tosearch + "%'"; }
            //Show Manage Employee 
            employeeMngtable.Rows.Clear();
            MySqlCommand showemployeecommand = new MySqlCommand(searchQuery, conn);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            PayrollPanel.Width = 0;
            dtrcontainer.Width = 0;
            manageEmployeePanel.Width = 1060;
            deductionPanel.Width = 0;
            payrollSummary.Width = 0;
            cashAdvancePanel.Width = 0;

            refreshEmployeeTable();
            searchBy.SelectedIndex = 0;
        }
    
        private void employeeMngtable_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PayrollPanel.Width = 0;
            dtrcontainer.Width = 0;
            manageEmployeePanel.Width = 0;
            deductionPanel.Width = 0;
            payrollSummary.Width = 0;
            cashAdvancePanel.Width = 1060;

            //show cash advance table
            cashAdvanceTable.Width = 1055;
            requestTable.Width = 0;
            cashAdvanceTable.Rows.Clear();
            String query = "Select cashadvance.id as id, cashadvance.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as employeeName, cashadvance.Debit as debit, cashadvance.Credit as credit, cashadvance.Balance as balance from cashadvance inner join employeeinfo on cashadvance.employeeId = employeeInfo.employeeId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cashAdvanceTable.Rows.Add(new String[] { reader["id"].ToString(), reader["employeeId"].ToString(), reader["employeeName"].ToString(), reader["debit"].ToString(), reader["credit"].ToString(), reader["balance"].ToString() });
                }
            }
            conn.Close();
        }



        private void employeeMngtable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = employeeMngtable.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = employeeMngtable.Rows[selectedrowindex];
            //show button
            if (e.ColumnIndex == 6)
            {
                using(var employeeInfoForm = new employeeInfo())
                {
                    employeeInfoForm.employeeIdtouse = int.Parse(selectedRow.Cells[0].Value.ToString());
                    employeeInfoForm.ShowDialog();
                }
            }

            // update button
            if (e.ColumnIndex == 7)
            {
                using(var editform = new employeeInfoEdit(this))
                {
                    editform.idtouse = int.Parse(selectedRow.Cells[0].Value.ToString());
                    editform.employeeEditMode = true;
                    editform.ShowDialog();
                }
            }

            //delete button
            if(e.ColumnIndex == 8)
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You wanna Delete This Employee?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Insert into delete employee archive
                    String employeeRet = "Insert into delete_employeeinfo select * from employeeinfo where employeeId=@id";
                    MySqlCommand retcommand = new MySqlCommand(employeeRet, conn);
                    conn.Open();
                    retcommand.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    retcommand.ExecuteNonQuery();

                    //deleting employee info 
                    String employeeDelete = "Delete From employeeinfo Where employeeId=@id";
                    MySqlCommand deletecommand = new MySqlCommand(employeeDelete, conn);
                    deletecommand.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int x = deletecommand.ExecuteNonQuery();

                    if (x == 1){MessageBox.Show("Delete Successfully!");}
                    else { MessageBox.Show("Delete Fail!"); }
                    conn.Close();
                    refreshEmployeeTable();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PayrollPanel.Width = 0;
            dtrcontainer.Width = 0;
            manageEmployeePanel.Width = 0;
            deductionPanel.Width = 1055;
            payrollSummary.Width = 0;
            cashAdvancePanel.Width = 0;

            //sss as default
            deductioonTitle.Text = "SSS";
            deductionTable.Rows.Clear();
            String sssquery = "Select payrollsummary.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as fullname, payrollsummary.sss as deduct, payrollsummary.payrollDate as payrolldate from payrollsummary INNER JOIN employeeinfo on payrollsummary.employeeId = employeeinfo.employeeId";
            MySqlCommand sssCommand = new MySqlCommand(sssquery, conn);
            conn.Open();
            MySqlDataReader sssreader = sssCommand.ExecuteReader();
            if (sssreader.HasRows)
            {

                while (sssreader.Read())
                {
                    deductionTable.Rows.Add(new String[] { sssreader["employeeId"].ToString(), sssreader["fullname"].ToString(), sssreader["deduct"].ToString(), sssreader["payrolldate"].ToString() });
                }

            }
            conn.Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            searchEmployeeTable(toSearch.Text);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            using(var addemp = new employeeInfoEdit(this))
            {
                addemp.employeeAddMode = true;
                addemp.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using(var showdelete = new deletedEmployee(this))
            {
                showdelete.ShowDialog();
            }
        }

        private void deductionPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            deductioonTitle.Text = "SSS";
            deductionTable.Rows.Clear();
            String sssquery = "Select payrollsummary.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as fullname, payrollsummary.sss as deduct, payrollsummary.payrollDate as payrolldate from payrollsummary INNER JOIN employeeinfo on payrollsummary.employeeId = employeeinfo.employeeId";
            MySqlCommand sssCommand = new MySqlCommand(sssquery, conn);
            conn.Open();
            MySqlDataReader sssreader = sssCommand.ExecuteReader();
            if (sssreader.HasRows)
            {

                while (sssreader.Read())
                {
                    deductionTable.Rows.Add(new String[] { sssreader["employeeId"].ToString(), sssreader["fullname"].ToString(), sssreader["deduct"].ToString(), sssreader["payrolldate"].ToString() });
                }

            }
            conn.Close();
                
        }

        private void button9_Click(object sender, EventArgs e)
        {
            deductioonTitle.Text = "Pag Ibig";
            deductionTable.Rows.Clear();
            String sssquery = "Select payrollsummary.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as fullname, payrollsummary.pagibig as deduct, payrollsummary.payrollDate as payrolldate from payrollsummary INNER JOIN employeeinfo on payrollsummary.employeeId = employeeinfo.employeeId";
            MySqlCommand sssCommand = new MySqlCommand(sssquery, conn);
            conn.Open();
            MySqlDataReader sssreader = sssCommand.ExecuteReader();
            if (sssreader.HasRows)
            {

                while (sssreader.Read())
                {
                    deductionTable.Rows.Add(new String[] { sssreader["employeeId"].ToString(), sssreader["fullname"].ToString(), sssreader["deduct"].ToString(), sssreader["payrolldate"].ToString() });
                }

            }
            conn.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            deductioonTitle.Text = "Tax";
            deductionTable.Rows.Clear();
            String sssquery = "Select payrollsummary.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as fullname, payrollsummary.tax as deduct, payrollsummary.payrollDate as payrolldate from payrollsummary INNER JOIN employeeinfo on payrollsummary.employeeId = employeeinfo.employeeId";
            MySqlCommand sssCommand = new MySqlCommand(sssquery, conn);
            conn.Open();
            MySqlDataReader sssreader = sssCommand.ExecuteReader();
            if (sssreader.HasRows)
            {

                while (sssreader.Read())
                {
                    deductionTable.Rows.Add(new String[] { sssreader["employeeId"].ToString(), sssreader["fullname"].ToString(), sssreader["deduct"].ToString(), sssreader["payrolldate"].ToString() });
                }

            }
            conn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            deductioonTitle.Text = "Phil. Health";
            deductionTable.Rows.Clear();
            String sssquery = "Select payrollsummary.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as fullname, payrollsummary.philhealth as deduct, payrollsummary.payrollDate as payrolldate from payrollsummary INNER JOIN employeeinfo on payrollsummary.employeeId = employeeinfo.employeeId";
            MySqlCommand sssCommand = new MySqlCommand(sssquery, conn);
            conn.Open();
            MySqlDataReader sssreader = sssCommand.ExecuteReader();
            if (sssreader.HasRows)
            {

                while (sssreader.Read())
                {
                    deductionTable.Rows.Add(new String[] { sssreader["employeeId"].ToString(), sssreader["fullname"].ToString(), sssreader["deduct"].ToString(), sssreader["payrolldate"].ToString() });
                }

            }
            conn.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            /*
            SSS
                Pag Ibig
                    Tax
                    Phil.Health
                    */
            String toFind = "";
            if (deductioonTitle.Text == "SSS") { toFind = ""; }


        }

        private void button15_Click(object sender, EventArgs e)
        {
            using(var usesss = new showDeduct())
            {
                usesss.tableUsed = "sss";
                usesss.ShowDialog();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (var usephilhealth = new showDeduct())
            {
                usephilhealth.tableUsed = "philhealth";
                usephilhealth.ShowDialog();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (var usetax = new showDeduct())
            {
                usetax.tableUsed = "tax";
                usetax.ShowDialog();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dtrTable.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
                "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where employeeinfo.employeeId like '%"+searchDtr.Text+"%' and attendanceDate between @date1 and @date2";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            command2.Parameters.AddWithValue("@date1", dtrFrom.Value);
            command2.Parameters.AddWithValue("@date2", dtrTo.Value);
            conn.Open();
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dtrTable.Rows.Add(new String[] { reader[0].ToString(), reader[2].ToString() + " " + reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            searchDtr.Visible = false;
            absentSearch.Visible = true;
            absentTable.Width = 0;
            dtrTable.Width = 1018;
            dtrTable.Rows.Clear();
            String myquery2 = "SELECT employeeattendance.attendanceId, employeeattendance.employeeId, employeeinfo.employeeFirstName, employeeLastName, " +
                "employeeattendance.employeeIn, employeeattendance.employeeOut, employeeattendance.attendanceDate FROM employeeattendance " +
                "INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId";
            MySqlCommand command2 = new MySqlCommand(myquery2, conn);
            conn.Open();
            MySqlDataReader reader = command2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dtrTable.Rows.Add(new String[] { reader[0].ToString(), reader[2].ToString() + " " + reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() });
                }
            }
            conn.Close();
        }

        private void dtrFrom_ValueChanged(object sender, EventArgs e)
        {
            dtrTo.MinDate = dtrFrom.Value.AddDays(1);

        }

        private void dtrTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showAbsent_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            searchDtr.Visible = false;
            absentSearch.Visible = true;
            absentTable.Width = 1018;
            dtrTable.Width = 0;
            absentTable.Rows.Clear();
            //String myquery2 = "SELECT concat(employeeinfo.employeeFirstName, ' ', employeeInfo.employeeLastName) as employeeName,employeeinfo.dateCreated as createTo, COUNT(employeeattendance.attendanceDate) as presentDay FROM employeeattendance INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate between @date1 and @date2 group by employeeName order by presentDay";
            String myquery2 = "SELECT concat(employeeinfo.employeeFirstName, ' ', employeeinfo.employeeLastName) as employeeName, employeeattendance.employeeId as id,employeeinfo.dateCreated as tocreate, COUNT(employeeattendance.attendanceDate) as presentDay FROM employeeattendance INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate between @date1 and @date2 group by employeeattendance.employeeId";
            MySqlCommand command = new MySqlCommand(myquery2, conn);
            conn.Open();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime firstDay = new DateTime(year, month, 1);
            command.Parameters.AddWithValue("@date1", firstDay);
            command.Parameters.AddWithValue("@date2", DateTime.Now);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int numWorkingdays = GetNumberOfWorkingDays(firstDay, DateTime.Now);
                    DateTime createDate = DateTime.Parse(reader["tocreate"].ToString());
                    if (firstDay <= createDate)
                    {
                        numWorkingdays = GetNumberOfWorkingDays(createDate, DateTime.Now);
                    }
                    int noabsent = 0;
                    noabsent = numWorkingdays - int.Parse(reader["presentDay"].ToString());
                    if (noabsent < 0) { noabsent = 0; }

                    absentTable.Rows.Add(new String[] { reader["employeeName"].ToString(), noabsent.ToString()});
                }

            }
            conn.Close();

        }

            private void button16_Click_1(object sender, EventArgs e)
        {
            absentTable.Rows.Clear();
            String myquery2 = "SELECT concat(employeeinfo.employeeFirstName, ' ', employeeinfo.employeeLastName) as employeeName, employeeattendance.employeeId as id,employeeinfo.dateCreated as tocreate, COUNT(employeeattendance.attendanceDate) as presentDay FROM employeeattendance INNER JOIN employeeinfo ON employeeattendance.employeeId = employeeinfo.employeeId where attendanceDate between @date1 and @date2 group by employeeattendance.employeeId";
            MySqlCommand command = new MySqlCommand(myquery2, conn);
            conn.Open();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime firstDay = new DateTime(year, month, 1);
            command.Parameters.AddWithValue("@date1", dtrFrom.Value);
            command.Parameters.AddWithValue("@date2", dtrTo.Value);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int numWorkingdays = GetNumberOfWorkingDays(dtrFrom.Value, dtrTo.Value);
                    DateTime createDate = DateTime.Parse(reader["tocreate"].ToString());
                    if (firstDay <= createDate)
                    {
                        numWorkingdays = GetNumberOfWorkingDays(createDate, dtrTo.Value);
                    }
                    int noabsent = 0;
                    noabsent = numWorkingdays - int.Parse(reader["presentDay"].ToString());
                    if (noabsent < 0) { noabsent = 0; }

                    absentTable.Rows.Add(new String[] { reader["employeeName"].ToString(), noabsent.ToString() });
                }

            }
            conn.Close();

        }

        private void button20_Click(object sender, EventArgs e)
        {
            payrollTable.Rows.Clear();
            String query = "Select payrollsummary.id as Id, payrollsummary.payrollDate as payrollDate,employeeinfo.employeeId as employeeId ,concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as employeeName, payrollsummary.grossPay as gp, payrollsummary.netPay as np from payrollsummary INNER JOIN employeeinfo on employeeinfo.employeeId = payrollsummary.employeeId where payrollsummary.employeeId like '%"+payrollSearch.Text+"%'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    payrollTable.Rows.Add(new String[] { reader["Id"].ToString(), reader["payrollDate"].ToString(), reader["employeeId"].ToString(), reader["employeeName"].ToString(), reader["gp"].ToString(), reader["np"].ToString() });
                }
            }
            conn.Close();
        }

        private void button16_Click_2(object sender, EventArgs e)
        {
            payrollTable.Rows.Clear();
            String query = "Select payrollsummary.id as Id, payrollsummary.payrollDate as payrollDate,employeeinfo.employeeId as employeeId ,concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as employeeName, payrollsummary.grossPay as gp, payrollsummary.netPay as np from payrollsummary INNER JOIN employeeinfo on employeeinfo.employeeId = payrollsummary.employeeId where payrollsummary.employeeId like '%" + payrollSearch.Text + "%' and payrollsummary.payrollDate between @date1 and @date2";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@date1", payrollFrom.Value);
            cmd.Parameters.AddWithValue("@date2", payrollTo.Value);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    payrollTable.Rows.Add(new String[] { reader["Id"].ToString(), reader["payrollDate"].ToString(), reader["employeeId"].ToString(), reader["employeeName"].ToString(), reader["gp"].ToString(), reader["np"].ToString() });
                }
            }
            conn.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            PayrollPanel.Width = 0;
            dtrcontainer.Width = 0;
            manageEmployeePanel.Width = 0;
            deductionPanel.Width = 0;
            payrollSummary.Width = 1060;
            cashAdvancePanel.Width = 0;

            //show payroll tab
            String query = "Select payrollsummary.id as Id, payrollsummary.payrollDate as payrollDate,employeeinfo.employeeId as employeeId ,concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as employeeName, payrollsummary.grossPay as gp, payrollsummary.netPay as np from payrollsummary INNER JOIN employeeinfo on employeeinfo.employeeId = payrollsummary.employeeId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    payrollTable.Rows.Add(new String[] { reader["Id"].ToString(), reader["payrollDate"].ToString(),reader["employeeId"].ToString(), reader["employeeName"].ToString(), reader["gp"].ToString(), reader["np"].ToString() });
                }
            }
            conn.Close();


        }

        private void payrollTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = payrollTable.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = payrollTable.Rows[selectedrowindex];
            //show button
            if (e.ColumnIndex == 6)
            {
                using (var payrollSum = new payrollSummary())
                {
                    payrollSum.payrollid = int.Parse(selectedRow.Cells[0].Value.ToString());
                    payrollSum.ShowDialog();
                }
            }
        }

        private void requestTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = requestTable.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = requestTable.Rows[selectedrowindex];
            //accept button
            if (e.ColumnIndex == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure?", "Accept", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                { 
                    //accept
                    String acc = "INSERT INTO cashadvance(employeeId, Debit,Balance) select employeeId, amount, amount from cashadvancerequest where id=@id";
                    MySqlCommand cmd = new MySqlCommand(acc, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int value1 = cmd.ExecuteNonQuery();
                    if (value1 == 1) { MessageBox.Show("Added Succesfully!"); }
                    else { MessageBox.Show("Failed!"); }
                    conn.Close();

                    String toDel = "delete from cashadvancerequest where id=@id";
                    MySqlCommand cmd1 = new MySqlCommand(toDel, conn);
                    conn.Open();
                    cmd1.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int value2 = cmd1.ExecuteNonQuery();
                    conn.Close();


                    requestTable.Rows.Clear();
                    String rqs = "select cashadvancerequest.id as id, cashadvancerequest.employeeId as employeeId, concat(employeeInfo.employeeFirstname,' ',employeeInfo.employeeLastName) as employeeName, cashadvancerequest.reason as reason, cashadvancerequest.amount as amount from cashadvancerequest inner join employeeInfo on cashadvancerequest.employeeId=employeeInfo.employeeId";
                    MySqlCommand cmd2 = new MySqlCommand(rqs, conn);
                    conn.Open();
                    MySqlDataReader rd = cmd2.ExecuteReader();
                    while (rd.Read())
                    {
                        requestTable.Rows.Add(new String[] { rd["id"].ToString(), rd["employeeId"].ToString(), rd["employeeName"].ToString(), rd["reason"].ToString(), rd["amount"].ToString() });
                    }
                    conn.Close();
                }
            }

            //decline btn
            if (e.ColumnIndex == 6)
            {
                DialogResult dialogResult1 = MessageBox.Show("Are you Sure?", "Decline", MessageBoxButtons.YesNo);
                if(dialogResult1 == DialogResult.Yes)
                {
                    //decline
                    String toDel = "delete from cashadvancerequest where id=@id";
                    MySqlCommand cmd1 = new MySqlCommand(toDel, conn);
                    conn.Open();
                    cmd1.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                    int value2 = cmd1.ExecuteNonQuery();
                    if (value2 == 1) { MessageBox.Show("Deleted Succesfully!"); }
                    else { MessageBox.Show("Failed!"); }
                    conn.Close();

                    requestTable.Rows.Clear();
                    String rqs = "select cashadvancerequest.id as id, cashadvancerequest.employeeId as employeeId, concat(employeeInfo.employeeFirstname,' ',employeeInfo.employeeLastName) as employeeName, cashadvancerequest.reason as reason, cashadvancerequest.amount as amount from cashadvancerequest inner join employeeInfo on cashadvancerequest.employeeId=employeeInfo.employeeId";
                    MySqlCommand cmd = new MySqlCommand(rqs, conn);
                    conn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        requestTable.Rows.Add(new String[] { rd["id"].ToString(), rd["employeeId"].ToString(), rd["employeeName"].ToString(), rd["reason"].ToString(), rd["amount"].ToString() });
                    }
                    conn.Close();
                }
            }


        }

        private void button18_Click(object sender, EventArgs e)
        {
        }

        private void cashadvnrqstBtn_Click(object sender, EventArgs e)
        {
            cashAdvanceTable.Width = 0;
            requestTable.Width = 1052;

            requestTable.Rows.Clear();
            String rqs = "select cashadvancerequest.id as id, cashadvancerequest.employeeId as employeeId, concat(employeeInfo.employeeFirstname,' ',employeeInfo.employeeLastName) as employeeName, cashadvancerequest.reason as reason, cashadvancerequest.amount as amount from cashadvancerequest inner join employeeInfo on cashadvancerequest.employeeId=employeeInfo.employeeId";
            MySqlCommand cmd = new MySqlCommand(rqs,conn);
            conn.Open();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                requestTable.Rows.Add(new String[] { rd["id"].ToString(), rd["employeeId"].ToString(), rd["employeeName"].ToString(), rd["reason"].ToString(), rd["amount"].ToString() });
            }
            conn.Close();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //show cash advance table
            cashAdvanceTable.Width = 1055;
            requestTable.Width = 0;
            cashAdvanceTable.Rows.Clear();
            String query = "Select cashadvance.id as id, cashadvance.employeeId as employeeId, concat(employeeinfo.employeeFirstName,' ', employeeinfo.employeeLastName) as employeeName, cashadvance.Debit as debit, cashadvance.Credit as credit, cashadvance.Balance as balance from cashadvance inner join employeeinfo on cashadvance.employeeId = employeeInfo.employeeId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cashAdvanceTable.Rows.Add(new String[] { reader["id"].ToString(), reader["employeeId"].ToString(), reader["employeeName"].ToString(), reader["debit"].ToString(), reader["credit"].ToString(), reader["balance"].ToString() });
                }
            }
            conn.Close();
        }

        private void cashAdvanceTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void employeeId_TextChanged(object sender, EventArgs e)
        {
            calculateBtn.Enabled = false;
            showRecord.Enabled = false;
            calculateBtn.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminLogin1 adminlog = new adminLogin1();
            adminlog.ShowDialog();
            this.Close();
            this.Dispose();
        }

        private void payrollSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
