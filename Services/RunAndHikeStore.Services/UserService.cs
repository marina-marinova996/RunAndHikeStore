namespace RunAndHikeStore.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.User;

    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        /// <summary>
        /// Get user for edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="usersPerPage"></param>
        /// <returns></returns>
        public async Task<AllUsersViewModel> GetUsers(string searchTerm, int currentPage = 1, int usersPerPage = 6)
        {
            var usersQuery = this.repo.All<ApplicationUser>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery.Where(u => u.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                                                    u.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                                                    u.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            var users = await usersQuery.Skip((currentPage - 1) * usersPerPage)
                       .Take(usersPerPage)
                       .Select(u => new UserListViewModel()
                       {
                           Id = u.Id,
                           Email = u.Email,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                       })
                      .ToListAsync();

            var totalRecords = usersQuery.Count();

            return new AllUsersViewModel()
            {
                Users = users,
                TotalRecordsCount = totalRecords,
            };
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            if (await this.ExistsById(model.Id))
            {
                bool result = false;
                var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    await this.repo.SaveChangesAsync();
                    result = true;
                }

                return result;
            }
            else
            {
                  throw new ArgumentException("Unknown user");
            }
        }

        /// <summary>
        /// Check if size exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string id)
        {
            return await repo.All<ApplicationUser>()
                            .AnyAsync(o => o.Id == id);
        }
    }
}
