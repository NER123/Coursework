using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using CourseWork;

namespace kurs_Test
{

    public partial class LoginForm : Form
    {
        public bool enter = false;
        public bool correct = false;
        public string UserId;
        public int UserType;
        public UserRights UR = new UserRights();

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
            SqlCommand cmd = new SqlCommand("SELECT Login, Password, UserType, ProjectID, EmplRight, WorkPlaceRight ,TechObjRight, NeedsRight FROM [User] WHERE Login='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", conn);
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

                    UR.Empl = reader.GetBoolean(4);
                    UR.WorkPlace = reader.GetBoolean(5);
                    UR.TechObj = reader.GetBoolean(6);
                    UR.Needs = reader.GetBoolean(7);

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
