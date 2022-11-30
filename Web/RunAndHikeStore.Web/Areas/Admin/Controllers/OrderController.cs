namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RunAndHikeStore.Services;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Order;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants;

    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            this.orderService = _orderService;
        }

        /// <summary>
        /// Manage Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageOrders([FromQuery] AllOrdersViewModel query)
        {
            this.ViewData["Title"] = "Manage Orders";

            try
            {
                var queryResult = await orderService.GetAllOrdersAsync(
                                                                            query.SearchTerm,
                                                                            query.CurrentPage,
                                                                            AllOrdersViewModel.OrdersPerPage,
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

        /// <summary>
        /// Edit Order, find by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["Title"] = "Edit Order";
            try
            {

                EditOrderDetailViewModel order = await orderService.GetViewModelForEditByIdAsync(id);

                order.PaymentStatuses = this.orderService.GetPaymentStatuses();
                order.OrderStatuses = this.orderService.GetOrderStatuses();

                if (order == null)
                {
                    return BadRequest();
                }

                return View(order);
            }
            catch (System.Exception)
            {

                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        /// <summary>
        /// Edit Order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditOrderDetailViewModel model)
        {
            try
            {
                await orderService.Edit(model);
                TempData[MessageConstant.SuccessMessage] = "Successfully editted!";

                return RedirectToAction("ManageOrders", "Order");
            }
            catch (System.Exception)
            {

                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        /// <summary>
        /// Delete Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await orderService.Delete(id);

            return RedirectToAction(nameof(this.ManageOrders));
        }
    }
}
