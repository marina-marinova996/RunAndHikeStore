
using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Models.Enums;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Order;
using RunAndHikeStore.Web.ViewModels.Order.Enum;
using RunAndHikeStore.Web.ViewModels.Product;
using RunAndHikeStore.Web.ViewModels.ShoppingCart;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class OrderServiceTests
    {
        private IRepository repo;
        private IOrderService orderService;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("RunHikeDB")
                .Options;

            dbContext = new ApplicationDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
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


        [Test]
        public async Task TestDeleteOrder()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var order = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Approved,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            await orderService.Delete(order.Id.ToString());

            var dbOrders = await this.orderService.GetAllOrders();
            var dbOrder = dbOrders.FirstOrDefault();
            var isActive = dbOrders.Contains(dbOrder);

            Assert.AreEqual(false, isActive);
        }

        [Test]
        public async Task TestEditOrder()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var order = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            var edittedModel = new EditOrderDetailViewModel()
            {
                OrderId = "123456",
                OrderStatusId = (int)Data.Models.Enums.OrderStatus.Approved,
                PaymentStatusId = (int)Data.Models.Enums.PaymentStatus.Paid,
            };

            await orderService.Edit(edittedModel);

            var dbOrder = await repo.GetByIdAsync<Order>("123456");

            Assert.True((int)dbOrder.OrderStatus == (int)Data.Models.Enums.OrderStatus.Approved);
            Assert.True((int)dbOrder.PaymentStatus == (int)Data.Models.Enums.PaymentStatus.Paid);
        }

        [Test]
        public async Task TestCreateOrder()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
                ShoppingCartId = "123456",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails()
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var deliveryAddress = new Address()
            {
                Id = "579939b8-ea8e-4daa-8c81-0635a3f7ff73",
                StreetAddress = "Osmi Primorski Polk 122",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
            };

            await repo.AddAsync(deliveryAddress);
            await repo.SaveChangesAsync();

            var cartItems = new List<CartItemViewModel>();

            var product = new Product()
            {
                Id = "99214889-61aa-4285-a80b-1ae5281f11bb",
                ProductNumber = "45656519",
                Name = "Pace 2",
                Description = "The Coros Pace 2 test description",
                UnitPrice = 180m,
                Color = "Orange",
                ImageUrl = "12356789",
                Gender = Data.Models.Enums.Gender.Unisex,
                BrandId = "f1b41645-1b83-4975-a5d1-26c713c25321",
                ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450"
            };

            await repo.AddAsync(product);
            await repo.SaveChangesAsync();

            var size = new Size()
            {

                Id = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                Name = "One Size",
                ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450",
            };


            await repo.AddAsync(size);
            await repo.SaveChangesAsync();

            var productSize = new ProductSize
            {
                ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                SizeId = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                UnitsInStock = 2,
            };

            await repo.AddAsync(productSize);
            await repo.SaveChangesAsync();

            var cartItem = new CartItem()
            {
                Id = "123",
                ShoppingCartId = "123456",
                ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                Product = product,
                SizeId = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                Size = size,
            };

            await repo.AddAsync(cartItem);
            await repo.SaveChangesAsync();

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Color = product.Color,
                ImageUrl = product.ImageUrl,
                Gender = "Unisex",
                BrandId = product.BrandId,
                ProductTypeId = product.ProductTypeId,
            };

            var cartItemModel = new CartItemViewModel()
            {
                Id = cartItem.Id,
                ShoppingCartId = cartItem.ShoppingCartId,
                ProductId = cartItem.ProductId,
                SizeId = cartItem.SizeId,
                ApplicationUserId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                Total = (180).ToString(),
                Quantity = 1,
                Size = cartItem.Size.Name,
                Product = productViewModel,
            };

            cartItems.Add(cartItemModel);

            var model = new CreateOrderViewModel();

            model.BillingDetails = new EditBillingDetailsViewModel()
            {
                Id = billingDetails.Id,
                FirstName = billingDetails.FirstName,
                LastName = billingDetails.LastName,
                StreetAddress = billingDetails.StreetAddress,
                City = billingDetails.City,
                Country = billingDetails.Country,
                PostalCode = billingDetails.PostalCode,
                PhoneNumber = billingDetails.PhoneNumber,

            };

            model.DeliveryAddress = new EditAddressViewModel()
            {
                Id = deliveryAddress.Id,
                StreetAddress = deliveryAddress.StreetAddress,
                City = deliveryAddress.City,
                Country = deliveryAddress.Country,
                PostalCode = deliveryAddress.PostalCode,
            };

            model.CartItems = cartItems;

            await orderService.CreateAsync(model, user.Id);

            var dbOrders = await orderService.GetAllOrders();
            var dbOrder = dbOrders.FirstOrDefault();
            var dbOrderDetails = dbOrder.OrderDetails.FirstOrDefault();

            Assert.That(billingDetails.FirstName, Is.EqualTo(dbOrder.BillingDetails.FirstName));
            Assert.That(billingDetails.LastName, Is.EqualTo(dbOrder.BillingDetails.LastName));
            Assert.That(billingDetails.Country, Is.EqualTo(dbOrder.BillingDetails.Country));
            Assert.That(billingDetails.City, Is.EqualTo(dbOrder.BillingDetails.City));
            Assert.That(billingDetails.StreetAddress, Is.EqualTo(dbOrder.BillingDetails.StreetAddress));
            Assert.That(billingDetails.PostalCode, Is.EqualTo(dbOrder.BillingDetails.PostalCode));
            Assert.That(billingDetails.CustomerId, Is.EqualTo(dbOrder.BillingDetails.CustomerId));
            Assert.True(dbOrder.OrderDetails.Count == 1);
            Assert.True(dbOrderDetails.UnitPrice == product.UnitPrice);
            Assert.True(dbOrderDetails.OrderQuantity == cartItemModel.Quantity);
            Assert.True(dbOrderDetails.ProductId == productViewModel.Id);
        }

        [Test]
        public async Task TestGetAllOrders()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var orders = new List<Order>();

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var order = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            var secondOrder = new Order
            {
                Id = "1234567",
                OrderDate = new DateTime(2022, 12, 8),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            orders.Add(order);
            orders.Add(secondOrder);
            await repo.AddRangeAsync(orders);
            await repo.SaveChangesAsync();

            var dbOrders = await this.orderService.GetAllOrders();

            Assert.That(orders.Count, Is.EqualTo(dbOrders.Count()));
        }

        [Test]
        [TestCase("Ivan", 1, 6)]
        [TestCase("Petrov", 1, 6)]
        [TestCase("ivan@gmail.com", 1, 6)]
        public async Task TestGetAllAsync(string searchTerm, int currentPage = 1, int brandsPerPage = 6, OrdersSorting sorting = OrdersSorting.Newest)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var orders = new List<Order>();

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            var secondUser = new ApplicationUser()
            {
                Id = "bc519db8-e466-49ed-a0b4-0ea89282c076",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@runandhikestore.com",
                UserName = "admin@runandhikestore.com",
            };

            await repo.AddAsync(user);
            await repo.AddAsync(secondUser);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            var billingDetailsSecondUser = new BillingDetails
            {
                Id = "79939b8-ea8e-4daa-8c81-0635a3f7ff2",
                FirstName = "Admin",
                LastName = "Admin",
                StreetAddress = "Osmi Primorski Polk 12",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665443",
                CustomerId = "bc519db8-e466-49ed-a0b4-0ea89282c076"
            };

            await repo.AddAsync(billingDetails);
            await repo.AddAsync(billingDetailsSecondUser);
            await repo.SaveChangesAsync();

            var firstOrderFirstUser = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            var secondOrderFirstUser = new Order
            {
                Id = "1234567",
                OrderDate = new DateTime(2022, 12, 8),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };


            var firstOrderSecondUser = new Order
            {
                Id = "1234548",
                OrderDate = new DateTime(2022, 12, 8),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "bc519db8-e466-49ed-a0b4-0ea89282c076",
                BillingDetailsId = "79939b8-ea8e-4daa-8c81-0635a3f7ff2",
            };

            orders.Add(firstOrderFirstUser);
            orders.Add(secondOrderFirstUser);
            orders.Add(firstOrderSecondUser);
            await repo.AddRangeAsync(orders);
            await repo.SaveChangesAsync();

            var allOrdersViewModel = await this.orderService.GetAllOrdersAsync(searchTerm, currentPage, brandsPerPage, sorting = OrdersSorting.Newest);

            var IsContaining = allOrdersViewModel.Orders.Any(x => x.BillingDetails.FirstName.Contains(searchTerm) || x.BillingDetails.LastName.Contains(searchTerm) || x.Email.Contains(searchTerm));

            Assert.That(allOrdersViewModel.Orders.Count(), Is.EqualTo(2));
            Assert.That(allOrdersViewModel.Orders.Count(), Is.EqualTo(2));
            Assert.That(allOrdersViewModel.TotalRecordsCount, Is.EqualTo(2));
            Assert.True(IsContaining);
        }

        [Test]
        [TestCase(1)]
        public void TestGetOrderStatusAsStringByIdApproved(int id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatus = "Approved";
            var status = GetOrderStatusAsStringById(id);
            Assert.True(expectedStatus == status);
        }

        [Test]
        [TestCase(2)]
        public void TestGetOrderStatusAsStringByIdDeclined(int id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatus = "Declined";
            var status = GetOrderStatusAsStringById(id);
            Assert.True(expectedStatus == status);
        }

        [TestCase(3)]
        public void TestGetOrderStatusAsStringByIdShipped(int id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatus = "Shipped";
            var status = GetOrderStatusAsStringById(id);
            Assert.True(expectedStatus == status);
        }

        [Test]
        [TestCase(1)]
        public void TestGetPaymentStatusAsStringById(int id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatus = "Paid";
            var status = GetPaymentStatusAsStringById(id);
            Assert.True(expectedStatus == status);
        }

        [Test]
        [TestCase(2)]
        public void TestGetPaymentStatusAsStringByIdNotPaid(int id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatus = "Not Paid";
            var status = GetPaymentStatusAsStringById(id);
            Assert.True(expectedStatus == status);
        }

        [Test]
        public void TestGetPaymentStatuses()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatuses = new List<PaymentStatusViewModel>();
            var paidStatus = new PaymentStatusViewModel()
            {
                Id = (int)PaymentStatus.Paid,
                Name = "Paid",
            };
            var notPaidStatus = new PaymentStatusViewModel()
            {
                Id = (int)PaymentStatus.NotPaid,
                Name = "Not Paid",
            };

            expectedStatuses.Add(paidStatus);
            expectedStatuses.Add(notPaidStatus);

            var statuses = this.orderService.GetPaymentStatuses();
            Assert.AreEqual(expectedStatuses.Count, statuses.Count());

            Assert.IsTrue(statuses.Any(x => x.Id == (int)PaymentStatus.Paid));
            Assert.IsTrue(statuses.Any(x => x.Id == (int)PaymentStatus.NotPaid));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)PaymentStatus.Paid) && x.Name == "Paid"));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)PaymentStatus.NotPaid) && x.Name == "Not Paid"));
        }

        [Test]
        public void TestGetOrderStatuses()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var expectedStatuses = new List<OrderStatusViewModel>();
            var approvedStatus = new OrderStatusViewModel()
            {
                Id = (int)OrderStatus.Approved,
                Name = "Approved",
            };

            var declinedStatus = new OrderStatusViewModel()
            {
                Id = (int)OrderStatus.Declined,
                Name = "Declined",
            };

            var pendingStatus = new OrderStatusViewModel()
            {
                Id = (int)OrderStatus.Pending,
                Name = "Pending",
            };

            var shippedStatus = new OrderStatusViewModel()
            {
                Id = (int)OrderStatus.Shipped,
                Name = "Pending",
            };

            expectedStatuses.Add(approvedStatus);
            expectedStatuses.Add(declinedStatus);
            expectedStatuses.Add(pendingStatus);
            expectedStatuses.Add(shippedStatus);

            var statuses = this.orderService.GetOrderStatuses();

            Assert.AreEqual(expectedStatuses.Count, statuses.Count());

            Assert.IsTrue(statuses.Any(x => x.Id == (int)OrderStatus.Approved));
            Assert.IsTrue(statuses.Any(x => x.Id == (int)OrderStatus.Declined));
            Assert.IsTrue(statuses.Any(x => x.Id == (int)OrderStatus.Pending));
            Assert.IsTrue(statuses.Any(x => x.Id == (int)OrderStatus.Shipped));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)OrderStatus.Approved) && x.Name == "Approved"));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)OrderStatus.Declined) && x.Name == "Declined"));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)OrderStatus.Pending) && x.Name == "Pending"));
            Assert.IsTrue(statuses.Any(x => (x.Id == (int)OrderStatus.Shipped) && x.Name == "Shipped"));
        }

        [Test]
        [TestCase("123456")]
        public async Task TestGetViewModelForEditById(string id)
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var order = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            var model = await this.orderService.GetViewModelForEditByIdAsync(id);

            Assert.True(order.Id == model.OrderId);
            Assert.True((int)order.OrderStatus == model.OrderStatusId);
            Assert.True((int)order.PaymentStatus == model.PaymentStatusId);
        }

        [Test]
        public async Task TestExistsOrderByIdIsFalse()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var orderId = "123456";

            var isExists = await orderService.ExistsById(orderId);

            Assert.False(isExists);
        }

        [Test]
        public async Task TestExistsCartItemByIdIsTrue()
        {
            repo = new Repository(dbContext);
            orderService = new OrderService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var billingDetails = new BillingDetails
            {
                Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
            };

            await repo.AddAsync(billingDetails);
            await repo.SaveChangesAsync();

            var order = new Order
            {
                Id = "123456",
                OrderDate = new DateTime(2022, 12, 10),
                OrderStatus = Data.Models.Enums.OrderStatus.Pending,
                PaymentStatus = Data.Models.Enums.PaymentStatus.NotPaid,
                CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            var isExists = await orderService.ExistsById(order.Id);

            Assert.True(isExists);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}