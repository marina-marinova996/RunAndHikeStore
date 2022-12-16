using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Models.Enums;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Stock;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class StockServiceTests
    {
        private IRepository repo;
        private IStockService stockService;
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
        public async Task TestAddStock()
        {
            repo = new Repository(dbContext);
            stockService = new StockService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            var productTypes = new List<ProductType>();
            productTypes.Add(productType);

            await repo.AddRangeAsync(productTypes);

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };

            await repo.AddAsync(size);

            var product = new Product()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "23456789",
                ImageUrl = "123456689970",
                UnitPrice = 150m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            await repo.AddAsync(product);
            await repo.SaveChangesAsync();

            var expectedStock = new AddStockViewModel()
            {
                SizeId = size.Id,
                ProductId = product.Id,
                UnitsInStock = 12,
            };

            await stockService.AddStock(expectedStock);

            var dbStocks = await stockService.GetAllStocks();
            var dbStock = dbStocks.FirstOrDefault();

            Assert.AreEqual(expectedStock.SizeId, dbStock.SizeId);
            Assert.AreEqual(expectedStock.ProductId, dbStock.ProductId);
            Assert.AreEqual(expectedStock.UnitsInStock, dbStock.UnitsInStock);
        }

        [Test]
        public async Task TestEditStock()
        {
            repo = new Repository(dbContext);
            stockService = new StockService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            await repo.AddAsync(productType);

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var product = new Product()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "23456789",
                ImageUrl = "123456689970",
                UnitPrice = 150m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            await repo.AddAsync(product);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            var stockForEdit  = new EditStockViewModel()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 15,
            };

            await stockService.EditStock(stockForEdit);

            var dbStocks = await this.stockService.GetAllStocks();
            var dbStock = dbStocks.FirstOrDefault();

            var isEditted = dbStocks.Contains(dbStock);

            Assert.AreEqual(stockForEdit.SizeId, dbStock.SizeId);
            Assert.AreEqual(stockForEdit.ProductId, dbStock.ProductId);
            Assert.AreEqual(stockForEdit.UnitsInStock, dbStock.UnitsInStock);
            Assert.AreEqual(true, isEditted);
        }

        [Test]
        public async Task TestDeleteStock()
        {
            repo = new Repository(dbContext);
            stockService = new StockService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            await repo.AddAsync(productType);

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var product = new Product()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "23456789",
                ImageUrl = "123456689970",
                UnitPrice = 150m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            await repo.AddAsync(product);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await stockService.DeleteStock(stock.ProductId, stock.SizeId);

            var dbStocks = await this.stockService.GetAllStocks();
            var dbStock = dbStocks.FirstOrDefault();
            var isDeleted = dbStock.IsDeleted;

            Assert.AreEqual(true, isDeleted);
        }

        [Test]
        public async Task TestExistsStockByProductIdIsFalse()
        {
            repo = new Repository(dbContext);
            stockService = new StockService(repo);

            var productId = "123455";
            var sizeId = "123456";

            var isExists = await stockService.ExistsById(productId,sizeId);

            Assert.False(isExists);
        }

        [Test]
        public async Task TestExistsSizeByIdIsTrue()
        {
            repo = new Repository(dbContext);
            stockService = new StockService(repo);

            var productType = new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test",
            };

            await repo.AddAsync(productType);

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var product = new Product()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "23456789",
                ImageUrl = "123456689970",
                UnitPrice = 150m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            await repo.AddAsync(product);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            var isExists = await stockService.ExistsById(product.Id,size.Id);

            Assert.True(isExists);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}