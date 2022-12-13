using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Stock
{
    public class AllStocksViewModel
    {
        /// <summary>
        /// Products Per Page.
        /// </summary>
        public const int ProductsPerPage = 6;

        /// <summary>
        /// Current Page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total Records for pagination.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Stocks.
        /// </summary>
        public IEnumerable<EditStockViewModel> Stocks { get; set; } = new List<EditStockViewModel>();

        /// <summary>
        /// Search.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
