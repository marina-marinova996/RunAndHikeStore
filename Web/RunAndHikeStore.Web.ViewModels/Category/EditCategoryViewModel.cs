namespace RunAndHikeStore.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Category;

    public class EditCategoryViewModel : AddCategoryViewModel
    {
        /// <summary>
        /// Category Id.
        /// </summary>
        public string Id { get; set; }
    }
}
