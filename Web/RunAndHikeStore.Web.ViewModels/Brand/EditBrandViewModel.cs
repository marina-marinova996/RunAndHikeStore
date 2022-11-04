namespace RunAndHikeStore.Web.ViewModels.Brand
{
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Brand;

    public class EditBrandViewModel : AddBrandViewModel
    {
        public string Id { get; set; }
    }
}
