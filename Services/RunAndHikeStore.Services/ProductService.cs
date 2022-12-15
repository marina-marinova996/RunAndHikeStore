namespace RunAndHikeStore.Services
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.Product.Enum;
    using RunAndHikeStore.Web.ViewModels.Size;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public ProductService(
            IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="model">Product model.</param>
        /// <returns></returns>
        public async Task Add(AddProductViewModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                ProductNumber = model.ProductNumber,
                ProductTypeId = model.ProductTypeId,
                Color = model.Color,
                BrandId = model.BrandId,
                Description = model.Description,
                Gender = (Gender)model.GenderId,
                UnitPrice = model.UnitPrice,
                ImageUrl = model.ImageUrl,
            };

            if (model.MultiCategoriesIds.Any())
            {
                foreach (var categoryId in model.MultiCategoriesIds)
                {
                    var productCategory = new CategoryProduct
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                    };
                    product.Categories.Add(productCategory);
                }
            }

            await this.repo.AddAsync(product);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="model">Product model.</param>
        /// <returns></returns>
        public async Task Edit(EditProductViewModel model)
        {
            var product = await this.repo.All<Product>(x => x.Id == model.Id)
                                         .Where(p => p.IsDeleted == false)
                                         .Include(p => p.Categories)
                                         .FirstOrDefaultAsync();

            if (product != null && model != null)
            {
                product.ProductNumber = model.ProductNumber;
                product.ProductTypeId = model.ProductTypeId;
                product.BrandId = model.BrandId;
                product.Name = model.Name;
                product.Description = model.Description;
                product.Color = model.Color;
                product.ImageUrl = model.ImageUrl;
                product.Gender = (Gender)model.GenderId;
                product.UnitPrice = model.UnitPrice;

                if (model.MultiCategoriesIds != null)
                {
                    foreach (var category in product.Categories)
                    {
                        if (!model.MultiCategoriesIds.Contains(category.CategoryId))
                        {
                            product.Categories.Remove(category);
                        }
                    }

                    foreach (var categoryId in model.MultiCategoriesIds)
                    {
                        if (!product.Categories.Any(x => x.CategoryId == categoryId))
                        {
                            var productCategory = new CategoryProduct
                            {
                                ProductId = product.Id,
                                CategoryId = categoryId,
                            };
                            product.Categories.Add(productCategory);
                        }
                    }
                }

                await this.repo.SaveChangesAsync();
            }
        }

        public async Task Delete(string id)
        {
            var product = await this.repo.All<Product>()
                                         .Where(p => p.IsDeleted == false)
                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                product.IsDeleted = true;
                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetByIdAsync(string id)
        {
            var product = this.repo.AsNoTracking<Product>(p => p.Id == id);

            if (product == null)
            {
                throw new NullReferenceException();
            }

            return await this.repo
                        .AsNoTracking<Product>(p => p.Id == id)
                        .Where(p => p.IsDeleted == false)
                        .Include(p => p.Brand)
                        .Where(p => p.Brand.IsDeleted == false)
                        .Include(p => p.Sizes)
                        .Include(p => p.ProductType)
                        .Where(p => p.ProductType.IsDeleted == false)
                        .Include(p => p.Categories)
                        .Select(p => new ProductViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            ImageUrl = p.ImageUrl,
                            UnitPrice = p.UnitPrice,
                            Description = p.Description,
                            Color = p.Color,
                            Brand = p.Brand.Name,
                            ProductType = p.ProductType.Name,
                            BrandId = p.BrandId,
                            ProductTypeId = p.ProductTypeId,
                            GenderId = (int)p.Gender,
                            Gender = GetGenderAsStringById((int)p.Gender),
                            Sizes = p.Sizes
                                        .Where(ps => ps.IsDeleted == false)
                                        .Select(ps => new ProductSizeViewModel()
                                        {
                                            SizeId = ps.SizeId,
                                            ProductId = ps.ProductId,
                                            SizeName = ps.Size.Name,
                                            UnitsInStock = ps.UnitsInStock,
                                        }),
                            Categories = p.Categories
                                          .Where(cp => cp.Category.IsDeleted == false)
                                          .Select(cp => new ProductCategoryViewModel()
                                          {
                                            ProductId = cp.ProductId,
                                            CategoryId = cp.CategoryId,
                                            CategoryName = cp.Category.Name,
                                          }),
                        }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditProductViewModel> GetViewModelForEditByIdAsync(string id)
        {
            var model = await this.repo
                        .AsNoTracking<Product>(p => p.Id == id)
                        .Where(p => p.IsDeleted == false)
                        .Include(p => p.Brand)
                        .Where(p => p.Brand.IsDeleted == false)
                        .Include(p => p.Sizes)
                        .Include(p => p.ProductType)
                        .Where(p => p.ProductType.IsDeleted == false)
                        .Include(p => p.Categories)
                        .Select(p => new EditProductViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            ImageUrl = p.ImageUrl,
                            UnitPrice = p.UnitPrice,
                            Description = p.Description,
                            Color = p.Color,
                            BrandId = p.BrandId,
                            Brand = p.Brand.Name,
                            ProductTypeId = p.ProductTypeId,
                            ProductType = p.ProductType.Name,
                            GenderId = (int)p.Gender,
                            Gender = GetGenderAsStringById((int)p.Gender),
                            MultiCategoriesIds = p.Categories
                                            .Where(c => c.Category.IsDeleted == false)
                                            .Select(c => c.CategoryId)
                                            .ToList(),
                        }).FirstOrDefaultAsync();

            model.ProductTypes = await this.GetProductTypesAsync();
            model.Brands = await this.GetBrandsAsync();
            model.Genders = this.GetGenders();

            return model;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await this.repo.All<Category>()
                                  .Where(c => c.IsDeleted == false)
                                  .Select(c => new CategoryViewModel
                                  {
                                    Id = c.Id,
                                    Name = c.Name,
                                  }).ToListAsync();
        }

        /// <summary>
        /// Get all product types.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync()
        {
            return await this.repo.AsNoTracking<ProductType>()
                                  .Where(pt => pt.IsDeleted == false)
                                  .Select(pt => new ProductTypeViewModel
                                  {
                                     Id = pt.Id,
                                     Name = pt.Name,
                                  })
                                  .ToListAsync();
        }

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BrandViewModel>> GetBrandsAsync()
        {
            return await this.repo.AsNoTracking<Brand>()
                                  .Where(b => b.IsDeleted == false)
                                  .Select(b => new BrandViewModel
                                  {
                                     Id = b.Id,
                                     Name = b.Name,
                                  })
                                  .OrderBy(b => b.Name)
                                  .ToListAsync();
        }

        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SizeViewModel>> GetSizesAsync()
        {
            var sizes = await this.repo.AsNoTracking<Size>()
                                       .Where(s => s.IsDeleted == false)
                                       .Select(s => new SizeViewModel
                                       {
                                         Id = s.Id,
                                         Name = s.Name,
                                       }).ToListAsync();
            return sizes;
        }

        /// <summary>
        /// Get all genders.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GenderViewModel> GetGenders()
        {
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

            return genders;
        }

        /// <summary>
        /// Get Gender as string.
        /// </summary>
        /// <param name="genderId"></param>
        /// <returns></returns>
        public static string GetGenderAsStringById(int genderId)
        {
            switch (genderId)
            {
                case (int)Gender.Male:
                    return "Male";
                    break;
                case (int)Gender.Female:
                    return "Female";
                    break;
                case (int)Gender.Unisex:
                    return "Unisex";
                    break;
            }

            return "Not specified";
        }

        public async Task<AllProductsQueryViewModel> GetAllSorted(string genderId, IEnumerable<string> multiCategoriesIds, string productTypeId, IEnumerable<string> multiBrandsIds, IEnumerable<string> multiSizesIds, string searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6)
        {
            var productsQuery = this.repo.AsNoTracking<Product>()
                              .Include(p => p.Brand)
                              .Where(p => p.Brand.IsDeleted == false)
                              .Include(p => p.ProductType)
                              .Where(p => p.ProductType.IsDeleted == false)
                              .Include(p => p.Categories)
                              .Include(p => p.Sizes)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.ProductNumber.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Color.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.ProductType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Brand.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            if (productTypeId != null)
            {
                productsQuery = productsQuery.Where(p => p.ProductTypeId == productTypeId);
            }

            if (genderId != null)
            {
                productsQuery = productsQuery.Where(p => (int)p.Gender == int.Parse(genderId));
            }

            productsQuery = sorting switch
            {
                ProductSorting.HighestPriceFirst => productsQuery
                                                    .OrderByDescending(p => p.UnitPrice),
                ProductSorting.LowestPriceFirst => productsQuery
                                                    .OrderBy(p => p.UnitPrice),
                _ => productsQuery.OrderByDescending(p => p.CreatedOn),
            };

            var result = new AllProductsQueryViewModel();

            if (multiSizesIds != null)
            {
                result.MultiSizesIds = multiSizesIds;
                productsQuery = productsQuery.Where(p => p.Sizes.Any(s => s.IsDeleted == false));
                productsQuery = productsQuery.Where(p => p.Sizes.Any(s => multiSizesIds.Contains(s.SizeId)));
            }

            if (multiCategoriesIds != null)
            {
                result.MultiCategoriesIds = multiCategoriesIds;
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => c.IsDeleted == false));
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => multiCategoriesIds.Contains(c.CategoryId)));
            }

            if (multiBrandsIds != null)
            {
                result.MultiBrandsIds = multiBrandsIds;
                productsQuery = productsQuery.Where(p => multiBrandsIds.Contains(p.BrandId));
            }

            var products = await productsQuery
                                   .Skip((currentPage - 1) * productsPerPage)
                                   .Take(productsPerPage)
                                   .Select(p => new ProductQueryViewModel
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       ProductNumber = p.ProductNumber,
                                       ImageUrl = p.ImageUrl,
                                       UnitPrice = p.UnitPrice,
                                       Brand = p.Brand.Name,
                                       BrandId = p.BrandId,
                                   }).ToListAsync();

            result.TotalProductsCount = productsQuery.Count();
            result.Products = products;

            return result;
        }

        public async Task<ManageAllProductsViewModel> GetManageAllSorted(string searchTerm, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6)
        {
            var productsQuery = this.repo.AsNoTracking<Product>()
                             .Include(p => p.Brand)
                             .Where(p => p.Brand.IsDeleted == false)
                             .Include(p => p.ProductType)
                             .Where(p => p.ProductType.IsDeleted == false)
                             .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.ProductNumber.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Color.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.ProductType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Brand.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.HighestPriceFirst => productsQuery
                                                    .OrderByDescending(p => p.UnitPrice),
                ProductSorting.LowestPriceFirst => productsQuery
                                                    .OrderBy(p => p.UnitPrice),
                _ => productsQuery.OrderByDescending(p => p.CreatedOn),
            };

            var products = await productsQuery
                                    .Skip((currentPage - 1) * productsPerPage)
                                    .Take(productsPerPage)
                                    .Select(p => new ProductQueryManageAllViewModel
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        ProductNumber = p.ProductNumber,
                                        ImageUrl = p.ImageUrl,
                                        UnitPrice = p.UnitPrice,
                                        Color = p.Color,
                                        Brand = p.Brand.Name,
                                        ProductType = p.ProductType.Name,
                                        BrandId = p.BrandId,
                                        ProductTypeId = p.ProductTypeId,
                                        GenderId = (int)p.Gender,
                                        Gender = GetGenderAsStringById((int)p.Gender),
                                    })
                                    .ToListAsync();

            var totalProductsCount = productsQuery.Count();

            return new ManageAllProductsViewModel()
            {
                Products = products,
                TotalProductsCount = totalProductsCount,
            };
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetAllProducts()
        {
            return await this.repo.All<Product>().ToListAsync();
        }

        public async Task<bool> ExistsById(string id)
        {
            return await repo.All<Product>()
                .AnyAsync(a => a.Id == id);
        }
    }
}