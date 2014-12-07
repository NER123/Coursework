
using System.Windows.Forms;

namespace kurs_Test
{
    public partial class TechObj : Form
    {
        public string UserId;

        public TechObj()
        {
        }

        public TechObj(string userID)
        {
            UserId = userID;
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var planrep = new PlanRep(UserId);
            planrep.ShowDialog();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var emerep = new EmeRep(UserId);
            emerep.ShowDialog();
        }

        private void button3_Click_1(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
