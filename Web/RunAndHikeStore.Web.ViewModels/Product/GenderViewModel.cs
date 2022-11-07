using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.Product
{
    public class GenderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}