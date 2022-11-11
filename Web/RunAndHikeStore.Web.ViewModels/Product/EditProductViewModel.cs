namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RunAndHikeStore.Common.GlobalConstants.Product;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using RunAndHikeStore.Common;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Web.ViewModels.Category;

    public class EditProductViewModel : AddProductViewModel
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string ProductType { get; set; }

        public string Gender { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
