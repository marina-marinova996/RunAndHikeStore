using RunAndHikeStore.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.ProductType;

namespace RunAndHikeStore.Data.Models
{
    public class ProductType : BaseDeletableModel<string>
    {
        public ProductType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        [Required]
        [StringLength(ProductTypeNameMaxLength)]
        public string Name { get; set; }
    }
}
