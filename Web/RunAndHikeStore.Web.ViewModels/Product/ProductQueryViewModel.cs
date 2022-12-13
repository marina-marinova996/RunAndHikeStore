namespace RunAndHikeStore.Web.ViewModels.Product
{
    public class ProductQueryViewModel
    {
        /// <summary>
        /// Product Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Product Number.
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Product Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Image URL.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Unit Price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// Brand
        /// </summary>
        public string Brand { get; set; }
    }
}
