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
        public const int BrandsPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        public IEnumerable<EditBrandViewModel> Brands { get; set; } = new List<EditBrandViewModel>();

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
