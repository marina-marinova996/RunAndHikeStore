using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Stock
{
    public class AllStocksViewModel
    {
        public const int ProductsPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public IEnumerable<EditStockViewModel> Stocks { get; set; } = new List<EditStockViewModel>();

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
