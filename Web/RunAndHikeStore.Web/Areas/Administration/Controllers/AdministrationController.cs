namespace RunAndHikeStore.Web.Areas.Administration.Controllers
{
    using RunAndHikeStore.Common;
    using RunAndHikeStore.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
