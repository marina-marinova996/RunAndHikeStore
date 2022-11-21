using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.User
{
    public class UserListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
