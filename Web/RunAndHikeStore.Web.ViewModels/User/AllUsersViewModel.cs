namespace RunAndHikeStore.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllUsersViewModel
    {
        /// <summary>
        /// Users per page.
        /// </summary>
        public const int UsersPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Total records for pagination.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public IEnumerable<UserListViewModel> Users { get; set; }
    }
}
