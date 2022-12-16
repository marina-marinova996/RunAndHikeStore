namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.Stock;
    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants;

    public class StockController : BaseController
    {
        private readonly IStockService stockService;
        private readonly IProductService productService;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="stockService"></param>
        /// <param name="productService"></param>
        public StockController(IStockService stockService, IProductService productService)
        {
            this.stockService = stockService;
            this.productService = productService;
        }

        /// <summary>
        /// Add stock for product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddStock(string productId)
        {
                if (await this.productService.ExistsById(productId))
                {
                    var model = await stockService.GetStockViewModelByProductId(productId);
                    model.Sizes = await productService.GetSizesAsync();

                    ViewData["Title"] = "Add Stock";

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
        }

        /// <summary>
        /// Add Stock for product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockViewModel model)
        {
            try
            {
                if (await this.productService.ExistsById(model.ProductId))
                {
                    this.ViewData["Title"] = "Add Stock";

                    if (!this.ModelState.IsValid)
                    {
                        return View(model);
                    }

                    await stockService.AddStock(model);
                    TempData[MessageConstant.SuccessMessage] = "Successfully added!";

                    return this.RedirectToAction(nameof(this.ManageStocks));
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.ArgumentException)
            {
                ModelState.AddModelError("", "The stock for product with this size Id is already added");
                TempData[MessageConstant.WarningMessage] = $"The stock for product with this size Id was already added earlier!\nYou are redirected to Manage Stocks.";

                return RedirectToAction("ManageStocks", "Stock");
            }
        }

        /// <summary>
        /// Manage stocks for products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageStocks([FromQuery] AllStocksViewModel query)
        {
            this.ViewData["Title"] = "Manage Stocks";

            try
            {
                var queryResult = await stockService.GetAllStocksAsync(
                                                                            query.SearchTerm,
                                                                            query.CurrentPage,
                                                                            AllStocksViewModel.ProductsPerPage);

                query.Stocks = queryResult.Stocks;
                query.TotalRecordsCount = queryResult.TotalRecordsCount;

                return View(query);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Edit stock for product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditStock(string productId, string sizeId)
        {
            try
            {
                if (await this.stockService.ExistsById(productId, sizeId))
                {
                    EditStockViewModel stockModel = await stockService.GetViewModelForEdit(productId, sizeId);
                    stockModel.Sizes = await productService.GetSizesAsync();

                    ViewData["Title"] = "Edit Stock";

                    return View(stockModel);
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Edit stock for product.
        /// </summary>
        /// <param name="stockModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditStock(EditStockViewModel stockModel)
        {
            this.ViewData["Title"] = "Edit Stock";

            if (!this.ModelState.IsValid)
            {
                return View(stockModel);
            }

            try
            {
                if (await this.stockService.ExistsById(stockModel.ProductId, stockModel.SizeId))
                {
                    await stockService.EditStock(stockModel);
                    TempData[MessageConstant.SuccessMessage] = "Successfully editted!";

                    return RedirectToAction(nameof(this.ManageStocks));
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Delete stock for product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteStock(string productId, string sizeId)
        {
            try
            {
                if (await this.stockService.ExistsById(productId, sizeId))
                {
                    await this.stockService.DeleteStock(productId, sizeId);

                    return RedirectToAction(nameof(this.ManageStocks));
                }
                else
                {
                    return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }
    }
}
