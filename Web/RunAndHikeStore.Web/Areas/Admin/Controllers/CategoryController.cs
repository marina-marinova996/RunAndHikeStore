namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Category;
    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants;

    public class CategoryController : BaseController
    {
        private ICategoryService categoryService;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="_categoryService"></param>
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddCategoryViewModel();

            ViewData["Title"] = "Add Category";

            return View(model);
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.Add(model);
            TempData[MessageConstant.SuccessMessage] = "Successfully added!";

            return RedirectToAction("ManageAll", "Category");
        }

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (await this.categoryService.ExistsById(id))
            {
                ViewData["Title"] = "Edit Category";

                EditCategoryViewModel category = await categoryService.GetViewModelForEditByIdAsync(id);
                return View(category);
            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditCategoryViewModel model)
        {
            if (await this.categoryService.ExistsById(id))
            {
                await categoryService.Edit(id, model);

                return RedirectToAction("ManageAll", "Category");
            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Manage all categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageAll([FromQuery] AllCategoriesViewModel query)
        {
            try
            {
                var queryResult = await categoryService.GetAllAsync(query.SearchTerm,
                                                            query.CurrentPage,
                                                            AllCategoriesViewModel.CategoriesPerPage);

                query.Categories = queryResult.Categories;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                ViewData["Title"] = "Manage Categories";

                return View(query);
            }
            catch (System.Exception)
            {

                ModelState.AddModelError("", "Something went wrong");
                return View(query);
            }
        }

        /// <summary>
        /// Delete category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.categoryService.ExistsById(id))
            {
                await categoryService.Delete(id);

                return RedirectToAction(nameof(this.ManageAll));

            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }
    }
}
