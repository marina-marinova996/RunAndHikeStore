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

        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddCategoryViewModel();

            this.ViewData["Title"] = "Add Category";

            return this.View(model);
        }

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

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditCategoryViewModel model)
        {
            await this.categoryService.Edit(id, model);

            return this.RedirectToAction("ManageAll", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> ManageAll()
        {
            var categories = await this.categoryService.GetAllAsync();
            this.ViewData["Title"] = "Manage Categories";

            return this.View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            CategoryViewModel category = await this.categoryService.GetByIdAsync(id);

            await this.categoryService.Delete(category);

            return this.RedirectToAction(nameof(this.ManageAll));
        }
    }
}
