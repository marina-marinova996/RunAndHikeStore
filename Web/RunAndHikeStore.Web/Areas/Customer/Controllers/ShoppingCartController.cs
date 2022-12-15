﻿namespace RunAndHikeStore.Web.Areas.Customer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ClaimsPrincipalExtensions;
    using RunAndHikeStore.Web.ViewModels.Order;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Threading.Tasks;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ICustomerService customerService, IProductService productService, IOrderService orderService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
            this.orderService = orderService;
            this.customerService = customerService;
        }

        /// <summary>
        /// View Shopping Cart Items.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Add to Cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove item from cart.
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveCartItem(string cartItemId)
        {
            try
            {
                if (await this.shoppingCartService.ExistsCartItemById(cartItemId))
                {
                    await shoppingCartService.RemoveCartItem(cartItemId);
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Remove all Cart items.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get Create order view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var model = new CreateOrderViewModel();
            try
            {
                var userId = User.Id();

                var user = await shoppingCartService.FindUserById(userId);

                if (user != null)
                {
                    model.CartItems = await shoppingCartService.GetAllCartItems(userId);
                    model.BillingDetails = await customerService.GetCustomerBillingDetailsByUserId(userId);
                    model.DeliveryAddress = await customerService.GetCustomerDeliveryAddressByUserId(userId);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }

        /// <summary>
        /// Create order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var customerId = User.Id();

            model.CartItems = await shoppingCartService.GetAllCartItems(customerId);
            await orderService.CreateAsync(model, customerId);

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
