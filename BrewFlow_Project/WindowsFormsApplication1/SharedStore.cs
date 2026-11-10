using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class CoffeeProduct
    {
        public string Name; public decimal Price; public int Quantity;
        public CoffeeProduct(string name, decimal price, int quantity) { Name=name; Price=price; Quantity=quantity; }
    }
    public class CoffeeOrder
    {
        public string Number; public DateTime Date; public decimal Total; public string Status; public string PaymentMethod; public string DeliveryType; public string Address;
        public CoffeeOrder(string number, decimal total, string paymentMethod, string deliveryType, string address) { Number=number; Total=total; Date=DateTime.Now; Status="Принят"; PaymentMethod=paymentMethod; DeliveryType=deliveryType; Address=address; }
    }
    public static class CafeData
    {
        public static readonly List<CoffeeProduct> Cart = new List<CoffeeProduct>();
        public static readonly List<CoffeeOrder> Orders = new List<CoffeeOrder>();
        public static decimal Discount = 0m;
        public static decimal CartTotal { get { return Cart.Sum(x => x.Price * x.Quantity); } }
        public static void Add(string name, decimal price)
        {
            CoffeeProduct p=Cart.FirstOrDefault(x=>x.Name==name);
            if(p==null) Cart.Add(new CoffeeProduct(name,price,1)); else p.Quantity++;
        }
        public static void Remove(string name) { Cart.RemoveAll(x=>x.Name==name); }
        public static void Change(string name,int quantity) { CoffeeProduct p=Cart.FirstOrDefault(x=>x.Name==name); if(p!=null) { if(quantity<=0) Remove(name); else p.Quantity=quantity; } }
        public static void PlaceOrder(string paymentMethod, string deliveryType, string address)
        {
            if(Cart.Count==0) return;
            decimal total=CartTotal*(1m-Discount);
            Orders.Insert(0,new CoffeeOrder("BF-"+DateTime.Now.ToString("yyyyMMdd-HHmmss"),total,paymentMethod,deliveryType,address));
            Cart.Clear(); Discount=0m;
        }
        public static void ShowPage(Form owner, Form page)
        {
            owner.Hide(); page.StartPosition=FormStartPosition.CenterScreen;
            page.FormClosed += delegate { owner.Show(); owner.Activate(); };
            page.Show(owner);
        }
    }
    public static class CafeStyle
    {
        public static readonly Color Background=Color.Moccasin;
        public static readonly Color Brown=Color.SaddleBrown;
        public static void StyleButton(Button b)
        {
            b.BackColor = Color.SandyBrown;
            b.ForeColor = Brown;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Brown;
            b.FlatAppearance.MouseOverBackColor = Color.SandyBrown;
            b.FlatAppearance.MouseDownBackColor = Color.SandyBrown;
            b.UseVisualStyleBackColor = false;
            b.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }
        public static Label Heading(string value,int x,int y,int w)
        { return new Label { Text=value, Location=new Point(x,y), Size=new Size(w,38), Font=new Font("Segoe Print",16,FontStyle.Bold), ForeColor=Brown }; }
    }
}
