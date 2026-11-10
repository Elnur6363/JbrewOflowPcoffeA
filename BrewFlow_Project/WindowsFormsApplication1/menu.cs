using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class menu : Form
    {
        public menu() { InitializeComponent(); this.Shown += menu_Shown; }
        private void menu_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { Close(); }
        private void button7_Click(object sender, EventArgs e) { CafeData.ShowPage(this,new shopping_cart()); }
        private void menu_Shown(object sender, EventArgs e)
        {
            Button[] buttons={button1,button2,button3,button4,button5,button6,button7};
            foreach(Button b in buttons) CafeStyle.StyleButton(b);
            button1.Click += delegate { AddItem("Американо",140m); };
            button2.Click += delegate { AddItem("Латте",220m); };
            button3.Click += delegate { AddItem("Капучино",200m); };
            button4.Click += delegate { AddItem("Чизкейк",250m); };
            button5.Click += delegate { AddItem("Круасан",180m); };
        }
        private void AddItem(string name,decimal price) { CafeData.Add(name,price); MessageBox.Show(name+" добавлен в корзину.","BrewFlow",MessageBoxButtons.OK,MessageBoxIcon.Information); }
    }
}
