namespace RunAndHikeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RunAndHikeStore.Data.Common.Models;
    using static RunAndHikeStore.Common.GlobalConstants.Category;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
                this.Id = Guid.NewGuid().ToString();
                this.Products = new HashSet<CategoryProduct>();
        }

        /// <summary>
        /// Gets or sets category name.
        /// </summary>
        [Required]
        [StringLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// Products Navigation property.
        /// </summary>
        public ICollection<CategoryProduct> Products { get; set; }
    }
}
