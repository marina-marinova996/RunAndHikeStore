namespace RunAndHikeStore.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
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

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync();

        Task<IEnumerable<BrandViewModel>> GetBrandsAsync();

        Task<IEnumerable<SizeViewModel>> GetSizesAsync();

        IEnumerable<GenderViewModel> GetGenders();

        Task Edit(string id, EditProductViewModel model);
    }
}
