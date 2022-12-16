using Microsoft.AspNetCore.Mvc;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Size;
using System.Threading.Tasks;
using static RunAndHikeStore.Common.GlobalConstants;

namespace RunAndHikeStore.Web.Areas.Admin.Controllers
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
        public async Task<IActionResult> ManageAll([FromQuery] AllSizesViewModel query)
        {
            try
            {
                var queryResult = await this.sizeService.GetAllAsync(query.SearchTerm,
                                                             query.CurrentPage,
                                                             AllSizesViewModel.SizesPerPage);

                query.Sizes = queryResult.Sizes;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                this.ViewData["Title"] = "Manage Sizes";

                return this.View(query);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View(query);
            }
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
            TempData[MessageConstant.SuccessMessage] = "Successfully added!";

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
            if (await this.sizeService.ExistsById(id))
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
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Edit size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SizeViewModel model)
        {
            if (await this.sizeService.ExistsById(model.Id))
            {
                await this.sizeService.Edit(model);
                TempData[MessageConstant.SuccessMessage] = "Successfully editted!";

                return this.RedirectToAction(nameof(this.ManageAll));

            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Delete size.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.sizeService.ExistsById(id))
            {
                await this.sizeService.Delete(id);

                return this.RedirectToAction(nameof(this.ManageAll));
            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }
    }
}
