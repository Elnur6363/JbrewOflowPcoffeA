using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApplication1
{
    public class CartItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total
        {
            get { return Price * Quantity; }
        }
    }

    public static class Cart
    {
        public static List<CartItem> Items = new List<CartItem>();

        public static void Add(string name, decimal price)
        {
            CartItem item = Items.FirstOrDefault(x => x.Name == name);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                Items.Add(new CartItem
                {
                    Name = name,
                    Price = price,
                    Quantity = 1
                });
            }
        }

        public static decimal GetTotal()
        {
            return Items.Sum(x => x.Total);
        }

        public static void Clear()
        {
            Items.Clear();
        }
    }
}
