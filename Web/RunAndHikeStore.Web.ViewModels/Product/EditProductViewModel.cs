namespace RunAndHikeStore.Web.ViewModels.Product
{
    using RunAndHikeStore.Web.ViewModels.Category;
    using System.Collections.Generic;

    public class EditProductViewModel : AddProductViewModel
    {
        /// <summary>
        /// Product Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Brand Name.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Product Type Name.
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender { get; set; }
    }
}
