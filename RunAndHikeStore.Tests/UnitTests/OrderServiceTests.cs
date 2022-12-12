using Microsoft.Extensions.DependencyInjection;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Services;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;

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

        //[Test]
        //public async Task TestDeleteOrder()
        //{
        //    repo = new Repository(dbContext);
        //    orderService = new OrderService(repo);
        //}

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}