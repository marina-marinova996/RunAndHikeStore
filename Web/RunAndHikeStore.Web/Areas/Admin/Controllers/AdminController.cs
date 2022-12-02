namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RunAndHikeStore.Common;
    using System.Threading.Tasks;

    public class AdminController : BaseController
    {
        public async Task<IActionResult> Home()
        {
           return View();
        }
    }
}
