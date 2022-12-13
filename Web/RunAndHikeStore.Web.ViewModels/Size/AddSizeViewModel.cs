namespace RunAndHikeStore.Web.ViewModels.Size
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RunAndHikeStore.Web.ViewModels.Product;

    using static RunAndHikeStore.Common.GlobalConstants.Size;

    public class AddSizeViewModel
    {
        /// <summary>
        /// Size Name.
        /// </summary>
        [Required]
        [StringLength(ProductSizeNameMaxLength, MinimumLength = ProductSizeNameMinLength)]
        public string Name { get; set; }

        /// <summary>
        /// Product Type.
        /// </summary>
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        [Required]
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Product Types.
        /// </summary>
        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}
