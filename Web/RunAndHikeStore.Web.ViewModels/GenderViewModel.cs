using System.Collections.Generic;
using RunAndHikeStore.Web.ViewModels.Product;

namespace RunAndHikeStore.Web.ViewModels
{
    public class GenderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}