using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.Order.Enum;
using RunAndHikeStore.Web.ViewModels.Order;
using RunAndHikeStore.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="usersPerPage"></param>
        /// <returns></returns>
        Task<AllUsersViewModel> GetUsers(string searchTerm, int currentPage = 1, int usersPerPage = 6);

        /// <summary>
        /// Get user for edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserEditViewModel> GetUserForEdit(string id);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(UserEditViewModel model);

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserById(string id);

        /// <summary>
        /// Check if size exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsById(string id);
    }
}