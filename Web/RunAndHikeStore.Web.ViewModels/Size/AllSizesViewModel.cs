using RunAndHikeStore.Web.ViewModels.Stock;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Size
{
    public class AllSizesViewModel
    {
        /// <summary>
        /// Sizes per page.
        /// </summary>
        public const int SizesPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total records per page.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Sizes.
        /// </summary>
        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
