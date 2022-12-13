using RunAndHikeStore.Web.ViewModels.Brand;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RunAndHikeStore.Web.ViewModels.Category
{
    public class AllCategoriesViewModel
    {
        /// <summary>
        /// Categories per page for pagination.
        /// </summary>
        public const int CategoriesPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total records for pagination.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Categories.
        /// </summary>
        public IEnumerable<EditCategoryViewModel> Categories { get; set; } = new List<EditCategoryViewModel>();

        /// <summary>
        /// Search.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
