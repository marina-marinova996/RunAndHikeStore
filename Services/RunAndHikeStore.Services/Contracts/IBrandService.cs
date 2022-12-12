using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Brand;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IBrandService
    {
        Task Add(AddBrandViewModel model);

        Task<AllBrandsViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int brandsPerPage = 6);

        Task<BrandViewModel> GetByIdAsync(string id);

        Task Edit(EditBrandViewModel model);

        Task<EditBrandViewModel> GetViewModelForEditByIdAsync(string id);

        Task Delete(string id);

        Task<List<Brand>> GetAllBrands();
    }
}
