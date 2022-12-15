namespace RunAndHikeStore.Web.Areas.Customer.Controllers
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
                ViewData["Title"] = "Our Products";
                // Sidebar ViewBags for filtering
                ViewBag.Categories = new List<SelectListItem>();

                var categories = await productService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    ViewBag.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id });
                }

                ViewBag.ProductTypes = new List<SelectListItem>();

                var productTypes = await productService.GetProductTypesAsync();

                foreach (var productType in productTypes)
                {
                    ViewBag.ProductTypes.Add(new SelectListItem() { Text = productType.Name, Value = productType.Id });
                }

                ViewBag.Sizes = new List<SelectListItem>();

                var sizes = await productService.GetSizesAsync();

                foreach (var size in sizes)
                {
                    ViewBag.Sizes.Add(new SelectListItem() { Text = size.Name, Value = size.Id });
                }

                ViewBag.Genders = new List<SelectListItem>();

                var genders = productService.GetGenders();

                foreach (var gender in genders)
                {
                    ViewBag.Genders.Add(new SelectListItem() { Text = gender.Name, Value = gender.Id.ToString() });
                }

                ViewBag.Brands = new List<SelectListItem>();

                var brands = await productService.GetBrandsAsync();

                foreach (var brand in brands)
                {
                    ViewBag.Brands.Add(new SelectListItem() { Text = brand.Name, Value = brand.Id });
                }

                var queryResult = await productService.GetAllSorted(
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

                return View(query);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(query);
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
                if (await this.productService.ExistsById(id))
                {
                    ProductViewModel product = await productService.GetByIdAsync(id);
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
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
