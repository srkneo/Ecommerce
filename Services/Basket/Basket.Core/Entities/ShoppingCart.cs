﻿
namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
        public ShoppingCart() { }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
