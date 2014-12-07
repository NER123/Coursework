using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kurs_Test;

namespace CourseWork
{
    public partial class Main : Form
    {
        public string UserId;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var login = new LoginForm();
            login.Closing += _loginFormClosing;
            login.ShowDialog();
        }


        private void _loginFormClosing(object sender, EventArgs e)
        {
            LoginForm login = sender as LoginForm;

            if (!login.enter)
            {
                this.Close();
            }
            if (!login.correct)
            {
                this.Close();
            }
            UserId = login.UserID;
            label1.Text = login.UserID;
            label2.Text = "Добро пожаловать в Курсовая_Работа_Тест.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var employee = new Empl(UserId);
            employee.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var need = new Needs(UserId);
            need.ShowDialog();
        }

    }
}
