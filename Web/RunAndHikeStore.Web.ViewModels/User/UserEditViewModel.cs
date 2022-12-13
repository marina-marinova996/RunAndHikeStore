using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.User
{
    public class UserEditViewModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First Name.
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name.
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
