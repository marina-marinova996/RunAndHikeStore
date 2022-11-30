namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Web.ViewModels.Order.Enum;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllOrdersViewModel
    {
        public const int OrdersPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public OrdersSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<ManageOrderViewModel> Orders { get; set; }
    }
}
