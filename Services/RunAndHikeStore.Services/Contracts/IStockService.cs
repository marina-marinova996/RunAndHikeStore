using RunAndHikeStore.Web.ViewModels.Stock;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IStockService
    {
        Task<AllStocksViewModel> GetAllStocksAsync(string searchTerm, int currentPage = 1, int productsPerPage = 6);

        Task AddStock(AddStockViewModel model);

        Task<AddStockViewModel> GetStockViewModelByProductId(string productId);

        Task DeleteStock(EditStockViewModel model);

        Task EditStock(EditStockViewModel model);

        Task<EditStockViewModel> GetViewModelForEdit(string productId, string sizeId);
    }
}
