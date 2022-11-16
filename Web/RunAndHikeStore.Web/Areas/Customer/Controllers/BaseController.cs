namespace RunAndHikeStore.Web.Areas.Customer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Customer")]
    public class BaseController : Controller
    {
    }
}
