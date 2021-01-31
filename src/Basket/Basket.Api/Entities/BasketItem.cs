namespace Basket.Api.Entities
{
    public class BasketItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        
        // Added to upgrade the performance. 
        // We do not want to go to Catalog Api 
        // to get the product name
        public string ProductName { get; set; }
    }
}
