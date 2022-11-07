using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartItemViewModel> CartItems { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
