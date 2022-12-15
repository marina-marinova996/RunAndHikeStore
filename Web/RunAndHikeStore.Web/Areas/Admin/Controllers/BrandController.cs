namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;

    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants;

    public class BrandController : BaseController
    {
        private IBrandService brandService;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="_brandService"></param>
        public BrandController(IBrandService _brandService)
        {
            brandService = _brandService;
        }

        /// <summary>
        /// Manage all table with brands.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageAll([FromQuery] AllBrandsViewModel query)
        {
            try
            {
                var queryResult = await brandService.GetAllAsync(query.SearchTerm,
                                                             query.CurrentPage,
                                                             AllBrandsViewModel.BrandsPerPage);

                query.Brands = queryResult.Brands;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                ViewData["Title"] = "Manage Brands";

                return View(query);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(query);
            }

        }

        /// <summary>
        /// Add new brand.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddBrandViewModel();

            ViewData["Title"] = "Add Brand";

            return View(model);
        }

        /// <summary>
        /// Add new brand.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await brandService.Add(model);
            TempData[MessageConstant.SuccessMessage] = "Successfully added!";

            return RedirectToAction("ManageAll", "Brand");
        }

        /// <summary>
        /// Edit brand, find by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (await this.brandService.ExistsById(id))
            {
                EditBrandViewModel brand = await brandService.GetViewModelForEditByIdAsync(id);

                if (brand == null)
                {
                    // When product with this Id doesn't exists
                    return BadRequest();
                }

                ViewData["Title"] = "Edit Brand";

                return View(brand);
            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Edit brand.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            try
            {
                if (await this.brandService.ExistsById(model.Id))
                {
                    await brandService.Edit(model);
                    TempData[MessageConstant.SuccessMessage] = "Successfully editted!";

                    return RedirectToAction("ManageAll", "Brand");
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        /// <summary>
        /// Delete brand.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.brandService.ExistsById(id))
            {
                await brandService.Delete(id);

                return RedirectToAction(nameof(this.ManageAll));

            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }
    }
}
