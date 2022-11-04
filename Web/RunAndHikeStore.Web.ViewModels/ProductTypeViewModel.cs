using System.Collections;
using System.Collections.Generic;
using RunAndHikeStore.Web.ViewModels.Product;

namespace RunAndHikeStore.Web.ViewModels
{
    public class ProductTypeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
