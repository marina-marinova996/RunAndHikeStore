using RunAndHikeStore.Web.ViewModels.Brand;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IBrandService
    {
        Task Add(AddBrandViewModel model);

        Task<IEnumerable<BrandViewModel>> GetAllAsync();

        Task<BrandViewModel> GetByIdAsync(string id);

        Task Edit(EditBrandViewModel model);

        Task<EditBrandViewModel> GetViewModelForEditByIdAsync(string id);

        Task Delete(string id);
    }
}
