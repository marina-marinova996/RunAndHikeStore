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
    public class ShoppingCartServiceTests
    {
        private IRepository repo;
        private IShoppingCartService cartService;
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

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}