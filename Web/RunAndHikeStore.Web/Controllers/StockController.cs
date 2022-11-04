using Microsoft.AspNetCore.Mvc;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Product;
using RunAndHikeStore.Web.ViewModels.Stock;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.Controllers
{
    public class StockController : BaseController
    {
        private readonly IStockService stockService;
        private readonly IProductService productService;

        public StockController(IStockService stockService, IProductService productService)
        {
            this.stockService = stockService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> AddStock(string productId)
        {
            var model = await this.stockService.GetStockViewModelByProductId(productId);
            model.Sizes = await this.productService.GetSizesAsync();

            this.ViewData["Title"] = "Add Stock";

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockViewModel model)
        {
            this.ViewData["Title"] = "Add Stock";

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.stockService.AddStock(model);

                return this.RedirectToAction(nameof(this.ManageStocks));
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageStocks()
        {
            this.ViewData["Title"] = "Manage Stocks";

            try
            {
                var products = await this.stockService.GetAllStocksAsync();

                foreach (var product in products)
                {
                    product.Sizes = await this.productService.GetSizesAsync();
                }

                return this.View(products);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditStock(string productId, string sizeId)
        {
            try
            {
                EditStockViewModel stockModel = await this.stockService.GetViewModelForEdit(productId, sizeId);
                stockModel.Sizes = await this.productService.GetSizesAsync();

                this.ViewData["Title"] = "Edit Stock";

                return this.View(stockModel);
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditStock(EditStockViewModel stockModel)
        {
            this.ViewData["Title"] = "Edit Stock";

            if (!this.ModelState.IsValid)
            {
                return this.View(stockModel);
            }

            try
            {
                await this.stockService.EditStock(stockModel);

                return this.RedirectToAction(nameof(this.ManageStocks));

            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStock(string productId, string sizeId)
        {
            try
            {
                EditStockViewModel productStock = await this.stockService.GetViewModelForEdit(productId, sizeId);

                await this.stockService.DeleteStock(productStock);

                return this.RedirectToAction(nameof(this.ManageStocks));
            }
            catch (System.Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }
    }
}
