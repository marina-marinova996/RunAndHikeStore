namespace RunAndHikeStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ClaimsPrincipalExtensions;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductService productService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductService productService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ShoppingCartViewModel();
            try
            {
                var userId = this.User.Id();

                var user = await this.shoppingCartService.FindUserById(userId);

                if (user != null && user.ShoppingCart.CartItems != null)
                {
                    model.CartItems = await this.shoppingCartService.GetAllCartItems(userId);
                }
            }
            catch
            {
                this.ModelState.AddModelError("", "Something went wrong");
            }

            this.ViewData["Title"] = "Shopping Cart";

            return this.View(model);
        }

        public async Task<IActionResult> AddToCart(string productId, string sizeId)
        {
            try
            {
                var userId = this.User.Id();

                ProductViewModel product = await this.productService.GetByIdAsync(productId);

                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction("Details", "Product", product);
                }

                var cartModel = await this.shoppingCartService.FindCartItem(productId, userId);

                if (await this.shoppingCartService.IsInStock(productId, sizeId))
                {
                    await this.shoppingCartService.AddToCart(productId, userId, sizeId, 1);
                }
                else
                {
                    this.ModelState.AddModelError("", "There is no unit in stock!");
                }
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
            }

            return this.RedirectToAction("All", "Product");
        }

        public async Task<IActionResult> RemoveCartItem(string cartItemId)
        {
            try
            {
                await this.shoppingCartService.RemoveCartItem(cartItemId);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> RemoveAllCartItems()
        {
            var userId = this.User.Id();

            try
            {
                await this.shoppingCartService.RemoveAllCartItems(userId);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
