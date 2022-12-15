using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Stock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IStockService
    {
        /// <summary>
        /// Get all stocks.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="productsPerPage"></param>
        /// <returns></returns>
        Task<AllStocksViewModel> GetAllStocksAsync(string searchTerm, int currentPage = 1, int productsPerPage = 6);

        /// <summary>
        /// Add new stock.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddStock(AddStockViewModel model);

        /// <summary>
        /// Get View Model by Product Id.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<AddStockViewModel> GetStockViewModelByProductId(string productId);

        /// <summary>
        /// Delete stock.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteStock(string productId, string sizeId);

        /// <summary>
        /// Edit Stock.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditStock(EditStockViewModel model);

        /// <summary>
        /// Get View Model For edit.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>

        Task<EditStockViewModel> GetViewModelForEdit(string productId, string sizeId);

        /// <summary>
        /// Get all stocks.
        /// </summary>
        /// <returns></returns>
        Task<List<ProductSize>> GetAllStocks();

        /// <summary>
        /// Check if order exists.
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsById(string productId, string sizeId);
    }
}
