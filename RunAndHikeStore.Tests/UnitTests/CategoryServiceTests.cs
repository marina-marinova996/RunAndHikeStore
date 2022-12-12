using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Category;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class CategoryServiceTests
    {
        private IRepository repo;
        private ICategoryService categoryService;
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
        public async Task TestAddCategory()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var expectedCategory = new AddCategoryViewModel()
            {
                Name = "Category Test"
            };

            await categoryService.Add(expectedCategory);

            var dbCategories = await categoryService.GetAllCategories();
            var dbCategory = dbCategories.FirstOrDefault();

            Assert.AreEqual(expectedCategory.Name, dbCategory.Name);
        }

        [Test]
        public async Task TestEditCategory()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            await repo.AddAsync(new Category()
            {
                Id = "1",
                Name = "Test Name",
            });

            await repo.SaveChangesAsync();

            await categoryService.Edit("1", new EditCategoryViewModel()
            {
                Name = "Test Category Name is editted",
            });

            var dbCategory = await repo.GetByIdAsync<Category>("1");

            Assert.That(dbCategory.Name, Is.EqualTo("Test Category Name is editted"));
        }

        [Test]
        public async Task TestDeleteCategory()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var expectedCategory = new Category()
            {
                Name = "Category Test"
            };

            await repo.AddAsync(expectedCategory);
            await repo.SaveChangesAsync();

            var dbCategories = await categoryService.GetAllCategories();
            var dbCategory = dbCategories.FirstOrDefault();
            await categoryService.Delete(dbCategory.Id);

            var categories = await this.categoryService.GetAllCategories();
            var isActive = categories.Contains(dbCategory);

            Assert.AreEqual(false, isActive);
        }

        [Test]
        public async Task TestGetByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var expectedCategory = new Category()
            {
                Id = "1",
                Name = "Category Test"
            };

            await repo.AddAsync(expectedCategory);
            await repo.SaveChangesAsync();

            var categoryModel = await categoryService.GetByIdAsync(expectedCategory.Id);

            Assert.AreEqual(expectedCategory.Name, categoryModel.Name);
        }

        [Test]
        public async Task TestGetViewModelForEditByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var expectedCategory = new Category()
            {
                Id = "1",
                Name = "Category Test"
            };

            await repo.AddAsync(expectedCategory);
            await repo.SaveChangesAsync();

            var categoryModel = await categoryService.GetViewModelForEditByIdAsync(expectedCategory.Id);

            Assert.AreEqual(expectedCategory.Name, categoryModel.Name);
            Assert.AreEqual(expectedCategory.Id, categoryModel.Id);
        }

        [Test]
        public async Task TestGetAllProducts()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var categories = new List<Category>();

            var expectedCategory = new Category()
            {
                Name = "Test Name",
            };

            categories.Add(expectedCategory);

            var secondExpectedCategory = new Category()
            {
                Name = "Test Name 2",
            };

            categories.Add(secondExpectedCategory);

            await repo.AddRangeAsync(categories);
            await repo.SaveChangesAsync();

            var dbCategories = await this.categoryService.GetAllCategories();

            Assert.That(categories.Count, Is.EqualTo(dbCategories.Count()));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}