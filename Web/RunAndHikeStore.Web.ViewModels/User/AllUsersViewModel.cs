using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RunAndHikeStore.Web.ViewModels.User
{
    public class AllUsersViewModel
    {
        public const int UsersPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalRecordsCount { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<UserListViewModel> Users { get; set; }
    }
}
