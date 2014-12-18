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

namespace CourseWork
{
    public partial class Registr : Form
    {
        private SqlConnection _conn;
        private int _type;
        
        public Registr()
        {
            InitializeComponent();
            this.Closing += FormClos;
            Registr_Load();
        }

        private void FormClos(object sender, CancelEventArgs e)
        {
            _conn.Close();
        }

        private void Registr_Load()
        {
            radioButton3.Checked = true;
            _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
            _conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var id = Guid.NewGuid();
            string idString = id.ToString();
            bool ok = false;

            var cmds = new SqlCommand("SELECT * FROM [User] WHERE Login ='" + textBox1.Text + "'", _conn);
            var reader = cmds.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
            {
                const string message = "Такой ник уже существует.";
                const string caption = "Ошибка.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            else
                ok = true;

            reader.Close();

            if (ok)
            {
                var cmd =
                    new SqlCommand(
                        "INSERT INTO [User] (ID, Login, Password, UserType, ProjectID, EmplRight, WorkPlaceRight, TechObjRight, NeedsRight) VALUES ('" + idString + "','" +
                        textBox1.Text + "','" + textBox2.Text + "'," + _type + ",'" + idString + "'," + 1 + "," + 1 + "," + 1 + "," + 1 + ")", _conn);
                cmd.ExecuteNonQuery();
                string message = "Пользователь " + textBox1.Text + " зарегистрирован.";
                const string caption = "Регистрация прошла успешно.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _type = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _type = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _type = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _type = 3;
        }
    }
}
