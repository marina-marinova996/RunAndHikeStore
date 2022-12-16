using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Models;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Models.Enums;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;
using System.Net.WebSockets;

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

        [Test]
        [TestCase("1", "6e736140-d201-4e92-afe8-d52895ec1bc2", "1", 1)]
        public async Task TestAddToCart(string productId, string userId, string sizeId, int quantity)
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart(productId, userId, sizeId, quantity);
            var isAdded = this.repo.All<CartItem>().Any(x => x.ProductId == productId && x.SizeId == sizeId && x.Quantity == quantity);
            var cartItemsCount = this.repo.All<CartItem>().Where(c => c.ShoppingCart.ApplicationUser.Id == userId).Count();

            Assert.IsTrue(isAdded);
            Assert.AreEqual(1, cartItemsCount);
        }

        [Test]
        [TestCase("6e736140-d201-4e92-afe8-d52895ec1bc2")]
        public async Task TestRemoveAllCartItems(string userId)
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart("1", "6e736140-d201-4e92-afe8-d52895ec1bc2", "1", 1);
            await cartService.RemoveAllCartItems(userId);

            var countCartItems = this.repo.All<CartItem>().Where(c => c.ShoppingCart.ApplicationUser.Id == userId).Count();

            Assert.AreEqual(0, countCartItems);
        }

        [Test]
        public async Task TestRemoveCartItem()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart("1", "6e736140-d201-4e92-afe8-d52895ec1bc2", "1", 1);

            var cartItem = await this.repo.All<CartItem>().Where(x => x.ShoppingCart.ApplicationUser.Id == "6e736140-d201-4e92-afe8-d52895ec1bc2").FirstOrDefaultAsync();
            await cartService.RemoveCartItem(cartItem.Id);

            var countCartItems = this.repo.All<CartItem>().Where(c => c.ShoppingCart.ApplicationUser.Id == "6e736140-d201-4e92-afe8-d52895ec1bc2").Count();
            var isActive = this.repo.All<CartItem>().Any(c => c.Id == cartItem.Id);

            Assert.AreEqual(0, countCartItems);
            Assert.AreEqual(false, isActive);
        }

        [Test]
        [TestCase("1", "6e736140-d201-4e92-afe8-d52895ec1bc2", "1", 1)]
        public async Task TestCreateCartItem(string productId, string userId, string sizeId, int quantity)
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            var cartItem = await this.cartService.CreateCartItem(productId, userId, sizeId, quantity);

            Assert.IsTrue(cartItem != null);
            Assert.AreEqual(productId, cartItem.ProductId);
            Assert.AreEqual(sizeId, cartItem.SizeId);
            Assert.AreEqual(quantity, cartItem.Quantity);
        }


        [Test]
        public async Task TestIsInStock()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 12,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            var isInStock = await this.cartService.IsInStock(expectedProduct.Id, size.Id);

            Assert.True(isInStock);
        }

        [Test]
        public async Task TestIsInStockFalse()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 0,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            var isInStock = await this.cartService.IsInStock(expectedProduct.Id, size.Id);

            Assert.False(isInStock);
        }

        [Test]
        public async Task TestIsInStockFalseWhenThereIsOneCartItemAdded()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 1,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart(expectedProduct.Id, user.Id, size.Id, 1);
            var isInStock = await this.cartService.IsInStock(expectedProduct.Id, size.Id);

            Assert.False(isInStock);
        }

        [Test]
        public async Task TestIsInStockFalseWhenThereIsMoreThanOneCartItemAdded()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 2,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart(expectedProduct.Id, user.Id, size.Id, 1);
            await cartService.AddToCart(expectedProduct.Id, user.Id, size.Id, 1);

            var isInStock = await this.cartService.IsInStock(expectedProduct.Id, size.Id);

            Assert.False(isInStock);
        }


        [Test]
        [TestCase("1", "6e736140-d201-4e92-afe8-d52895ec1bc2", "1", 1)]
        public async Task TestAddToCartThrowsArgumentException(string productId, string userId, string sizeId, int quantity)
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 2,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart(productId, userId, sizeId, quantity);
            await cartService.AddToCart(productId, userId, sizeId, quantity);

            var cartItems = await this.repo.AsNoTracking<CartItem>().Where(c => c.ProductId == productId && c.SizeId == sizeId && c.ShoppingCart.ApplicationUser.Id == userId).ToListAsync();
            var quantityInCartItems = cartItems.Select(x => x.Quantity).ToList().Sum();

            Assert.That(async () => await cartService.AddToCart(productId, userId, sizeId, quantity),
                    Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task TestExistsCartItemById()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

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

            var size = new Size()
            {
                Id = "1",
                Name = "Test Name",
                ProductTypeId = "12345",
            };
            await repo.AddAsync(size);

            var stock = new ProductSize()
            {
                SizeId = "1",
                ProductId = "1",
                UnitsInStock = 2,
            };

            await repo.AddAsync(stock);
            await repo.SaveChangesAsync();

            await cartService.AddToCart(expectedProduct.Id, user.Id, size.Id, 1);

            var cartItem = await this.repo.All<CartItem>().FirstOrDefaultAsync();

            var isExistsCartItem = await cartService.ExistsCartItemById(cartItem.Id);

            Assert.True(isExistsCartItem);

        }

        [Test]
        public async Task TestExistsCartItemByIdIsFalse()
        {
            repo = new Repository(dbContext);
            cartService = new ShoppingCartService(repo);

            var cartItemId = "123456";

            var isExistsCartItem = await cartService.ExistsCartItemById(cartItemId);

            Assert.False(isExistsCartItem);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}