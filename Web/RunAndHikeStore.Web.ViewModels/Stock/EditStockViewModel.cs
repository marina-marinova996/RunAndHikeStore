using RunAndHikeStore.Web.ViewModels.Size;
using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.Stock
{
    public class EditStockViewModel
    {
        public string ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductType { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Color { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string SizeId { get; set; }

        public string SizeName { get; set; }

        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();
    }
}
