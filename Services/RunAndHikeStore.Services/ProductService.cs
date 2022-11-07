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
    using RunAndHikeStore.Web.ViewModels.Size;

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

            var productSize = new ProductSize
            {
                ProductId = product.Id,
                SizeId = model.Size.SizeId,
                UnitsInStock = model.Size.UnitsInStock,
            };
            product.Sizes.Add(productSize);

            var productCategory = new CategoryProduct
            {
                ProductId = product.Id,
                CategoryId = model.CategoryId,
            };

            product.Categories.Add(productCategory);

            await this.repo.AddAsync(product);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="model">Product model.</param>
        /// <returns></returns>
        public async Task Edit(string id, EditProductViewModel model)
        {
            var product = await this.repo.All<Product>(x => x.Id == id)
                                         .Where(p => p.IsDeleted == false)
                                         .Include(p => p.Brand)
                                         .Include(p => p.Sizes)
                                         .Include(p => p.ProductType)
                                         .Include(p => p.Categories)
                                         .FirstOrDefaultAsync();

            if (product != null)
            {
                product.ProductNumber = model.ProductNumber;
                product.ProductTypeId = model.ProductTypeId;
                product.Brand.Id = model.BrandId;
                product.Name = model.Name;
                product.Description = model.Description;
                product.Color = model.Color;
                product.Gender = (Gender)model.GenderId;
                product.UnitPrice = model.UnitPrice;
                product.Categories = model.Categories
                                                     .Select(c => new CategoryProduct
                                                     {
                                                         CategoryId = c.Id,
                                                         ProductId = product.Id,
                                                     }).ToList();

                await this.repo.SaveChangesAsync();
            }
        }

        public async Task Delete(ProductViewModel model)
        {
            var product = await this.repo.All<Product>()
                                         .Where(p => p.IsDeleted == false)
                                         .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product != null)
            {
                product.IsDeleted = true;
                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of products.</returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await this.repo.AsNoTracking<Product>()
                                  .Where(p => p.IsDeleted == false)
                                  .Include(p => p.Brand)
                                  .Include(p => p.Sizes)
                                  .Include(p => p.ProductType)
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
                                      BrandId = p.BrandId,
                                      Brand = p.Brand.Name,
                                      ProductType = p.ProductType.Name,
                                      ProductTypeId = p.ProductTypeId,
                                      GenderId = (int)p.Gender,
                                      Gender = GetGenderAsStringById((int)p.Gender),
                                      Sizes = p.Sizes
                                               .Where(ps => ps.Size.IsDeleted == false)
                                               .Select(ps => new ProductSizeViewModel()
                                               {
                                                    ProductId = p.Id,
                                                    SizeId = ps.SizeId,
                                                    SizeName = ps.Size.Name,
                                                    UnitsInStock = ps.UnitsInStock,
                                               }),
                                      Categories = p.Categories
                                                   .Where(cp => cp.Category.IsDeleted == false)
                                                   .Select(c => new ProductCategoryViewModel()
                                                   {
                                                      ProductId = c.ProductId,
                                                      CategoryId = c.CategoryId,
                                                      CategoryName = c.Category.Name,
                                                   }),
                                  }).ToListAsync();
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of products.</returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllByProductTypeAsync(string productTypeId)
        {
            return await this.repo.AsNoTracking<Product>()
                                  .Where(p => p.IsDeleted == false)
                                  .Include(p => p.Brand)
                                  .Include(p => p.Sizes)
                                  .Include(p => p.ProductType)
                                  .Where(p => p.ProductTypeId == productTypeId)
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
                                     BrandId = p.BrandId,
                                     Brand = p.Brand.Name,
                                     ProductType = p.ProductType.Name,
                                     ProductTypeId = p.ProductTypeId,
                                     GenderId = (int)p.Gender,
                                     Gender = GetGenderAsStringById((int)p.Gender),
                                     Sizes = p.Sizes
                                              .Where(ps => ps.Size.IsDeleted == false)
                                              .Select(ps => new ProductSizeViewModel()
                                              {
                                                ProductId = p.Id,
                                                SizeId = ps.SizeId,
                                                SizeName = ps.Size.Name,
                                                UnitsInStock = ps.UnitsInStock,
                                              }),
                                     Categories = p.Categories
                                                   .Where(cp => cp.Category.IsDeleted == false)
                                                   .Select(c => new ProductCategoryViewModel()
                                                   {
                                                     ProductId = p.Id,
                                                     CategoryId = c.CategoryId,
                                                     CategoryName = c.Category.Name,
                                                   }),
                                  }).ToListAsync();
        }

        /// <summary>
        /// Get product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetByIdAsync(string id)
        {
            return await this.repo
                        .AsNoTracking<Product>(p => p.Id == id)
                        .Where(p => p.IsDeleted == false)
                        .Include(p => p.Brand)
                        .Include(p => p.Sizes)
                        .Include(p => p.ProductType)
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
            var product = await this.repo
                        .AsNoTracking<Product>(p => p.Id == id)
                        .Where(p => p.IsDeleted == false)
                        .Include(p => p.Brand)
                        .Include(p => p.Sizes)
                        .Include(p => p.ProductType)
                        .Include(p => p.Categories)
                        .Select(p => new EditProductViewModel()
                        {
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
                            Categories = p.Categories
                                            .Where(c => c.Category.IsDeleted == false)
                                            .Select(c => new CategoryViewModel()
                                            {
                                                Id = c.CategoryId,
                                                Name = c.Category.Name,
                                            }),
                        }).FirstOrDefaultAsync();

            product.ProductTypes = await this.GetProductTypesAsync();
            product.Brands = await this.GetBrandsAsync();
            product.Categories = await this.GetCategoriesAsync();
            product.Genders = await this.GetGendersAsync();

            return product;
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
                                         SizeId = s.Id,
                                         SizeName = s.Name,
                                       }).ToListAsync();
            return sizes;
        }

        /// <summary>
        /// Get all genders.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GenderViewModel>> GetGendersAsync()
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
                 Id = (int)Gender.Female,
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
    }
}
