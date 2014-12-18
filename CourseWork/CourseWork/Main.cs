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
        public int UserType;
        public UserRights UR = new UserRights();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var login = new LoginForm();
            login.Closing += _loginFormClosing;
            login.FormClosing += _loginFormClosing;
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

            UserId = login.UserId;
            UserType = login.UserType;
            UR = login.UR;

            label1.Text = login.UserId;
            label2.Text = "Добро пожаловать в Курсовая_Работа_Тест.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UR.Empl)
            {
                var employee = new Empl(UserId);
                employee.ShowDialog();
            }
            else
            {
                const string message = "У вас нет прав.";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (UR.Needs)
            {
                var need = new Needs(UserId);
                need.ShowDialog();
            }
            else
            {
                const string message = "У вас нет прав.";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UR.WorkPlace)
            {
                var wplace = new Workplace(UserId);
                wplace.ShowDialog();
            }
            else
            {
                const string message = "У вас нет прав.";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (UR.TechObj)
            {
                var tobject = new TechObj(UserId);
                tobject.ShowDialog();
            }
            else
            {
                const string message = "У вас нет прав.";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserType == 2)
            {
                const string message = "Данная опция не доступна при тестовом аккаунте .";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            else
            {
                var calculation = new Calc(UserId);
                calculation.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (UserType == 0)
            {
                var registr = new Registr();
                registr.ShowDialog();
            }
            else
            {
                const string message = "Данная опция доступна только пользователям с правами админа.";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (UserType == 2)
            {
                const string message = "Данная опция не доступна при тестовом аккаунте .";
                const string caption = "Ограничения.";
                const MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            else
            {
                var miniU = new MiniUser(UserId);
                miniU.ShowDialog();
            }
        }

    }
}
