namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();

        // iterate shopping cart items and multiple by the price & qty
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        //Required for Mapping
        public ShoppingCart()
        {
        }
    }
}
