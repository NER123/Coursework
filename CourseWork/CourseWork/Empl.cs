using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace kurs_Test
{
    public partial class Empl : Form
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adapt;
        private DataSet _ds;
        private readonly String _userId;
        private SqlCommandBuilder _scbuild;

        public Empl()
        {

        }

        public Empl(string Userid)
        {
            InitializeComponent();
            _userId = Userid;
            Empl_Load();
        }

        private void Empl_Load()
        {
            try
            {
                _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
                _conn.Open();
                _adapt = new SqlDataAdapter("SELECT Name, Fam, Otc, Pay, ID FROM [Empl] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");
                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.UserDeletedRow += DeletingRow;
                dataGridView1.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void DeletingRow(object sender, DataGridViewRowEventArgs e)
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
            _conn.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Guid ID = Guid.NewGuid();
                string ID_string = ID.ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [Empl] ([UserID],[Name],[Fam],[Otc],[Pay],[ID]) VALUES ('" + _userId + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'," + Convert.ToInt16(textBox4.Text) + ",'" + ID_string + "')", _conn);
                cmd.ExecuteNonQuery();
                _adapt = new SqlDataAdapter("SELECT Name, Fam, Otc, Pay, ID FROM [Empl] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[4].Visible = false;
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
                object test = _ds.Tables[0].Rows[index][4, DataRowVersion.Original];
                string test2 = Convert.ToString(test);
                test2 = test2.Replace(" ", string.Empty);
                SqlCommand cmd = new SqlCommand("DELETE FROM [Empl] WHERE ID='" + test2 + "'", _conn);
                cmd.ExecuteNonQuery();
                dataGridView1.Rows.RemoveAt(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
