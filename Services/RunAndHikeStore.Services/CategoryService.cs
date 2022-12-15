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
        public async Task Delete(string id)
        {
            var category = await this.repo.All<Category>()
                .FirstOrDefaultAsync(p => p.Id == id);

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
        public async Task<AllCategoriesViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int categoriesPerPage = 6)
        {
            var categoryQuery = this.repo.AsNoTracking<Category>()
                                  .Where(p => p.IsDeleted == false)
                                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                categoryQuery = categoryQuery.Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var categories = await categoryQuery
                            .Skip((currentPage - 1) * categoriesPerPage)
                            .Take(categoriesPerPage)
                            .Select(c => new EditCategoryViewModel()
                            {
                                Id = c.Id,
                                Name = c.Name,
                            }).OrderBy(c => c.Name)
                            .ToListAsync();

            var totalRecords = categoryQuery.Count();

            return new AllCategoriesViewModel()
            {
                Categories = categories,
                TotalRecordsCount = totalRecords,
            };
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
                                              Id = c.Id,
                                              Name = c.Name,
                                          }).FirstOrDefaultAsync();

            return category;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetAllCategories()
        {
            return await this.repo.All<Category>().ToListAsync();
        }

        /// <summary>
        /// Check if category exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string id)
        {
            return await repo.All<Category>()
                            .AnyAsync(c => c.Id == id);
        }
    }
}
