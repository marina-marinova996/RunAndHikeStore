namespace RunAndHikeStore.Data.Models
{
    using RunAndHikeStore.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Brand;

    public class Brand : BaseDeletableModel<string>
    {
        public Brand()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<Product>();
        }

        /// <summary>
        /// Gets or sets the name of the brand.
        /// </summary>
        [Required]
        [StringLength(BrandNameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// FK Products table.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
