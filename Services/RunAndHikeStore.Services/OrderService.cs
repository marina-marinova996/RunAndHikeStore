
namespace RunAndHikeStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Customer;
    using RunAndHikeStore.Web.ViewModels.Order;
    using RunAndHikeStore.Web.ViewModels.Order.Enum;

    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        public OrderService(IRepository _repo)
        {
            this.repo = _repo;
        }

        /// <summary>
        /// Create order.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateOrderViewModel model, string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentException("Unknown customer");
            }

            if (model.CartItems.Any())
            {
                var order = new Order()
                {
                    CustomerId = customerId,
                    BillingDetailsId = model.BillingDetails.Id,
                    OrderStatus = OrderStatus.Pending,
                };

                var orderDetails = model.CartItems.Select(c => new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = c.ProductId,
                    OrderQuantity = c.Quantity,
                    Size = c.Size,
                    UnitPrice = c.Product.UnitPrice,
                }).ToList();

                order.OrderDetails = orderDetails;

                foreach (var orderDetail in order.OrderDetails)
                {
                    var productSize = await this.repo.All<ProductSize>().Where(ps => ps.ProductId == orderDetail.ProductId && ps.Size.Name == orderDetail.Size).FirstOrDefaultAsync();

                    if (productSize != null)
                    {
                        if (orderDetail.OrderQuantity > productSize.UnitsInStock)
                        {
                            throw new ArgumentException("Not enough units in stock");
                        }
                    }
                }

                await this.repo.AddAsync(order);
                var cartItems = await this.repo.All<CartItem>().Include(c => c.ShoppingCart).Where(c => c.ShoppingCart.ApplicationUser.Id == customerId).ToListAsync();
                this.repo.DeleteRange(cartItems);
                await this.repo.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Missing cart items");
            }
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="ordersPerPage"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AllOrdersViewModel> GetAllOrdersAsync(string searchTerm, int currentPage = 1, int ordersPerPage = 6, OrdersSorting sorting = OrdersSorting.Newest)
        {
            var ordersQuery = this.repo.AsNoTracking<Order>()
                                    .Where(o => o.IsDeleted == false)
                                    .Include(o => o.Customer)
                                    .Include(o => o.OrderDetails)
                                    .Include(o => o.BillingDetails)
                                    .AsQueryable();

            if (ordersQuery == null)
            {
                throw new ArgumentException("No orders");
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                ordersQuery = ordersQuery.Where(o => o.BillingDetails.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                                                o.BillingDetails.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                                                o.Customer.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                                o.Id.ToLower().Contains(searchTerm.ToLower()) ||
                                                o.OrderDate.ToString().Contains(searchTerm.ToLower()));
            }

            ordersQuery = sorting switch
            {
                OrdersSorting.Oldest => ordersQuery
                                                    .OrderBy(o => o.OrderDate),
                _ => ordersQuery.OrderByDescending(o => o.OrderDate),
            };

            var orders = ordersQuery
                                    .Skip((currentPage - 1) * ordersPerPage)
                                    .Take(ordersPerPage)
                                    .Select(o => new ManageOrderViewModel()
                                    {
                                        OrderId = o.Id,
                                        Email = o.Customer.Email,
                                        TotalPrice = o.OrderDetails.Sum(od => od.UnitPrice * od.OrderQuantity).ToString("F2"),
                                        OrderDate = o.OrderDate.ToString("dd-MM-yyyy"),
                                        OrderStatus = GetOrderStatusAsStringById((int)o.OrderStatus),
                                        PaymentStatus = GetPaymentStatusAsStringById((int)o.PaymentStatus),
                                        BillingDetails = new BillingDetailsFormViewModel()
                                        {
                                            FirstName = o.BillingDetails.FirstName,
                                            LastName = o.BillingDetails.LastName,
                                            StreetAddress = o.BillingDetails.StreetAddress,
                                            City = o.BillingDetails.City,
                                            Country = o.BillingDetails.Country,
                                            PostalCode = o.BillingDetails.PostalCode,
                                            PhoneNumber = o.BillingDetails.PhoneNumber,
                                        },
                                    });

            var totalRecords = ordersQuery.Count();

            return new AllOrdersViewModel()
            {
                Orders = orders,
                TotalRecordsCount = totalRecords,
            };
        }

        /// <summary>
        /// Get Order Status as string.
        /// </summary>
        /// <returns></returns>
        public static string GetOrderStatusAsStringById(int statusId)
        {
            switch (statusId)
            {
                case (int)OrderStatus.Approved:
                    return "Approved";
                    break;
                case (int)OrderStatus.Declined:
                    return "Declined";
                    break;
                case (int)OrderStatus.Shipped:
                    return "Shipped";
                    break;
            }

            return "Pending";
        }

        /// <summary>
        /// Get Payment Status as string.
        /// </summary>
        /// <returns></returns>
        public static string GetPaymentStatusAsStringById(int statusId)
        {
            switch (statusId)
            {
                case (int)PaymentStatus.Paid:
                    return "Paid";
                    break;
                case (int)PaymentStatus.NotPaid:
                    return "Not Paid";
            }

            return "Error";
        }

        /// <summary>
        /// Delete Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            var order = await this.repo.All<Order>()
                                       .Where(o => o.IsDeleted == false)
                                       .FirstOrDefaultAsync();

            if (order != null)
            {
                order.IsDeleted = true;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit Order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(EditOrderDetailViewModel model)
        {
            var order = await this.repo.All<Order>()
                                       .Where(o => o.IsDeleted == false)
                                       .Include(o => o.OrderDetails.Where(d => d.IsDeleted == false))
                                       .Where(o => o.Id == model.OrderId)
                                       .FirstOrDefaultAsync();

            if (order != null)
            {
                order.OrderStatus = (OrderStatus)model.OrderStatusId;
                order.PaymentStatus = (PaymentStatus)model.PaymentStatusId;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get View Model for Edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditOrderDetailViewModel> GetViewModelForEditByIdAsync(string id)
        {
            var model = await this.repo.All<Order>()
                              .Where(o => o.IsDeleted == false)
                              .Where(o => o.Id == id)
                              .Include(o => o.OrderDetails.Where(d => d.IsDeleted == false))
                              .Select(o => new EditOrderDetailViewModel()
                              {
                                  OrderId = o.Id,
                                  OrderDate = o.OrderDate.ToString("dd-MM-yyyy"),
                                  Email = o.Customer.Email,
                                  OrderStatusId = (int)o.OrderStatus,
                                  PaymentStatusId = (int)o.PaymentStatus,
                                  TotalPrice = o.OrderDetails.Sum(od => od.UnitPrice * od.OrderQuantity).ToString("F2"),
                                  BillingDetails = new EditBillingDetailsViewModel()
                                  {
                                      Id = o.BillingDetailsId,
                                      FirstName = o.BillingDetails.FirstName,
                                      LastName = o.BillingDetails.LastName,
                                      StreetAddress = o.BillingDetails.StreetAddress,
                                      City = o.BillingDetails.City,
                                      Country = o.BillingDetails.Country,
                                      PostalCode = o.BillingDetails.PostalCode,
                                      PhoneNumber = o.BillingDetails.PhoneNumber,
                                  },
                              }).FirstOrDefaultAsync();

            return model;
        }

        /// <summary>
        /// Get all order statuses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderStatusViewModel> GetOrderStatuses()
        {
            List<OrderStatusViewModel> orderStatuses = new List<OrderStatusViewModel>();

            orderStatuses.Add(new OrderStatusViewModel
            {
                Id = (int)OrderStatus.Approved,
                Name = "Approved",
            });

            orderStatuses.Add(new OrderStatusViewModel
            {
                Id = (int)OrderStatus.Declined,
                Name = "Declined",
            });

            orderStatuses.Add(new OrderStatusViewModel
            {
                Id = (int)OrderStatus.Shipped,
                Name = "Shipped",
            });

            orderStatuses.Add(new OrderStatusViewModel
            {
                Id = (int)OrderStatus.Pending,
                Name = "Pending",
            });

            return orderStatuses;
        }

        /// <summary>
        /// Get all payment statuses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaymentStatusViewModel> GetPaymentStatuses()
        {
            List<PaymentStatusViewModel> paymentStatuses = new List<PaymentStatusViewModel>();

            paymentStatuses.Add(new PaymentStatusViewModel
            {
                Id = (int)PaymentStatus.Paid,
                Name = "Paid",
            });

            paymentStatuses.Add(new PaymentStatusViewModel
            {
                Id = (int)PaymentStatus.NotPaid,
                Name = "Not Paid",
            });

            return paymentStatuses;
        }

        /// <summary>
        /// Get all orders by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="ordersPerPage"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task<CustomerOrdersViewModel> GetAllOrdersByUserIdAsync(string userId, int currentPage = 1, int ordersPerPage = 6, OrdersSorting sorting = OrdersSorting.Newest)
        {
            var ordersQuery = this.repo.AsNoTracking<Order>()
                        .Where(o => o.IsDeleted == false)
                        .Include(o => o.Customer)
                        .Where(o => o.CustomerId == userId)
                        .Include(o => o.OrderDetails)
                        .Include(o => o.BillingDetails)
                        .AsQueryable();

            ordersQuery = sorting switch
            {
                OrdersSorting.Newest => ordersQuery
                                                    .OrderByDescending(o => o.Id),
                _ => ordersQuery.OrderBy(o => o.Id),
            };

            var orders = ordersQuery
                                    .Skip((currentPage - 1) * ordersPerPage)
                                    .Take(ordersPerPage)
                                    .Select(o => new CustomerOrderViewModel()
                                    {
                                        OrderId = o.Id,
                                        Email = o.Customer.Email,
                                        TotalPrice = o.OrderDetails.Sum(od => od.UnitPrice * od.OrderQuantity).ToString("F2"),
                                        OrderDate = o.OrderDate.ToString("dd-MM-yyyy"),
                                        OrderStatus = GetOrderStatusAsStringById((int)o.OrderStatus),
                                        PaymentStatus = GetPaymentStatusAsStringById((int)o.PaymentStatus),
                                        BillingDetails = new BillingDetailsFormViewModel()
                                        {
                                            FirstName = o.BillingDetails.FirstName,
                                            LastName = o.BillingDetails.LastName,
                                            StreetAddress = o.BillingDetails.StreetAddress,
                                            City = o.BillingDetails.City,
                                            Country = o.BillingDetails.Country,
                                            PostalCode = o.BillingDetails.PostalCode,
                                            PhoneNumber = o.BillingDetails.PhoneNumber,
                                        },
                                    });

            var totalRecords = ordersQuery.Count();

            return new CustomerOrdersViewModel()
            {
                Orders = orders,
                TotalRecordsCount = totalRecords,
            };
        }

        /// <summary>
        /// Get all orders from DB.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetAllOrders()
        {
            return await this.repo.All<Order>().Include(o => o.OrderDetails).ToListAsync();
        }

        /// <summary>
        /// Check if order exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string id)
        {
            return await repo.All<Order>()
                            .AnyAsync(o => o.Id == id);
        }
    }
}
