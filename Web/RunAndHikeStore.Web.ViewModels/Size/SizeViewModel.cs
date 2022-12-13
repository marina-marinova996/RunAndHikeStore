using RunAndHikeStore.Web.ViewModels.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Size
{
    public class SizeViewModel
    {
        /// <summary>
        /// Size Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Size Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product Type.
        /// </summary>
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Product Types.
        /// </summary>
        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}