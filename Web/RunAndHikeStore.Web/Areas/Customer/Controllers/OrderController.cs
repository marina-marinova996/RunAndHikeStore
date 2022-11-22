using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.Areas.Customer.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public OrderController(IOrderService _orderService, IUserService _userService)
        {
            this.orderService = _orderService;
            this.userService = _userService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
