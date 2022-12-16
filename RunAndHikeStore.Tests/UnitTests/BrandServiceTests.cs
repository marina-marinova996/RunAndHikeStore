using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Brand;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class BrandServiceTests
    {
        private IRepository repo;
        private IBrandService brandService;
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
        public async Task TestAddBrand()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var expectedBrand = new AddBrandViewModel()
            {
                Name = "Brand Test"
            };

            await brandService.Add(expectedBrand);

            var dbBrands = await brandService.GetAllBrands();
            var dbBrand = dbBrands.FirstOrDefault();

            Assert.AreEqual(expectedBrand.Name, dbBrand.Name);
        }


        [Test]
        public async Task TestEditBrand()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            await repo.AddAsync(new Brand()
            {
                Id = "1",
                Name = "Test Name",
            });

            await repo.SaveChangesAsync();

            await brandService.Edit(new EditBrandViewModel()
            {
                Id = "1",
                Name = "Test Brand Name is editted",
            });

            var dbBrand = await repo.GetByIdAsync<Brand>("1");

            Assert.That(dbBrand.Name, Is.EqualTo("Test Brand Name is editted"));
        }

        [Test]
        public async Task TestDeleteBrand()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var expectedBrand = new Brand()
            {
                Id = "1",
                Name = "Test Name",
            };

            await repo.AddAsync(expectedBrand);
            await repo.SaveChangesAsync();


            await brandService.Delete(expectedBrand.Id);

            var dbBrands = await this.brandService.GetAllBrands();
            var isActive = dbBrands.Contains(expectedBrand);

            Assert.AreEqual(false, isActive);
        }
        [Test]
        public async Task TestGetByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var expectedBrand = new Brand()
            {
                Id = "1",
                Name = "Test Name",
            };

            await repo.AddAsync(expectedBrand);
            await repo.SaveChangesAsync();

            var brandModel = await brandService.GetByIdAsync(expectedBrand.Id);


            Assert.AreEqual(expectedBrand.Name, brandModel.Name);
        }

        [Test]
        public async Task TestGetViewModelForEditByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var expectedBrand = new Brand()
            {
                Id = "1",
                Name = "Test Name",
            };

            await repo.AddAsync(expectedBrand);
            await repo.SaveChangesAsync();

            var brandModel = await brandService.GetViewModelForEditByIdAsync(expectedBrand.Id);


            Assert.AreEqual(expectedBrand.Name, brandModel.Name);
            Assert.AreEqual(expectedBrand.Id, brandModel.Id);
        }

        [Test]
        public async Task TestGetAllBrands()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var brands = new List<Brand>();

            var expectedBrand = new Brand()
            {
                Name = "Test Name",
            };

            brands.Add(expectedBrand);

            var secondExpectedBrand = new Brand()
            {
                Name = "Test Name 2",
            };

            brands.Add(secondExpectedBrand);

            await repo.AddRangeAsync(brands);
            await repo.SaveChangesAsync();

            var dbBrands = await this.brandService.GetAllBrands();

            Assert.That(brands.Count, Is.EqualTo(dbBrands.Count()));
        }

        [Test]
        [TestCase("North", 1, 6)]
        [TestCase("Jack", 1, 6)]
        [TestCase("Face", 1, 6)]
        [TestCase("Wolfskin", 1, 6)]
        [TestCase("North Face", 1, 6)]
        [TestCase("Jack Wolfskin", 1, 6)]
        public async Task TestGetAllAsync(string searchTerm, int currentPage = 1, int brandsPerPage = 6)
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var brands = new List<Brand>();

            var firstBrand = new Brand()
            {
                Name = "Jack Wolfskin",
            };

            brands.Add(firstBrand);

            var secondBrand = new Brand()
            {
                Name = "North Face",
            };

            brands.Add(secondBrand);

            await repo.AddRangeAsync(brands);
            await repo.SaveChangesAsync();

            var allBrandsViewModel = await this.brandService.GetAllAsync(searchTerm, currentPage, brandsPerPage);

            var IsContaining = allBrandsViewModel.Brands.Any(x => x.Name.Contains(searchTerm));

            Assert.That(allBrandsViewModel.Brands.Count(), Is.EqualTo(1));
            Assert.That(allBrandsViewModel.TotalRecordsCount, Is.EqualTo(1));
            Assert.True(IsContaining);
        }

        [Test]
        public async Task TestExistsBrandByIdIsFalse()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var brandId = "123456";

            var isExists = await brandService.ExistsById(brandId);

            Assert.False(isExists);
        }

        [Test]
        public async Task TestExistBrandByIdIsTrue()
        {
            repo = new Repository(dbContext);
            brandService = new BrandService(repo);

            var expectedBrand = new Brand()
            {
                Id = "123",
                Name = "Test Name",
            };

            await repo.AddAsync(expectedBrand);
            await repo.SaveChangesAsync();

            var isExists = await brandService.ExistsById(expectedBrand.Id);

            Assert.True(isExists);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}