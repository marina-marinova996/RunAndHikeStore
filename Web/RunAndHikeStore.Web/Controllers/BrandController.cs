namespace RunAndHikeStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;

    using System.Threading.Tasks;

    public class BrandController : BaseController
    {
        private IBrandService brandService;

        public BrandController(IBrandService _brandService)
        {
            this.brandService = _brandService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageAll()
        {
            var brands = await this.brandService.GetAllAsync();
            this.ViewData["Title"] = "Manage Brands";

            return this.View(brands);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddBrandViewModel();

            this.ViewData["Title"] = "Add Brand";

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBrandViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.brandService.Add(model);

            return this.RedirectToAction("ManageAll", "Brand");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            EditBrandViewModel brand = await this.brandService.GetViewModelForEditByIdAsync(id);

            if (brand == null)
            {
                // When product with this Id doesn't exists
                return this.BadRequest();
            }

            this.ViewData["Title"] = "Edit Brand";

            return this.View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            await this.brandService.Edit(model);

            return this.RedirectToAction("ManageAll", "Brand");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.brandService.Delete(id);

            return this.RedirectToAction(nameof(this.ManageAll));
        }
    }
}
