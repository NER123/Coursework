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
    public partial class EmeRep : Form
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adapt;
        private DataSet _ds;
        private readonly String _userId;
        private SqlCommandBuilder _scbuild;

        public EmeRep()
        {
        }

        public EmeRep(string id)
        {

            _userId = id;
            InitializeComponent();
            EmeRepLoad();
        }

        private void EmeRepLoad()
        {
            try
            {
                this.Closing += FormClos;

                _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
                _conn.Open();

                _adapt = new SqlDataAdapter("SELECT Cost, StartDate, EndDate, Comm, ID  FROM [EmeRep] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");

                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.UserDeletedRow += DeletingRow;
                dataGridView1.AllowUserToAddRows = false;
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

        private void DeletingRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                _scbuild = new SqlCommandBuilder(_adapt);
                _adapt.Update(_ds, "Person Details");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Guid ID = Guid.NewGuid();
                string ID_string = ID.ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [EmeRep] (UserID, Cost, StartDate, EndDate, Comm, ID) VALUES ('" + _userId + "'," + Convert.ToInt32(textBox3.Text) + ",'" + dateTimePicker1.Value.Date + "','" + dateTimePicker2.Value.Date + "','" + textBox4.Text + "','" + ID_string + "')", _conn);
                cmd.ExecuteNonQuery();
                _adapt = new SqlDataAdapter("SELECT Cost, StartDate, EndDate, Comm, ID  FROM [EmeRep] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");

                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].Visible = false;
                //  dataGridView1.UserDeletedRow += DeletingRow;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _scbuild = new SqlCommandBuilder(_adapt);
                _adapt.Update(_ds, "Person Details");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(index);

                _scbuild = new SqlCommandBuilder(_adapt);
                _adapt.Update(_ds, "Person Details");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
