using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// Gets all Categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        Task<AllCategoriesViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int categoriesPerPage = 6);

        /// <summary>
        /// Add a new category.
        /// </summary>
        /// <param name="model">Category model.</param>
        /// <returns></returns>
        Task Add(AddCategoryViewModel model);

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="model">Category model.</param>
        /// <returns></returns>
        Task Edit(string id, EditCategoryViewModel model);

        /// <summary>
        /// Delete particular category.
        /// </summary>
        Task Delete(string id);

        /// <summary>
        /// Get Category by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CategoryViewModel> GetByIdAsync(string id);

        /// <summary>
        /// Get View Model for edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<EditCategoryViewModel> GetViewModelForEditByIdAsync(string id);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetAllCategories();

        /// <summary>
        /// Check if Category exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}
