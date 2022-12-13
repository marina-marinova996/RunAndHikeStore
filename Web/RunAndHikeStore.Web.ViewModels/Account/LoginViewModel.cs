namespace RunAndHikeStore.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        /// <summary>
        /// Email for login.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Return URL.
        /// </summary>
        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
