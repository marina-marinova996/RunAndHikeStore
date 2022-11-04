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
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();

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
        Task Delete(CategoryViewModel model);

        Task<CategoryViewModel> GetByIdAsync(string id);

        Task<EditCategoryViewModel> GetViewModelForEditByIdAsync(string id);
    }
}
