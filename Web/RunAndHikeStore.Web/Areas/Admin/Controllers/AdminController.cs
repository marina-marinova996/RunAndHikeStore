namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AdminController : BaseController
    {
        /// <summary>
        /// Admin Home Page.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Home()
        {
           return View();
        }
    }
}
