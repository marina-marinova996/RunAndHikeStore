namespace RunAndHikeStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> All([FromQuery] AllProductsQueryViewModel query)
        {
            try
            {
                this.ViewData["Title"] = "Our Products";
                // Sidebar ViewBags for filtering
                this.ViewBag.Categories = new List<SelectListItem>();

                var categories = await this.productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    this.ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                this.ViewBag.ProductTypes = new List<SelectListItem>();

                var productTypes = await this.productService.GetProductTypesAsync();

                foreach (var productType in productTypes)
                {
                    this.ViewBag.ProductTypes.Add(new SelectListItem() { Text = productType.Name, Value = productType.Id });
                }

                this.ViewBag.Sizes = new List<SelectListItem>();

                var sizes = await this.productService.GetSizesAsync();

                foreach (var size in sizes)
                {
                    this.ViewBag.Sizes.Add(new SelectListItem() { Text = size.Name, Value = size.Id });
                }

                this.ViewBag.Genders = new List<SelectListItem>();

                var genders = this.productService.GetGenders();

                foreach (var gender in genders)
                {
                    this.ViewBag.Genders.Add(new SelectListItem() { Text = gender.Name, Value = gender.Id.ToString() });
                }

                this.ViewBag.Brands = new List<SelectListItem>();

                var brands = await this.productService.GetBrandsAsync();

                foreach (var brand in brands)
                {
                    this.ViewBag.Brands.Add(new SelectListItem() { Text = brand.Name, Value = brand.Id });
                }

                var queryResult = await this.productService.GetAllSorted(
                                                                         query.GenderId,
                                                                         query.MultiCategoriesIds,
                                                                         query.ProductTypeId,
                                                                         query.MultiBrandsIds,
                                                                         query.MultiSizesIds,
                                                                         query.SearchTerm,
                                                                         query.Sorting,
                                                                         query.CurrentPage,
                                                                         AllProductsQueryViewModel.ProductsPerPage);

                query.Products = queryResult.Products;
                query.TotalProductsCount = queryResult.TotalProductsCount;
                query.MultiCategoriesIds = queryResult.MultiCategoriesIds;
                query.MultiBrandsIds = queryResult.MultiBrandsIds;
                query.MultiSizesIds = queryResult.MultiSizesIds;

                return this.View(query);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View(query);
            }
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel()
            {
                ProductTypes = await this.productService.GetProductTypesAsync(),
                Brands = await this.productService.GetBrandsAsync(),
                Genders = this.productService.GetGenders(),
            };

            this.ViewData["Title"] = "Add Product";

            var categories = await this.productService.GetCategoriesAsync();

            this.ViewBag.Categories = new List<SelectListItem>();

            foreach (var category in categories)
            {
                this.ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
            }

            return this.View(model);
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <returns></returns>
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
                this.ViewBag.Categories = new List<SelectListItem>();

                var categories = await this.productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    this.ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                await this.productService.Add(model);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View(model);
            }
        }

        /// <summary>
        /// Manage all table with actions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageAll([FromQuery] ManageAllProductsViewModel query)
        {
            var queryResult = await this.productService.GetManageAllSorted(
                                                                     query.SearchTerm,
                                                                     query.Sorting,
                                                                     query.CurrentPage,
                                                                     ManageAllProductsViewModel.ProductsPerPage);
            query.Products = queryResult.Products;
            query.TotalProductsCount = queryResult.TotalProductsCount;
            this.ViewData["Title"] = "Manage Products";

            return this.View(query);
        }

        /// <summary>
        /// Edit product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["Title"] = "Edit Product";

            try
            {
                this.ViewBag.Categories = new List<SelectListItem>();

                var categories = await this.productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    this.ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                EditProductViewModel product = await this.productService.GetViewModelForEditByIdAsync(id);
                return this.View(product);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }

        /// <summary>
        /// Edit Product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            try
            {
                this.ViewBag.Categories = new List<SelectListItem>();

                var categories = await this.productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    this.ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                await this.productService.Edit(model);

                return this.RedirectToAction(nameof(this.ManageAll));
            }
            catch
            {
                this.ModelState.AddModelError("", "Something went wrong");
                return this.View();
            }
        }

        /// <summary>
        /// See details for the product by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
