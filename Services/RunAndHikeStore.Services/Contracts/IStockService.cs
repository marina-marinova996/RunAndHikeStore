﻿using RunAndHikeStore.Web.ViewModels.Stock;
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
        Task DeleteStock(EditStockViewModel model);

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
    }
}
