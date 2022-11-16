using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.Product
{
    public class ProductQueryManageAllViewModel : ProductQueryViewModel
    {
        public string Color { get; set; }

        public int GenderId { get; set; }

        public string Gender { get; set; }

        public string ProductTypeId { get; set; }

        public string ProductType { get; set; }
    }
}
