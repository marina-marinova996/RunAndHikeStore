﻿namespace RunAndHikeStore.Web.ViewModels.Brand
{
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Brand;

    public class AddBrandViewModel
    {
        [Required]
        [StringLength(BrandNameMaxLength, MinimumLength = BrandNameMinLength)]
        public string Name { get; set; }
    }
}
