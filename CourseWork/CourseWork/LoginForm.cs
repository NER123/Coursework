using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs_Test
{
    public partial class LoginForm : Form
    {
        public bool enter = false;
        public bool correct = false;
        public string UserID;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Login, Password FROM [User] WHERE Login='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                MessageBox.Show("Нет такого логина или пароля.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                enter = true;
                correct = true;
                UserID = reader.GetString(0);
                this.Close();
            }
            reader.Close();
            conn.Close();
        }
    }
}
