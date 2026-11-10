using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CafeStyle.StyleButton(button2);
            CafeStyle.StyleButton(button3);
            CafeStyle.StyleButton(button4);
            CafeStyle.StyleButton(button5);
            button4.Click += button4_Click;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            CafeData.ShowPage(this, new menu());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CafeData.ShowPage(this, new CafePageForm("Мои заказы", "История ваших заказов"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из BrewFlow?", "Выход",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CafeData.ShowPage(this, new CafePageForm("Поддержка",
                "Телефон: +7 (900) 000-00-00\r\nПочта: support@brewflow.ru"));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
