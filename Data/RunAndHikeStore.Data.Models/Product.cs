namespace RunAndHikeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using RunAndHikeStore.Data.Common.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using static RunAndHikeStore.Common.GlobalConstants.Product;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Categories = new HashSet<CategoryProduct>();
            this.Sizes = new HashSet<ProductSize>();
        }

        /// <summary>
        /// Gets or sets item number.
        /// </summary>
        [Required]
        public string ProductNumber { get; set; }

        /// <summary>
        /// Foreign Key Table Brands.
        /// </summary>
        [Required]
        public string BrandId { get; set; }

        /// <summary>
        /// Navigation property brand.
        /// </summary>
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        [Required]
        [StringLength(ProductNameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// Foreign Key Table ProductTypes.
        /// </summary>
        [Required]
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Navigation property product type.
        /// </summary>
        [ForeignKey(nameof(ProductTypeId))]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Color of the product.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets unit price of the product.
        /// </summary>
        [Required]
        [StringLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets image of the product.
        /// </summary>
        [Required]
        [StringLength(ProductImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the product stock of the product.
        /// </summary>
        public ICollection<ProductSize> Sizes { get; set; }

        /// <summary>
        /// Gets or sets gender.
        /// </summary>
        [Required]
        [StringLength(GenderMaxLength)]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets list of categories of the product.
        /// </summary>
        public ICollection<CategoryProduct> Categories { get; set; }
    }
}
