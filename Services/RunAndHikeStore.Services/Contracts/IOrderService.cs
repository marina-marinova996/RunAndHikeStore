﻿using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Order;
using RunAndHikeStore.Web.ViewModels.Order.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IOrderService
    {
        /// <summary>
        /// Create Order.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task CreateAsync(OrderViewModel model, string customerId);

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="ordersPerPage"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<AllOrdersViewModel> GetAllOrdersAsync(string searchTerm, int currentPage = 1, int ordersPerPage = 6, OrdersSorting sorting = OrdersSorting.Newest);

        /// <summary>
        /// Delete order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);

        /// <summary>
        /// Edit order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Edit(EditOrderDetailViewModel model);

        /// <summary>
        /// Get Order View Model for Edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditOrderDetailViewModel> GetViewModelForEditByIdAsync(string id);

        /// <summary>
        /// Add Billing Details for order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BillingDetails> AddBillingDetails(BillingDetailsFormViewModel model);

        /// <summary>
        /// Get all Order Statuses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderStatusViewModel> GetOrderStatuses();

        /// <summary>
        /// Get all Payment Statuses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PaymentStatusViewModel> GetPaymentStatuses();


        /// <summary>
        /// Get all orders for user.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="ordersPerPage"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<CustomerOrdersViewModel> GetAllOrdersByUserIdAsync(string userId, int currentPage = 1, int ordersPerPage = 6, OrdersSorting sorting = OrdersSorting.Newest);
    }
}