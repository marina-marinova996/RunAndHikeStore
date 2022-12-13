namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Web.ViewModels.Order.Enum;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllOrdersViewModel
    {
        /// <summary>
        /// Orders per page.
        /// </summary>
        public const int OrdersPerPage = 6;

        /// <summary>
        /// Current page for pagination.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total records for pagination.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Orders sorting.
        /// </summary>
        public OrdersSorting Sorting { get; set; }

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Orders.
        /// </summary>
        public IEnumerable<ManageOrderViewModel> Orders { get; set; }
    }
}
