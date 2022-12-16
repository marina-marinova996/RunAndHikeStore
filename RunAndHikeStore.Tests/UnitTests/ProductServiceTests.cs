using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Models.Enums;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Product;
using RunAndHikeStore.Web.ViewModels.Product.Enum;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IRepository repo;
        private IProductService productService;
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
        public async Task TestEditProduct()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.AddAsync(new Product()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "12456789",
                ImageUrl = "",
                UnitPrice = 150,
                Description = "This product is not edited.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
            });

            await repo.SaveChangesAsync();

            await productService.Edit(new EditProductViewModel()
            {
                Id = "1",
                Name = "Test Name",
                ProductNumber = "12456789",
                ImageUrl = "",
                UnitPrice = 150,
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Description = "This product is edited",
            });

            var dbProduct = await repo.GetByIdAsync<Product>("1");

            Assert.That(dbProduct.Description, Is.EqualTo("This product is edited"));
        }

        [Test]
        public async Task TestAddProduct()
        {

            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.AddAsync(new Category()
            {
                Id = "123",
                Name = "Category Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new AddProductViewModel()
            {
                Name = "Test Name",
                ProductNumber = "123450",
                ImageUrl = "123456689970",
                UnitPrice = 150m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                MultiCategoriesIds = new List<string>() { "123" },
                GenderId = 1,
            };

            await productService.Add(expectedProduct);

            var dbProducts = await this.productService.GetAllProducts();
            var dbProduct = dbProducts.FirstOrDefault(x => x.ProductNumber == expectedProduct.ProductNumber);

            Assert.AreEqual(expectedProduct.ProductNumber, dbProduct.ProductNumber);
            Assert.AreEqual(expectedProduct.Name, dbProduct.Name);
            Assert.AreEqual(expectedProduct.Description, dbProduct.Description);
            Assert.AreEqual(expectedProduct.Color, dbProduct.Color);
            Assert.AreEqual(expectedProduct.BrandId, dbProduct.BrandId);
            Assert.AreEqual(expectedProduct.ProductTypeId, dbProduct.ProductTypeId);
        }

        [Test]
        public async Task TestDeleteProduct()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
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

            await repo.AddAsync(expectedProduct);
            await repo.SaveChangesAsync();

            await productService.Delete(expectedProduct.Id);

            var products = await this.productService.GetAllProducts();
            var isActive = products.Contains(expectedProduct);


            Assert.AreEqual(false, isActive);
        }

        [Test]
        public async Task TestGetByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
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

            await repo.AddAsync(expectedProduct);
            await repo.SaveChangesAsync();

            var productModel = await productService.GetByIdAsync(expectedProduct.Id);


            Assert.AreEqual(expectedProduct.Name, productModel.Name);
            Assert.AreEqual(expectedProduct.ProductNumber, productModel.ProductNumber);
            Assert.AreEqual(expectedProduct.ImageUrl, productModel.ImageUrl);
            Assert.AreEqual(expectedProduct.UnitPrice, productModel.UnitPrice);
            Assert.AreEqual(expectedProduct.Description, productModel.Description);
            Assert.AreEqual(expectedProduct.Color, productModel.Color);
            Assert.AreEqual(expectedProduct.BrandId, productModel.BrandId);
            Assert.AreEqual(expectedProduct.ProductTypeId, productModel.ProductTypeId);
        }

        [Test]
        public async Task TestGetViewModelForEditByIdAsyncMethod()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
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

            await repo.AddAsync(expectedProduct);
            await repo.SaveChangesAsync();

            var productModel = await productService.GetViewModelForEditByIdAsync(expectedProduct.Id);


            Assert.AreEqual(expectedProduct.Id, productModel.Id);
            Assert.AreEqual(expectedProduct.Name, productModel.Name);
            Assert.AreEqual(expectedProduct.ProductNumber, productModel.ProductNumber);
            Assert.AreEqual(expectedProduct.ImageUrl, productModel.ImageUrl);
            Assert.AreEqual(expectedProduct.UnitPrice, productModel.UnitPrice);
            Assert.AreEqual(expectedProduct.Description, productModel.Description);
            Assert.AreEqual(expectedProduct.Color, productModel.Color);
            Assert.AreEqual(expectedProduct.BrandId, productModel.BrandId);
            Assert.AreEqual(expectedProduct.ProductTypeId, productModel.ProductTypeId);
        }

        [Test]
        public async Task TestGetCategoriesAsync()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            var category = new Category()
            {
                Id = "12345",
                Name = "Category Test"
            };

            var secondCategory = new Category()
            {
                Id = "1345",
                Name = "Category 2 Test"
            };

            var categories = new List<Category>();
            categories.Add(category);
            categories.Add(secondCategory);

            await repo.AddRangeAsync(categories);
            await repo.SaveChangesAsync();

            var dbCategories = await this.productService.GetCategoriesAsync();

            Assert.AreEqual(categories.Count, dbCategories.Count());
        }

        [Test]
        public async Task TestGetBrandsAsync()
        {
            var repo = new Repository(dbContext);
            productService = new ProductService(repo);

            var brand = new Brand()
            {
                Id = "12345",
                Name = "Brand Test"
            };

            var secondBrand = new Brand()
            {
                Id = "1345",
                Name = "Brand 2 Test"
            };

            var brands = new List<Brand>();
            brands.Add(brand);
            brands.Add(secondBrand);

            await repo.AddRangeAsync(brands);
            await repo.SaveChangesAsync();

            var dbBrands = await this.productService.GetBrandsAsync();

            Assert.That(brands.Count, Is.EqualTo(dbBrands.Count()));
        }

        [Test]
        public async Task TestGetProductTypesAsyncCount()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

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

            var dbProductTypes = await this.productService.GetProductTypesAsync();

            Assert.That(productTypes.Count, Is.EqualTo(dbProductTypes.Count()));
        }

        [Test]
        public async Task TestGetSizesAsync()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

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

            var size = new Size()
            {
                Id = "12345",
                Name = "Size Test",
                ProductTypeId = "1345",
            };

            var secondSize = new Size()
            {
                Id = "1345",
                Name = "Size 2 Test",
                ProductTypeId = "1345",
            };

            var thirdSize = new Size()
            {
                Id = "1345678",
                Name = "Size 3 Test",
                ProductTypeId = "12345",
            };


            var sizes = new List<Size>();
            sizes.Add(size);
            sizes.Add(secondSize);
            sizes.Add(thirdSize);

            await repo.AddRangeAsync(sizes);
            await repo.SaveChangesAsync();

            var dbSizes = await this.productService.GetSizesAsync();

            Assert.That(sizes.Count, Is.EqualTo(dbSizes.Count()));
        }

        [Test]
        public async Task TestGetAllProducts()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            var products = new List<Product>();

            var expectedProduct = new Product()
            {
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

            products.Add(expectedProduct);

            var secondExpectedProduct = new Product()
            {
                Name = "Test Name 2",
                ProductNumber = "123456789",
                ImageUrl = "1234566890",
                UnitPrice = 140m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            products.Add(secondExpectedProduct);

            await repo.AddRangeAsync(products);
            await repo.SaveChangesAsync();

            var dbProducts = await this.productService.GetAllProducts();

            Assert.That(products.Count, Is.EqualTo(dbProducts.Count()));
        }

        [Test]
        public async Task TestGetProductTypesAsync()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

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

            var dbProductTypes = await this.productService.GetProductTypesAsync();

            Assert.True(dbProductTypes.Any(x => x.Id == productType.Id && x.Name == productType.Name));
            Assert.True(dbProductTypes.Any(x => x.Id == secondProductType.Id && x.Name == secondProductType.Name));
        }

        [Test]
        public async Task TestGetGenders()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            List<GenderViewModel> genders = new List<GenderViewModel>();

            genders.Add(new GenderViewModel
            {
                Id = (int)Gender.Male,
                Name = "Male",
            });

            genders.Add(new GenderViewModel
            {
                Id = (int)Gender.Female,
                Name = "Female",
            });

            genders.Add(new GenderViewModel
            {
                Id = (int)Gender.Unisex,
                Name = "Unisex",
            });

            var actualGenders = this.productService.GetGenders();

            Assert.AreEqual(genders.Count(), actualGenders.Count());
            Assert.True(actualGenders.Any(x => x.Id == (int)Gender.Male && x.Name == "Male"));
            Assert.True(actualGenders.Any(x => x.Id == (int)Gender.Female && x.Name == "Female"));
            Assert.True(actualGenders.Any(x => x.Id == (int)Gender.Unisex && x.Name == "Unisex"));
        }

        [Test]
        [TestCase("", ProductSorting.Newest, 1, 6)]
        public async Task TestGetManageAllSorted(string searchTerm, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6)
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
            {
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

            var secondExpectedProduct = new Product()
            {
                Name = "Test Name 2",
                ProductNumber = "123456789",
                ImageUrl = "1234566890",
                UnitPrice = 140m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)1,
            };

            await repo.AddAsync(expectedProduct);
            await repo.SaveChangesAsync();
            await repo.AddAsync(secondExpectedProduct);
            await repo.SaveChangesAsync();

            var dbProducts = await this.productService.GetManageAllSorted(searchTerm, sorting, currentPage, productsPerPage);
            var newestProduct = dbProducts.Products.FirstOrDefault();

            Assert.That(newestProduct.Name, Is.EqualTo(secondExpectedProduct.Name));
            Assert.That(newestProduct.ProductNumber, Is.EqualTo(secondExpectedProduct.ProductNumber));
            Assert.That(newestProduct.ImageUrl, Is.EqualTo(secondExpectedProduct.ImageUrl));
            Assert.That(newestProduct.Color, Is.EqualTo(secondExpectedProduct.Color));
            Assert.That(newestProduct.BrandId, Is.EqualTo(secondExpectedProduct.BrandId));
            Assert.That(newestProduct.ProductTypeId, Is.EqualTo(secondExpectedProduct.ProductTypeId));
        }

        [Test]
        [TestCase("1", new []{"134"}, "12345", new[] { "1345" }, new[] { "1" }, "", ProductSorting.Newest, 1, 6)]
        public async Task TestGetAllSorted(string genderId, IEnumerable<string> multiCategoriesIds, string productTypeId, IEnumerable<string> multiBrandsIds, IEnumerable<string> multiSizesIds, string searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6)
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            var products = new List<Product>();

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.AddAsync(new Category()
            {
                Id = "134",
                Name = "Category Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
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

            products.Add(expectedProduct);

            var secondExpectedProduct = new Product()
            {
                Id = "2",
                Name = "Test Name 2",
                ProductNumber = "123456789",
                ImageUrl = "1234566890",
                UnitPrice = 140m,
                Description = "This product is added.",
                Color = "Red",
                BrandId = "1345",
                ProductTypeId = "12345",
                Gender = (Gender)2,
            };

            products.Add(secondExpectedProduct);

            await repo.AddRangeAsync(products);
            await repo.SaveChangesAsync();

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);


            var categoryProduct = new CategoryProduct()
            {
                CategoryId = "134",
                ProductId = "1",
            };
            await repo.AddAsync(categoryProduct);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();


            var dbProducts = await this.productService.GetAllSorted(genderId, multiCategoriesIds, productTypeId, multiBrandsIds, multiSizesIds, searchTerm, ProductSorting.Newest, currentPage, productsPerPage);

            Assert.That(dbProducts.TotalProductsCount == 1);
        }

        [Test]
        public async Task TestExistsProductByIdIsFalse()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            var productId = "123456";

            var isExists = await productService.ExistsById(productId);

            Assert.False(isExists);
        }

        [Test]
        public async Task TestExistsProductByIdIsTrue()
        {
            repo = new Repository(dbContext);
            productService = new ProductService(repo);

            await repo.AddAsync(new ProductType()
            {
                Id = "12345",
                Name = "Product Type Test"
            });

            await repo.AddAsync(new Brand()
            {
                Id = "1345",
                Name = "Brand Test"
            });

            await repo.SaveChangesAsync();

            var expectedProduct = new Product()
            {
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

            await this.repo.AddAsync(expectedProduct);
            await this.repo.SaveChangesAsync();

            var isExists = await productService.ExistsById(expectedProduct.Id);

            Assert.True(isExists);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
