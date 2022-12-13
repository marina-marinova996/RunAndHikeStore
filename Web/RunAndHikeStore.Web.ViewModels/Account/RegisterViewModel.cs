using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;

namespace RunAndHikeStore.Web.ViewModels
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Email for registration.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength=PasswordMinLength)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Confirm password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        /// <summary>
        /// First Name of the user.
        /// </summary>
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last Name.
        /// </summary>
        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;
    }
}
