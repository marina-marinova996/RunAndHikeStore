namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using RunAndHikeStore.Common;
    using RunAndHikeStore.Web.ViewModels.Brand;

    using static RunAndHikeStore.Common.GlobalConstants.Product;

    public class AddProductViewModel
    {
        /// <summary>
        /// Product Number.
        /// </summary>
        [Required]
        public string ProductNumber { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        [Required]
        public string BrandId { get; set; }

        /// <summary>
        /// Brands.
        /// </summary>
        public IEnumerable<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();

        /// <summary>
        /// Product Name.
        /// </summary>
        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; set; }

        /// <summary>
        /// Color.
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        [Required]
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Product Types.
        /// </summary>
        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();

        /// <summary>
        /// Gender Id.
        /// </summary>
        [Required]
        public int GenderId { get; set; }

        /// <summary>
        /// Genders.
        /// </summary>
        public IEnumerable<GenderViewModel> Genders { get; set; } = new List<GenderViewModel>();

        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Unit Price")]
        [Range(typeof(decimal), "0", GlobalConstants.DecimalMaxValue, ConvertValueInInvariantCulture = true)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Image URL.
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Multi Categories Ids - used for multiple select.
        /// </summary>
        [Required]
        public IEnumerable<string> MultiCategoriesIds { get; set; }
    }
}
