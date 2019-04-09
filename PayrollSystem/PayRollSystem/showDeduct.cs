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
    public partial class showDeduct : Form
    {
        public String tableUsed = "";
        public MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=employee;SslMode=none; Allow Zero Datetime=true;");
        public showDeduct()
        {
            InitializeComponent();
        }

        private void showDeduct_Load(object sender, EventArgs e)
        {
            taxTable.Width = 0;
            deductionTable.Width = 0;
            String querytouse = "";
            if (tableUsed == "sss") { querytouse = "SELECT * FROM `sss_table` ORDER BY minRange"; deductionTable.Width += 854; }
            if (tableUsed == "philhealth") { querytouse = "Select * from philhealth_table"; deductionTable.Width += 854; }
            if (tableUsed == "tax") { querytouse = "Select * from bir_table";
                taxTable.Width += 854;
                querytouse = "SELECT * from bir_table order by payPeriod, baseCompensation";
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand(querytouse, conn);
                MySqlDataReader read1 = cmd1.ExecuteReader();
                if (read1.HasRows)
                {
                    while (read1.Read())
                    {
                        String toabove = "";
                        if (double.Parse(read1[3].ToString()) >= 1000000000) { toabove = "And Above"; }
                        else { toabove = read1[3].ToString(); }
                        taxTable.Rows.Add(new String[] { read1[1].ToString(), read1[2].ToString(), toabove, (double.Parse(read1[4].ToString())*100)+"%".ToString(), read1[5].ToString()});
                    }
                }
                conn.Close();
            }
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(querytouse, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    String toabove = "";
                    if(double.Parse(read[2].ToString())>= 1000000000) { toabove = "And Above"; }
                    else { toabove = read[2].ToString(); }
                    deductionTable.Rows.Add(new String[] {read[1].ToString(), toabove, read[3].ToString() });
                }
            }
            conn.Close();
            
        }
    }
}
