using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Models.Enums;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Customer;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class CustomerServiceTests
    {
        private IRepository repo;
        private ICustomerService customerService;
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
        public async Task TestAddBillingDetails()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var expectedBillingDetails = new BillingDetailsFormViewModel()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
            };

            await customerService.AddBillingDetails(expectedBillingDetails, user.Id);

            var dbBillingDetails = await this.repo.All<BillingDetails>().FirstOrDefaultAsync();

            Assert.AreEqual(expectedBillingDetails.StreetAddress, dbBillingDetails.StreetAddress);
            Assert.AreEqual(expectedBillingDetails.City, dbBillingDetails.City);
            Assert.AreEqual(expectedBillingDetails.Country, dbBillingDetails.Country);
            Assert.AreEqual(expectedBillingDetails.PostalCode, dbBillingDetails.PostalCode);
            Assert.AreEqual(expectedBillingDetails.PhoneNumber, dbBillingDetails.PhoneNumber);
        }

        [Test]
        public async Task TestEditBillingDetails()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var initialBillingDetails = new BillingDetailsFormViewModel()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
            };

            await customerService.AddBillingDetails(initialBillingDetails, user.Id);
            var billingDetails = await this.repo.All<BillingDetails>().FirstOrDefaultAsync();

            var expectedBillingDetails = new EditBillingDetailsViewModel()
            {
                Id = billingDetails.Id,
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                StreetAddress = "Test Street Address",
                City = "Test",
                Country = "Test",
                PostalCode = "Test",
                PhoneNumber = "Test",
            };

            await this.customerService.EditBillingDetails(expectedBillingDetails);

            var dbBillingDetails = await this.repo.All<BillingDetails>().FirstOrDefaultAsync();

            Assert.AreEqual(expectedBillingDetails.StreetAddress, dbBillingDetails.StreetAddress);
            Assert.AreEqual(expectedBillingDetails.City, dbBillingDetails.City);
            Assert.AreEqual(expectedBillingDetails.Country, dbBillingDetails.Country);
            Assert.AreEqual(expectedBillingDetails.PostalCode, dbBillingDetails.PostalCode);
            Assert.AreEqual(expectedBillingDetails.PhoneNumber, dbBillingDetails.PhoneNumber);
        }

        [Test]
        public async Task TestAddDeliveryAddress()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var expectedAddress = new AddressViewModel()
            {
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
            };

            await customerService.AddDeliveryAddress(expectedAddress, user.Id);

            var dbAddress = await this.repo.All<Address>().FirstOrDefaultAsync();

            Assert.AreEqual(expectedAddress.StreetAddress, dbAddress.StreetAddress);
            Assert.AreEqual(expectedAddress.City, dbAddress.City);
            Assert.AreEqual(expectedAddress.Country, dbAddress.Country);
            Assert.AreEqual(expectedAddress.PostalCode, dbAddress.PostalCode);
        }

        [Test]
        public async Task TestEditDeliveryAddress()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var initialAddress = new AddressViewModel()
            {
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
            };

            await customerService.AddDeliveryAddress(initialAddress, user.Id);

            var initialDbAddress = await this.repo.All<Address>().FirstOrDefaultAsync();

            var expectedAddress = new EditAddressViewModel()
            {
                Id = initialDbAddress.Id,
                StreetAddress = "Test Street Address",
                City = "Test City",
                Country = "Test Country",
                PostalCode = "Test Postal Code",
            };

            await customerService.EditDeliveryAddress(expectedAddress);

            var dbAddress = await this.repo.All<Address>().FirstOrDefaultAsync();

            Assert.AreEqual(expectedAddress.StreetAddress, dbAddress.StreetAddress);
            Assert.AreEqual(expectedAddress.City, dbAddress.City);
            Assert.AreEqual(expectedAddress.Country, dbAddress.Country);
            Assert.AreEqual(expectedAddress.PostalCode, dbAddress.PostalCode);
        }

        [Test]
        public async Task TestGetCustomerBillingDetailsByUserId()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var initialBillingDetails = new BillingDetailsFormViewModel()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
            };

            await customerService.AddBillingDetails(initialBillingDetails, user.Id);
            var billingDetails = await this.repo.All<BillingDetails>().FirstOrDefaultAsync();

            var model = await this.customerService.GetCustomerBillingDetailsByUserId(user.Id);

            Assert.AreEqual(billingDetails.Id, model.Id);
            Assert.AreEqual(billingDetails.StreetAddress, model.StreetAddress);
            Assert.AreEqual(billingDetails.City, model.City);
            Assert.AreEqual(billingDetails.Country, model.Country);
            Assert.AreEqual(billingDetails.PostalCode, model.PostalCode);
            Assert.AreEqual(billingDetails.PhoneNumber, model.PhoneNumber);
        }


        [Test]
        public async Task TestGetCustomerDeliveryAddressByUserId()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var initialAddress = new AddressViewModel()
            {
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
            };

            await customerService.AddDeliveryAddress(initialAddress, user.Id);

            var expectedAddress = await this.repo.All<Address>().FirstOrDefaultAsync();

            var model = await this.customerService.GetCustomerDeliveryAddressByUserId(user.Id);

            Assert.AreEqual(expectedAddress.Id, model.Id);
            Assert.AreEqual(expectedAddress.StreetAddress, model.StreetAddress);
            Assert.AreEqual(expectedAddress.City, model.City);
            Assert.AreEqual(expectedAddress.Country, model.Country);
            Assert.AreEqual(expectedAddress.PostalCode, model.PostalCode);
        }

        [Test]
        public async Task TestIsCustomerHavingBillingDetails()
        {
            repo = new Repository(dbContext);
            customerService = new CustomerService(repo);

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

            var initialBillingDetails = new BillingDetailsFormViewModel()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                StreetAddress = "Osmi Primorski Polk 145",
                City = "Varna",
                Country = "Bulgaria",
                PostalCode = "9000",
                PhoneNumber = "+359899665543",
            };

            await customerService.AddBillingDetails(initialBillingDetails, user.Id);

            var isHavingBillingDetails = await customerService.IsCustomerHavingBillingDetails(user.Id);

            Assert.True(isHavingBillingDetails);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}