using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs_Test
{
    public partial class LoginForm : Form
    {
        public bool enter = false;
        public bool correct = false;
        public string UserId;
        public int UserType;

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
            SqlCommand cmd = new SqlCommand("SELECT Login, Password, UserType, ID FROM [User] WHERE Login='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            try
            {
                if (!reader.HasRows)
                    MessageBox.Show("Нет такого логина или пароля.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    UserId = reader.GetString(3);
                    UserType = reader.GetInt32(2);
                    reader.Close();
                    conn.Close();
                    enter = true;
                    correct = true;
                    this.Close();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
