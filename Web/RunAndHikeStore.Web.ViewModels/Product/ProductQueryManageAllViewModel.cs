namespace RunAndHikeStore.Web.ViewModels.Product
{
    public class ProductQueryManageAllViewModel : ProductQueryViewModel
    {
        /// <summary>
        /// Color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gender Id.
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Product Type.
        /// </summary>
        public string ProductType { get; set; }
    }
}
