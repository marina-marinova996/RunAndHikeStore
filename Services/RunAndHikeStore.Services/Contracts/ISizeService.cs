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
        Task Add(AddSizeViewModel model);

        Task<IEnumerable<SizeViewModel>> GetAllAsync();

        Task<SizeViewModel> GetByIdAsync(string id);

        Task Edit(SizeViewModel model);

        Task<SizeViewModel> GetViewModelForEditByIdAsync(string id);

        Task Delete(string id);
    }
}
