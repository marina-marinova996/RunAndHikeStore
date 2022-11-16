namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RunAndHikeStore.Common.GlobalConstants.Product;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using RunAndHikeStore.Common;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Size;


    public class AddProductViewModel
    {
        [Required]
        public string ProductNumber { get; set; }

        [Required]
        public string BrandId { get; set; }

        public IEnumerable<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();

        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string ProductTypeId { get; set; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();

        [Required]
        public int GenderId { get; set; }

        public IEnumerable<GenderViewModel> Genders { get; set; } = new List<GenderViewModel>();

        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Unit Price")]
        [Range(typeof(decimal), "0", GlobalConstants.DecimalMaxValue, ConvertValueInInvariantCulture = true)]
        public decimal UnitPrice { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public IEnumerable<string> MultiCategoriesIds { get; set; }
    }
}
