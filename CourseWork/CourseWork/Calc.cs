using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs_Test
{
    public partial class Calc : Form
    {

        private SqlConnection _conn;
        private SqlDataAdapter _adapt;
        private DataSet _ds;
        private readonly String _userId;
        private SqlCommandBuilder _scbuild;

        public Calc()
        {
        }

        public Calc(string id)
        {
            InitializeComponent();
            _userId = id;
            CalcLoad();
            textBox2.ReadOnly = true;
            textBox3.Font = new Font(textBox3.Font.FontFamily, 16);            
        }

        private void CalcLoad()
        {
            try
            {
                this.Closing += FormClos;
                int summ = 0;
                int temporate = 0;

                _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
                _conn.Open();

                var cmds = new SqlCommand("SELECT SUM(Pay) AS Total FROM [Empl] WHERE UserID ='" + _userId + "'", _conn);
                var reader = cmds.ExecuteReader();

                reader.Read();

                if (reader.HasRows)
                    temporate = reader.GetInt32(0)*12;
        
                reader.Close();

                summ += temporate;
                chart1.Series["Costs"].Points.AddXY("Зарплата",temporate);  

                cmds = new SqlCommand("SELECT Rent, Repair FROM [WorkPlacePay] WHERE UserID ='" + _userId + "'", _conn);
                reader = cmds.ExecuteReader();

                reader.Read();

                if (reader.HasRows)
                    temporate = reader.GetInt32(0) * 12 + reader.GetInt32(1); 

                reader.Close();

                summ += temporate;
                chart1.Series["Costs"].Points.AddXY("Рабочее место", temporate);  

                cmds = new SqlCommand("SELECT Cost, PerYear FROM [PlanRep] WHERE UserID ='" + _userId + "'", _conn);
                reader = cmds.ExecuteReader();
                temporate = 0;

                if (reader.HasRows)
                    while (reader.Read())
                        temporate += reader.GetInt32(0) * reader.GetInt32(1)/12 ;

                reader.Close();

                summ += temporate;
                chart1.Series["Costs"].Points.AddXY("Плановый почин", temporate);  

                textBox2.Text = Convert.ToString(summ);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private void FormClos(object sender, CancelEventArgs e)
        {
            _conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int calculation = Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text);

            if (calculation < 0)
                textBox3.BackColor = Color.DarkRed;
            else
            {
                textBox3.BackColor = Color.Green;
            }

            textBox3.Text = Convert.ToString(calculation);
            
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

    }
}
