using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.User
{
    public class UserListViewModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First Name.
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
    }
}
