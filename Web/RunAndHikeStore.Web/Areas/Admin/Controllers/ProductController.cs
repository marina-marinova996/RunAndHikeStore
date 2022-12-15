namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
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
                ProductTypes = await productService.GetProductTypesAsync(),
                Brands = await productService.GetBrandsAsync(),
                Genders = productService.GetGenders(),
            };

            ViewData["Title"] = "Add Product";

            var categories = await productService.GetCategoriesAsync();

            ViewBag.Categories = new List<SelectListItem>();

            foreach (var category in categories)
            {
                ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
            }

            return View(model);
        }

        /// <summary>
        /// Add new product.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            ViewData["Title"] = "Add Product";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ViewBag.Categories = new List<SelectListItem>();

                var categories = await productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                await productService.Add(model);

                TempData[MessageConstant.SuccessMessage] = "Successfully added!";

                return RedirectToAction(nameof(this.ManageAll));
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        /// <summary>
        /// Manage all table with actions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageAll([FromQuery] ManageAllProductsViewModel query)
        {
            var queryResult = await productService.GetManageAllSorted(
                                                                     query.SearchTerm,
                                                                     query.Sorting,
                                                                     query.CurrentPage,
                                                                     ManageAllProductsViewModel.ProductsPerPage);
            query.Products = queryResult.Products;
            query.TotalProductsCount = queryResult.TotalProductsCount;
            ViewData["Title"] = "Manage Products";

            return View(query);
        }

        /// <summary>
        /// Edit product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Edit Product";

            try
            {
                if (await this.productService.ExistsById(id))
                {
                    ViewBag.Categories = new List<SelectListItem>();

                    var categories = await productService.GetCategoriesAsync();

                    foreach (var category in categories)
                    {
                        ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                    }

                    EditProductViewModel product = await productService.GetViewModelForEditByIdAsync(id);

                    return View(product);
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }

            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
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
                if (await this.productService.ExistsById(model.Id))
                {
                    ViewBag.Categories = new List<SelectListItem>();

                    var categories = await productService.GetCategoriesAsync();

                    foreach (var category in categories)
                    {
                        ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                    }

                    await productService.Edit(model);
                    TempData[MessageConstant.SuccessMessage] = "Successfully editted!";

                    return RedirectToAction(nameof(this.ManageAll));
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
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
                if (await this.productService.ExistsById(id))
                {
                    ProductViewModel product = await productService.GetByIdAsync(id);

                    await productService.Delete(product.Id);

                    return RedirectToAction(nameof(this.ManageAll));
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }
    }
}
