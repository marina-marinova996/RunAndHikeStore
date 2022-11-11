using RunAndHikeStore.Web.ViewModels.Stock;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Size
{
    public class AllSizesViewModel
    {
        public const int SizesPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();


        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
