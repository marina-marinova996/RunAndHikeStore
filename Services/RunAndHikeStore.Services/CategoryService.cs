namespace RunAndHikeStore.Services
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Category;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public CategoryService(
            IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add new category.
        /// </summary>
        /// <param name="model">Product model.</param>
        /// <returns></returns>
        public async Task Add(AddCategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
            };

            await this.repo.AddAsync(category);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Delete category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Delete(CategoryViewModel model)
        {
            var category = await this.repo.All<Category>()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (category != null)
            {
                category.IsDeleted = true;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(string id, EditCategoryViewModel model)
        {
            var category = await this.repo.All<Category>()
                                          .Where(c => c.IsDeleted == false)
                                          .Where(c => c.Id == id)
                                          .FirstOrDefaultAsync();

            category.Name = model.Name;

            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await this.repo.AsNoTracking<Category>()
                                  .Where(p => p.IsDeleted == false)
                                  .Select(p => new CategoryViewModel()
                                  {
                                    Id = p.Id,
                                    Name = p.Name,
                                  }).ToListAsync();
        }

        /// <summary>
        /// Get category by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryViewModel> GetByIdAsync(string id)
        {
            return await this.repo.AsNoTracking<Category>()
                                  .Where(p => p.IsDeleted == false)
                                  .Select(p => new CategoryViewModel()
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                  }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Category View Model for Edit by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditCategoryViewModel> GetViewModelForEditByIdAsync(string id)
        {
            var category = await this.repo.AsNoTracking<Category>()
                                          .Where(c => c.IsDeleted == false)
                                          .Select(c => new EditCategoryViewModel()
                                          {
                                              Name = c.Name,
                                          }).FirstOrDefaultAsync();

            return category;
        }
    }
}
