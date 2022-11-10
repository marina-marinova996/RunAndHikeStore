using Microsoft.AspNetCore.Mvc;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Size;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.Controllers
{
    public class SizeController : BaseController
    {
        private readonly ISizeService sizeService;
        private readonly IProductService productService;

        public SizeController(ISizeService _sizeService, IProductService _productService)
        {
            this.sizeService = _sizeService;
            this.productService = _productService;
        }

        /// <summary>
        /// Manage all sizes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageAll()
        {
            var sizes = await this.sizeService.GetAllAsync();
            this.ViewData["Title"] = "Manage Sizes";

            return this.View(sizes);
        }

        /// <summary>
        /// Add new size.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddSizeViewModel()
            {
                ProductTypes = await this.productService.GetProductTypesAsync(),
            };

            this.ViewData["Title"] = "Add Size";

            return this.View(model);
        }

        /// <summary>
        /// Add new size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddSizeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.sizeService.Add(model);

            return this.RedirectToAction("ManageAll", "Size");
        }

        /// <summary>
        /// Edit size.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            SizeViewModel size = await this.sizeService.GetViewModelForEditByIdAsync(id);

            size.ProductTypes = await this.productService.GetProductTypesAsync();

            if (size == null)
            {
                // When product with this Id doesn't exists
                return this.BadRequest();
            }

            this.ViewData["Title"] = "Edit Size";

            return this.View(size);
        }

        /// <summary>
        /// Edit size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SizeViewModel model)
        {
            await this.sizeService.Edit(model);

            return this.RedirectToAction("ManageAll", "Brand");
        }

        /// <summary>
        /// Delete size.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.sizeService.Delete(id);

            return this.RedirectToAction(nameof(this.ManageAll));
        }
    }
}
