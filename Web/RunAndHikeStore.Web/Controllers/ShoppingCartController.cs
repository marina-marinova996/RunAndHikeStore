namespace RunAndHikeStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Threading.Tasks;

    public class ShoppingCartController : BaseController
    {
        //private readonly IShoppingCartService shoppingCartService;

        //public ShoppingCartController(IShoppingCartService shoppingCartService)
        //{
        //    this.shoppingCartService = shoppingCartService;
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ShoppingCartViewModel();

            this.ViewData["Title"] = "Shopping Cart";

            return this.View(model);
        }
    }
}
