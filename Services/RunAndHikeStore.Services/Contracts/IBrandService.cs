using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Brand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IBrandService
    {
        /// <summary>
        /// Add new brand.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Add(AddBrandViewModel model);

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="brandsPerPage"></param>
        /// <returns></returns>
        Task<AllBrandsViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int brandsPerPage = 6);

        /// <summary>
        /// Get Brand View Model by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BrandViewModel> GetByIdAsync(string id);

        /// <summary>
        /// Edit brand.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Edit(EditBrandViewModel model);

        /// <summary>
        /// Get View Model For Edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditBrandViewModel> GetViewModelForEditByIdAsync(string id);

        /// <summary>
        /// Delete Brand.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        Task<List<Brand>> GetAllBrands();

        /// <summary>
        /// Check if Brand exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}
