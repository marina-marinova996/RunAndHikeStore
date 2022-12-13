namespace RunAndHikeStore.Web.ViewModels.User
{
    public class UserRolesViewModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role names.
        /// </summary>
        public string[] RoleNames { get; set; }
    }
}