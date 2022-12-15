
using RunAndHikeStore.Data.Models;
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
        Task CreateAsync(CreateOrderViewModel model, string customerId);

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

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetAllOrders();

        /// <summary>
        /// Check if order exists.
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}