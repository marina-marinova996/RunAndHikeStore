namespace RunAndHikeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using RunAndHikeStore.Data.Common.Models;

    public class ShoppingCart : BaseModel<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<ShoppingCartProduct>();
        }

        /// <summary>
        /// Foreign Key to ApplicationUsers table.
        /// </summary>
        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Navigation property to ApplicationUsers table.
        /// </summary>
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<ShoppingCartProduct> Products { get; set; }
    }
}
