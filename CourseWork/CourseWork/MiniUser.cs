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
    public partial class MiniUser : Form
    {
        private SqlConnection _conn;
        private int _type;
        public string UserId;

        public MiniUser()
        {
        }

        
        public MiniUser(string id)
        {
            InitializeComponent();
            UserId = id;
            this.Closing += FormClos;
            MiniUser_Load();
        }

        private void FormClos(object sender, CancelEventArgs e)
        {
            _conn.Close();
        }

        private void MiniUser_Load()
        {
            _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
            _conn.Open();
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
                        textBox1.Text + "','" + textBox2.Text + "'," + 3 + ",'" + UserId + "'," + Convert.ToInt16(checkBox1.Checked) + "," + Convert.ToInt16(checkBox2.Checked) + "," +
                        Convert.ToInt16(checkBox3.Checked) + "," + Convert.ToInt16(checkBox4.Checked) + ")", _conn);
                cmd.ExecuteNonQuery();

                string message = "Пользователь " + textBox1.Text + " зарегистрирован.";
                const string caption = "Регистрация прошла успешно.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }  
    }
}
