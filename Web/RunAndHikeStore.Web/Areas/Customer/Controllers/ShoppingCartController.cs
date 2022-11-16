namespace RunAndHikeStore.Web.Areas.Customer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ClaimsPrincipalExtensions;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Customer")]
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
                var userId = User.Id();

                var user = await shoppingCartService.FindUserById(userId);

                if (user != null && user.ShoppingCart.CartItems != null)
                {
                    model.CartItems = await shoppingCartService.GetAllCartItems(userId);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            ViewData["Title"] = "Shopping Cart";

            return View(model);
        }

        public async Task<IActionResult> AddToCart(string productId, string sizeId)
        {
            try
            {
                var userId = User.Id();

                ProductViewModel product = await productService.GetByIdAsync(productId);

                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Details", "Product", product);
                }

                var cartModel = await shoppingCartService.FindCartItem(productId, userId);

                if (await shoppingCartService.IsInStock(productId, sizeId))
                {
                    await shoppingCartService.AddToCart(productId, userId, sizeId, 1);
                }
                else
                {
                    ModelState.AddModelError("", "There is no unit in stock!");
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction("All", "Product");
        }

        public async Task<IActionResult> RemoveCartItem(string cartItemId)
        {
            try
            {
                await shoppingCartService.RemoveCartItem(cartItemId);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> RemoveAllCartItems()
        {
            var userId = User.Id();

            try
            {
                await shoppingCartService.RemoveAllCartItems(userId);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction(nameof(this.Index));
        }
    }
}
