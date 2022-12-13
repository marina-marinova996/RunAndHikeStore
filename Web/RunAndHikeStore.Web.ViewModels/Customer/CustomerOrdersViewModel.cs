using RunAndHikeStore.Web.ViewModels.Order.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerOrdersViewModel
    {
        /// <summary>
        /// Orders per page.
        /// </summary>
        public const int OrdersPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total Records Count.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Sorting.
        /// </summary>
        public OrdersSorting Sorting { get; set; }

        /// <summary>
        /// Search.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Orders.
        /// </summary>
        public IEnumerable<CustomerOrderViewModel> Orders { get; set; }
    }
}
