namespace RunAndHikeStore.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// List all products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            this.ViewData["Title"] = "Products";
            try
            {
                var products = await this.productService.GetAllAsync();

                var model = new AllProductsViewModel()
                {
                    ProductTypes = await this.productService.GetProductTypesAsync(),
                    Brands = await this.productService.GetBrandsAsync(),
                    Categories = await this.productService.GetCategoriesAsync(),
                    Genders = await this.productService.GetGendersAsync(),
                    Sizes = await this.productService.GetSizesAsync(),
                    Products = products,
                };

                return this.View(model);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> AllFromProductType(string productTypeId)
        //{
        //    var products = await this.productService.GetAllByProductTypeAsync(productTypeId);

        //    var model = new AllProductsViewModel()
        //    {
        //        ProductTypes = await this.productService.GetProductTypesAsync(),
        //        Brands = await this.productService.GetBrandsAsync(),
        //        Categories = await this.productService.GetCategoriesAsync(),
        //        Genders = await this.productService.GetGendersAsync(),
        //        Sizes = await this.productService.GetSizesAsync(),
        //        Products = products,
        //    };

        //    this.ViewData["Title"] = "";

        //    return this.View(this.All);
        //}

        [HttpGet]
        public async Task<IActionResult> Filter(AllProductsViewModel model)
        {
            this.ViewData["Title"] = "Products";

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel()
            {
                ProductTypes = await this.productService.GetProductTypesAsync(),
                Brands = await this.productService.GetBrandsAsync(),
                Categories = await this.productService.GetCategoriesAsync(),
                Genders = await this.productService.GetGendersAsync(),
                Sizes = await this.productService.GetSizesAsync(),
            };

            this.ViewData["Title"] = "Add Product";

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            this.ViewData["Title"] = "Add Product";

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.productService.Add(model);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageAll()
        {
            var products = await this.productService.GetAllAsync();
            this.ViewData["Title"] = "Manage Products";

            return this.View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["Title"] = "Edit Product";

            try
            {
                EditProductViewModel product = await this.productService.GetViewModelForEditByIdAsync(id);
                return this.View(product);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditProductViewModel model)
        {
            await this.productService.Edit(id, model);

            return this.View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                ProductViewModel product = await this.productService.GetByIdAsync(id);
                return this.View(product);
            }
            catch (System.Exception)
            {

                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ProductViewModel product = await this.productService.GetByIdAsync(id);

                await this.productService.Delete(product);

                return this.RedirectToAction(nameof(this.ManageAll));
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }
    }
}
