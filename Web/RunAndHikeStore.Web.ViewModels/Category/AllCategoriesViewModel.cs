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
        public const int CategoriesPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public IEnumerable<EditCategoryViewModel> Categories { get; set; } = new List<EditCategoryViewModel>();

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
