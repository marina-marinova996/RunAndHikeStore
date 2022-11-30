using RunAndHikeStore.Web.ViewModels.Order.Enum;
using RunAndHikeStore.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerOrdersViewModel
    {
        public const int OrdersPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public OrdersSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<CustomerOrderViewModel> Orders { get; set; }
    }
}
