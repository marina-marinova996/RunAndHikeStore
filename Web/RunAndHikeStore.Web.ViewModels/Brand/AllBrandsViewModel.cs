using RunAndHikeStore.Web.ViewModels.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RunAndHikeStore.Web.ViewModels.Brand
{
    public class AllBrandsViewModel
    {
        /// <summary>
        /// Brands per page for pagination.
        /// </summary>
        public const int BrandsPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total records for pagination.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// All brands.
        /// </summary>
        public IEnumerable<EditBrandViewModel> Brands { get; set; } = new List<EditBrandViewModel>();

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
