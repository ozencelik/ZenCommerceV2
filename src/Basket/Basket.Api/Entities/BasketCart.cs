using System.Collections.Generic;

namespace Basket.Api.Entities
{
    public class BasketCart
    {
        public BasketCart()
        {
            Items = new List<BasketItem>();
        }

        public BasketCart(string userName)
        {
            UserName = userName;
            Items = new List<BasketItem>();
        }

        public string UserName { get; set; }

        public IList<BasketItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                    totalPrice += item.Price * item.Quantity;

                return totalPrice;
            }
        }
    }
}
