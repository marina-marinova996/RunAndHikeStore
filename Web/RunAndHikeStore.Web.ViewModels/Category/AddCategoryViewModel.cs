namespace RunAndHikeStore.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Category;

    public class AddCategoryViewModel
    {
        /// <summary>
        /// Category Name.
        /// </summary>
        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; }
    }
}
