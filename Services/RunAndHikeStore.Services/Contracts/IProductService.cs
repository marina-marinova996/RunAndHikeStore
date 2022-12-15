namespace RunAndHikeStore.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.Product.Enum;
    using RunAndHikeStore.Web.ViewModels.Size;

    public interface IProductService
    {
        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="productViewModel">Product model.</param>
        /// <returns></returns>
        Task Add(AddProductViewModel model);

        /// <summary>
        /// Delete particular product.
        /// </summary>
        Task Delete(string id);

        /// <summary>
        /// Get particular product by Id.
        /// </summary>
        Task<ProductViewModel> GetByIdAsync(string id);

        /// <summary>
        /// Get Edit View Model by Id.
        /// </summary>
        Task<EditProductViewModel> GetViewModelForEditByIdAsync(string id);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        /// <summary>
        /// Get all product types.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync();

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BrandViewModel>> GetBrandsAsync();

        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SizeViewModel>> GetSizesAsync();

        /// <summary>
        /// Get all genders.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GenderViewModel> GetGenders();

       /// <summary>
       /// Edit product.
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        Task Edit(EditProductViewModel model);

        /// <summary>
        /// Get products sorted.
        /// </summary>
        /// <param name="genderId"></param>
        /// <param name="multiCategoriesIds"></param>
        /// <param name="productTypeId"></param>
        /// <param name="multiBrandsIds"></param>
        /// <param name="multiSizesIds"></param>
        /// <param name="searchTerm"></param>
        /// <param name="sorting"></param>
        /// <param name="currentPage"></param>
        /// <param name="productsPerPage"></param>
        /// <returns></returns>
        Task<AllProductsQueryViewModel> GetAllSorted(string genderId, IEnumerable<string> multiCategoriesIds, string productTypeId, IEnumerable<string> multiBrandsIds, IEnumerable<string> multiSizesIds, string searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6);

        /// <summary>
        /// Get products in Manage All section sorted.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sorting"></param>
        /// <param name="currentPage"></param>
        /// <param name="productsPerPage"></param>
        /// <returns></returns>
        Task<ManageAllProductsViewModel> GetManageAllSorted(string searchTerm, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 6);

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAllProducts();

        /// <summary>
        /// Check if product exists.
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}
