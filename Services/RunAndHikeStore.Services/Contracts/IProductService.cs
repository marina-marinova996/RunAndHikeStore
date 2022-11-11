namespace RunAndHikeStore.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.Product.Enum;
    using RunAndHikeStore.Web.ViewModels.Size;

    public interface IProductService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of products.</returns>
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        /// <summary>
        /// Gets all products by type.
        /// </summary>
        /// <returns>List of products.</returns>
        Task<IEnumerable<ProductViewModel>> GetAllByProductTypeAsync(string productTypeId);

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="productViewModel">Product model.</param>
        /// <returns></returns>
        Task Add(AddProductViewModel model);

        /// <summary>
        /// Delete particular product.
        /// </summary>
        Task Delete(ProductViewModel model);

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
       /// <param name="id"></param>
       /// <param name="model"></param>
       /// <returns></returns>
        Task Edit(string id, EditProductViewModel model);

        Task<AllProductsViewModel> GetAllSorted(string searchTerm, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage=6);
    }
}
