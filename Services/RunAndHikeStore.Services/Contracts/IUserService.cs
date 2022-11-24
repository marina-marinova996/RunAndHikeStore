using RunAndHikeStore.Data.Models;
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
        Task<AllUsersViewModel> GetUsers(string searchTerm, int currentPage = 1, int usersPerPage = 6);

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);
    }
}