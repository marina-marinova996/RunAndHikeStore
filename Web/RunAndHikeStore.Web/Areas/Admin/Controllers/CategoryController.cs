namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Threading.Tasks;

    [Area("Admin")]
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
            EditCategoryViewModel category = await categoryService.GetViewModelForEditByIdAsync(id);

            if (category == null)
            {
                // When product with this Id doesn't exists
                return BadRequest();
            }

            ViewData["Title"] = "Edit Category";

            return View(category);
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
            await categoryService.Edit(id, model);

            return RedirectToAction("ManageAll", "Category");
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
            CategoryViewModel category = await categoryService.GetByIdAsync(id);

            await categoryService.Delete(category);

            return RedirectToAction(nameof(this.ManageAll));
        }
    }
}
