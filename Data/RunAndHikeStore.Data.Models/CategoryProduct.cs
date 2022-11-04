namespace RunAndHikeStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryProduct
    {
        /// <summary>
        /// FK to Products Table. Part of composite key.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// FK to Categories Table. Part of composite key.
        /// </summary>
        [Required]
        public string CategoryId { get; set; }

        /// <summary>
        /// Navigation property to Products table.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// Navigation property to Categories table.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public bool IsDeleted { get; set; }
    }
}
