using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Size;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class SizeServiceTests
    {
        private IRepository repo;
        private ISizeService sizeService;
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
        public async Task TestAddSize()
        {
            repo = new Repository(dbContext);
            sizeService = new SizeService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            var secondProductType = new ProductType()
            {
                Id = "1345",
                Name = "Product Type 2 Test",
            };

            var productTypes = new List<ProductType>();
            productTypes.Add(productType);
            productTypes.Add(secondProductType);

            await repo.AddRangeAsync(productTypes);
            await repo.SaveChangesAsync();

            var expectedSize = new AddSizeViewModel()
            {
                Name = "Size Test Name",
                ProductTypeId = "1345",
                ProductType = "Product Type 2 Test"
            };

            await sizeService.Add(expectedSize);

            var dbSizes = await sizeService.GetAllSizes();
            var dbSize = dbSizes.FirstOrDefault();

            Assert.AreEqual(expectedSize.Name, dbSize.Name);
            Assert.AreEqual(expectedSize.ProductTypeId, dbSize.ProductTypeId);
        }

        [Test]
        public async Task TestEditSize()
        {
            repo = new Repository(dbContext);
            sizeService = new SizeService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            var secondProductType = new ProductType()
            {
                Id = "1345",
                Name = "Product Type 2 Test",
            };

            var productTypes = new List<ProductType>();
            productTypes.Add(productType);
            productTypes.Add(secondProductType);

            await repo.AddRangeAsync(productTypes);
            await repo.SaveChangesAsync();

            await repo.AddAsync(new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            });

            await repo.SaveChangesAsync();

            await sizeService.Edit(new SizeViewModel()
            {
                Id = "1",
                Name = "Test Size Name is editted",
            });

            var dbSize = await repo.GetByIdAsync<Size>("1");

            Assert.That(dbSize.Name, Is.EqualTo("Test Size Name is editted"));
            Assert.That(dbSize.ProductTypeId, Is.EqualTo("12345"));
        }

        [Test]
        public async Task TestDeleteSize()
        {
            repo = new Repository(dbContext);
            sizeService = new SizeService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            var secondProductType = new ProductType()
            {
                Id = "1345",
                Name = "Product Type 2 Test",
            };

            var productTypes = new List<ProductType>();
            productTypes.Add(productType);
            productTypes.Add(secondProductType);

            await repo.AddRangeAsync(productTypes);
            await repo.SaveChangesAsync();

            var expectedSize = new Size()
            {
                Id = "1",
                Name = "Size Test Name",
                ProductTypeId = "1345",
            };

            await repo.AddAsync(expectedSize);
            await repo.SaveChangesAsync();

            await sizeService.Delete(expectedSize.Id);

            var dbSizes = await this.sizeService.GetAllSizes();
            var isActive = dbSizes.Contains(expectedSize);

            Assert.AreEqual(false, isActive);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}