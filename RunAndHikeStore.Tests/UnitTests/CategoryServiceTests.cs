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
        public async Task TestGetAllCategories()
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

        [Test]
        [TestCase("Walking", 1, 6)]
        [TestCase("Running", 1, 6)]
        [TestCase("Walk", 1, 6)]
        [TestCase("Run", 1, 6)]
        public async Task TestGetAllAsync(string searchTerm, int currentPage = 1, int brandsPerPage = 6)
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var categories = new List<Category>();

            var firstCategory = new Category()
            {
                Name = "Walking",
            };

            categories.Add(firstCategory);

            var secondCategory = new Category()
            {
                Name = "Running",
            };

            categories.Add(secondCategory);

            await repo.AddRangeAsync(categories);
            await repo.SaveChangesAsync();

            var allCategoriesViewModel = await this.categoryService.GetAllAsync(searchTerm, currentPage, brandsPerPage);

            var IsContaining = allCategoriesViewModel.Categories.Any(x => x.Name.Contains(searchTerm));

            Assert.That(allCategoriesViewModel.Categories.Count(), Is.EqualTo(1));
            Assert.That(allCategoriesViewModel.Categories.Count(), Is.EqualTo(1));
            Assert.That(allCategoriesViewModel.TotalRecordsCount, Is.EqualTo(1));
            Assert.True(IsContaining);
        }

        [Test]
        public async Task TestExistsCategoryByIdIsFalse()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var categoryId = "123456";

            var isExists = await categoryService.ExistsById(categoryId);

            Assert.False(isExists);
        }

        [Test]
        public async Task TestExistsCategoryByIdIsTrue()
        {
            repo = new Repository(dbContext);
            categoryService = new CategoryService(repo);

            var expectedCategory = new Category()
            {
                Id = "123",
                Name = "Test Name",
            };

            await repo.AddAsync(expectedCategory);
            await repo.SaveChangesAsync();

            var isExists = await categoryService.ExistsById(expectedCategory.Id);

            Assert.True(isExists);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}