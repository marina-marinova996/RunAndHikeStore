using Microsoft.AspNetCore.Mvc;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ClaimsPrincipalExtensions;
using RunAndHikeStore.Web.ViewModels.Customer;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.Areas.Customer.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;

        public CustomerController(IOrderService _orderService, ICustomerService _customerService)
        {
            this.orderService = _orderService;
            this.customerService = _customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            try
            {
                var userId = User.Id();

                if((await customerService.IsCustomerHavingBillingDetails(User.Id())))
                {
                    var model = new EditCustomerDetailsViewModel();

                    model.BillingDetails = await this.customerService.GetCustomerBillingDetailsByUserId(userId);
                    model.Address = await this.customerService.GetCustomerDeliveryAddressByUserId(userId);

                    return this.View("EditDetails", model);
                }
                else
                {
                    var model = new CustomerDetailsViewModel();

                    return this.View("AddDetails", model);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDetails(EditCustomerDetailsViewModel model)
        {
            try
            {
                var userId = User.Id();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await this.customerService.EditBillingDetails(model.BillingDetails);
                await this.customerService.EditDeliveryAddress(model.Address);

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDetails(CustomerDetailsViewModel model)
        {
            try
            {
                var userId = User.Id();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await this.customerService.AddBillingDetails(model.BillingDetails, userId);
                await this.customerService.AddDeliveryAddress(model.Address, userId);

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// View Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Orders([FromQuery] CustomerOrdersViewModel query)
        {
            this.ViewData["Title"] = "My Orders";

            try
            {
                var userId = User.Id();

                var queryResult = await orderService.GetAllOrdersByUserIdAsync(
                                                                            userId,
                                                                            query.CurrentPage,
                                                                            CustomerOrdersViewModel.OrdersPerPage,
                                                                            query.Sorting);

                query.Orders = queryResult.Orders;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                return View(query);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }
    }
}
