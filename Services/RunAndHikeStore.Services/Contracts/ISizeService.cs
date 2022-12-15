using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Brand;
using RunAndHikeStore.Web.ViewModels.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface ISizeService
    {
        /// <summary>
        /// Add new size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Add(AddSizeViewModel model);

        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="sizesPerPage"></param>
        /// <returns></returns>
        Task<AllSizesViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int sizesPerPage = 6);

        /// <summary>
        /// Get size by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SizeViewModel> GetByIdAsync(string id);

        /// <summary>
        /// Edit size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Edit(SizeViewModel model);

        /// <summary>
        /// Get View Model for Edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SizeViewModel> GetViewModelForEditByIdAsync(string id);

        /// <summary>
        /// Delete size.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);


        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <returns></returns>
        Task<List<Size>> GetAllSizes();

        /// <summary>
        /// Check if size exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}
