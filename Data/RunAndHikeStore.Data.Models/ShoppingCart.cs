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
            this.CartItems = new HashSet<CartItem>();
        }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
