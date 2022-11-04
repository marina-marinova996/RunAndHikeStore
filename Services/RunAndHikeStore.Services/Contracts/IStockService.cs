using RunAndHikeStore.Web.ViewModels;
using RunAndHikeStore.Web.ViewModels.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IStockService
    {
        Task<IEnumerable<EditStockViewModel>> GetAllStocksAsync();

        Task AddStock(AddStockViewModel model);

        Task<AddStockViewModel> GetStockViewModelByProductId(string productId);

        Task DeleteStock(EditStockViewModel model);

        Task EditStock(EditStockViewModel model);

        Task<EditStockViewModel> GetViewModelForEdit(string productId, string sizeId);
    }
}
