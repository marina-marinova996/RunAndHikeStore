namespace RunAndHikeStore.Web.Controllers
{
    using RunAndHikeStore.Services.Contracts;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }
    }
}
