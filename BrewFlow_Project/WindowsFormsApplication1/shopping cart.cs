using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class shopping_cart : Form
    {
        private FlowLayoutPanel items; private Label totalLabel; private TextBox promo;
        public shopping_cart() { InitializeComponent(); }
        private void shopping_cart_Load(object sender, EventArgs e)
        {
            Text="Корзина — BrewFlow"; BackColor=CafeStyle.Background; CafeStyle.StyleButton(button1); CafeStyle.StyleButton(button2); CafeStyle.StyleButton(button3);
            button2.Text="Назад"; button2.Click += delegate { Close(); }; button3.Click += ApplyPromo;
            textBox1.Visible=false;
            items=new FlowLayoutPanel { Location=new Point(25,70), Size=new Size(960,225), AutoScroll=true, FlowDirection=FlowDirection.TopDown, WrapContents=false, BackColor=Color.Bisque };
            Controls.Add(items);
            promo=new TextBox { Location=new Point(30,360), Size=new Size(145,25) }; Controls.Add(promo);
            Label hint=new Label { Text="Промокод",Location=new Point(30,325),AutoSize=true,Font=new Font("Segoe Print",14),ForeColor=CafeStyle.Brown }; Controls.Add(hint);
            totalLabel=new Label { Location=new Point(650,360),Size=new Size(320,35),Font=new Font("Segoe Print",15,FontStyle.Bold),ForeColor=CafeStyle.Brown }; Controls.Add(totalLabel);
            RefreshCart();
        }
        private void RefreshCart()
        {
            items.Controls.Clear();
            if(CafeData.Cart.Count==0) items.Controls.Add(new Label { Text="Корзина пока пуста. Добавьте что-нибудь из меню!",AutoSize=true,Font=new Font("Segoe Print",12),ForeColor=CafeStyle.Brown,Margin=new Padding(15) });
            foreach(CoffeeProduct p in CafeData.Cart.ToList())
            {
                Panel row=new Panel { Width=910,Height=42,BackColor=Color.Bisque,Margin=new Padding(4) };
                Label name=new Label { Text=p.Name,Location=new Point(8,8),Size=new Size(280,25),Font=new Font("Segoe Print",11),ForeColor=CafeStyle.Brown };
                Label price=new Label { Text=(p.Price*p.Quantity).ToString("0.00")+" руб.",Location=new Point(450,8),Size=new Size(130,25),Font=new Font("Segoe Print",10),ForeColor=CafeStyle.Brown };
                Button minus=new Button { Text="−",Location=new Point(650,5),Size=new Size(35,28) }; CafeStyle.StyleButton(minus);
                Label qty=new Label { Text=p.Quantity.ToString(),Location=new Point(692,9),Size=new Size(30,22),TextAlign=ContentAlignment.MiddleCenter };
                Button plus=new Button { Text="+",Location=new Point(727,5),Size=new Size(35,28) }; CafeStyle.StyleButton(plus);
                Button remove=new Button { Text="Удалить",Location=new Point(790,5),Size=new Size(85,28) }; CafeStyle.StyleButton(remove);
                minus.Click+=delegate { CafeData.Change(p.Name,p.Quantity-1); RefreshCart(); };
                plus.Click+=delegate { CafeData.Change(p.Name,p.Quantity+1); RefreshCart(); };
                remove.Click+=delegate { CafeData.Remove(p.Name); RefreshCart(); };
                row.Controls.Add(name); row.Controls.Add(price); row.Controls.Add(minus); row.Controls.Add(qty); row.Controls.Add(plus); row.Controls.Add(remove); items.Controls.Add(row);
            }
            totalLabel.Text="Итого: "+(CafeData.CartTotal*(1m-CafeData.Discount)).ToString("0.00")+" руб.";
            button1.Enabled=CafeData.Cart.Count>0;
        }
        private void ApplyPromo(object sender,EventArgs e)
        {
            string code=promo.Text.Trim().ToUpperInvariant();
            if(code=="BREW10") { CafeData.Discount=0.10m; MessageBox.Show("Скидка 10% применена!","BrewFlow"); }
            else if(code=="COFFEE15") { CafeData.Discount=0.15m; MessageBox.Show("Скидка 15% применена!","BrewFlow"); }
            else { CafeData.Discount=0m; MessageBox.Show("Промокод не найден. Попробуйте BREW10 или COFFEE15.","BrewFlow"); }
            RefreshCart();
        }
        private void button1_Click(object sender,EventArgs e)
        {
            if(CafeData.Cart.Count==0) { MessageBox.Show("Корзина пуста."); return; }
            using(PaymentForm payment=new PaymentForm(CafeData.CartTotal*(1m-CafeData.Discount)))
            {
                if(payment.ShowDialog(this)==DialogResult.OK)
                {
                    CafeData.PlaceOrder(payment.PaymentMethod,payment.DeliveryType,payment.Address);
                    MessageBox.Show("Заказ успешно оформлен!\r\nСпособ оплаты: "+payment.PaymentMethod+"\r\nПолучение: "+payment.DeliveryType,"BrewFlow — заказ принят",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Close();
                }
            }
        }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}
