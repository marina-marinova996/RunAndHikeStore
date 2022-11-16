namespace RunAndHikeStore.Web.ViewModels.Product
{
    using RunAndHikeStore.Web.ViewModels.Category;
    using System.Collections.Generic;

    public class EditProductViewModel : AddProductViewModel
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string ProductType { get; set; }

        public string Gender { get; set; }
    }
}
