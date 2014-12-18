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
    public partial class Workplace : Form
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adapt;
        private DataSet _ds;
        private readonly String _userId;
        private SqlCommandBuilder _scbuild;

        public Workplace()
        {
        }

        public Workplace(string Userid)
        {
            InitializeComponent();
            _userId = Userid;
            Workplace_Load();
        }

        private void Workplace_Load()
        {
            try
            {
                this.Closing += formClos;
                _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
                _conn.Open();

                var cmd = new SqlCommand("SELECT Rent, Repair FROM [WorkPlacePay] WHERE UserID ='" + _userId + "'", _conn);
                var reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    RentBox.Text = "Введите данные.";
                    RepairBox.Text = "Введите данные.";
                }
                else
                {
                    RentBox.Text = Convert.ToString(reader.GetInt32(0));
                    RepairBox.Text = Convert.ToString(reader.GetInt32(1));
                }
                reader.Close();

                _adapt = new SqlDataAdapter("SELECT Pay, Comment, ID FROM [WorkPlacePayPlus] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");

                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.UserDeletedRow += DeletingRow;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formClos(object sender, CancelEventArgs e)
        {
            _conn.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Guid ID = Guid.NewGuid();
                string ID_string = ID.ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [WorkPlacePayPlus] ([UserID], Pay, Comment, [ID]) VALUES ('" + _userId + "','" + Convert.ToInt32(textBox3.Text) + "','" + textBox4.Text + "','" + ID_string + "')", _conn);
                cmd.ExecuteNonQuery();
                _adapt = new SqlDataAdapter("SELECT Pay, Comment, ID FROM [WorkPlacePayPlus] WHERE UserID ='" + _userId + "'", _conn);
                _ds = new DataSet();
                _adapt.Fill(_ds, "Person Details");

                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.UserDeletedRow += DeletingRow;
                dataGridView1.AllowUserToAddRows = false;
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string delete = "";
                var cmds = new SqlCommand("SELECT Rent, Repair, ID FROM [WorkPlacePay] WHERE UserID ='" + _userId + "'", _conn);
                var reader = cmds.ExecuteReader();
                reader.Read();

                bool has = reader.HasRows;

                if (has)
                    delete = reader.GetString(2);

                reader.Close();

                if (has)
                {
                    SqlCommand cmdz =
                        new SqlCommand("DELETE FROM [WorkPlacePayPlus] WHERE ID='" + delete + "'", _conn);
                    cmdz.ExecuteNonQuery();
                }

                Guid ID = Guid.NewGuid();
                string ID_string = ID.ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [WorkPlacePay] ([UserID], Rent, Repair, ID) VALUES ('" + _userId + "'," + Convert.ToInt32(RentBox.Text) + "," + Convert.ToInt32(RepairBox.Text) + ",'" + ID_string + "')", _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
