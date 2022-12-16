using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.User;
using System.Linq;
using System.Threading.Tasks;
using static RunAndHikeStore.Common.GlobalConstants;
using ApplicationUser = RunAndHikeStore.Data.Models.ApplicationUser;

namespace RunAndHikeStore.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(
            RoleManager<ApplicationRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _userService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Manage Users.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IActionResult> ManageUsers([FromQuery] AllUsersViewModel query)
        {
            this.ViewData["Title"] = "Manage Users";

            var result = await userService.GetUsers(
                                                    query.SearchTerm,
                                                    query.CurrentPage,
                                                    AllUsersViewModel.UsersPerPage);

            query.Users = result.Users;
            query.TotalRecordsCount = result.TotalRecordsCount;

            return View(query);
        }

        /// <summary>
        /// See roles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Roles(string id)
        {
            var user = await userService.GetUserById(id);

            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
            };

            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        /// <summary>
        /// Add/Edit Roles.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await userService.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        /// <summary>
        /// Get user for edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if(await this.userService.ExistsById(id))
            {
                var model = await userService.GetUserForEdit(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Edit user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (await userService.UpdateUser(model))
                {
                    TempData[MessageConstant.SuccessMessage] = "Successfully editted!";
                    return RedirectToAction(nameof(ManageUsers));
                }
                else
                {
                    TempData[MessageConstant.ErrorMessage] = "Error!";
                    return View(model);
                }
            }
            catch (System.ArgumentException)
            {
                return RedirectToAction("Error404NotFound", "Home", new { area = "" });
                throw;
            }
        }
    }
}
