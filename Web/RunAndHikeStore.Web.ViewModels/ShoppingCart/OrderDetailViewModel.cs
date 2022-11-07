namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    using RunAndHikeStore.Web.ViewModels.Product;
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
