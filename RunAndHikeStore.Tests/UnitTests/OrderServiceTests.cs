using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Order;
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

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}