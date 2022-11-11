namespace RunAndHikeStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Threading.Tasks;

    public class CategoryController : BaseController
    {
        private ICategoryService categoryService;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="_categoryService"></param>
        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddCategoryViewModel();

            this.ViewData["Title"] = "Add Category";

            return this.View(model);
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoryService.Add(model);

            return this.RedirectToAction("ManageAll", "Category");
        }

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            EditCategoryViewModel category = await this.categoryService.GetViewModelForEditByIdAsync(id);

            if (category == null)
            {
                // When product with this Id doesn't exists
                return this.BadRequest();
            }

            this.ViewData["Title"] = "Edit Category";

            return this.View(category);
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
            await this.categoryService.Edit(id, model);

            return this.RedirectToAction("ManageAll", "Category");
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
                var queryResult = await this.categoryService.GetAllAsync(query.SearchTerm,
                                                            query.CurrentPage,
                                                            AllCategoriesViewModel.CategoriesPerPage);

                query.Categories = queryResult.Categories;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                this.ViewData["Title"] = "Manage Categories";

                return this.View(query);
            }
            catch (System.Exception)
            {

                this.ModelState.AddModelError("", "Something went wrong");
                return this.View(query);
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
            CategoryViewModel category = await this.categoryService.GetByIdAsync(id);

            await this.categoryService.Delete(category);

            return this.RedirectToAction(nameof(this.ManageAll));
        }
    }
}
