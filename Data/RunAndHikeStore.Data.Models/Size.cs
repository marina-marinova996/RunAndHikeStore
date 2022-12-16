namespace RunAndHikeStore.Data.Models
{
    using RunAndHikeStore.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RunAndHikeStore.Common.GlobalConstants.Size;

    public class Size : BaseDeletableModel<string>
    {
        public Size()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<ProductSize>();
        }

        /// <summary>
        /// Gets or sets Size name.
        /// </summary>
        [Required]
        [StringLength(ProductSizeNameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// Foreign Key to table Product type.
        /// </summary>
        [Required]
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Navigation property to Product type table.
        /// </summary>
        [ForeignKey(nameof(ProductTypeId))]
        public ProductType ProductType { get; set; }

        public ICollection<ProductSize> Products { get; set; }
    }
}