namespace RunAndHikeStore.Web.ViewModels.Size
{
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Size;

    public class AddSizeViewModel
    {
        [Required]
        [StringLength(ProductSizeNameMaxLength, MinimumLength = ProductSizeNameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        [Required]
        public string ProductTypeId { get; set; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}
