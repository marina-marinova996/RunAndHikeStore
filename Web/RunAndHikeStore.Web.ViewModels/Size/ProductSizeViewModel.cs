using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.Size
{
    public class ProductSizeViewModel
    {
        public string ProductId { get; set; }

        public string SizeId { get; set; }

        public string SizeName { get; set; }

        public int UnitsInStock { get; set; }
    }
}
