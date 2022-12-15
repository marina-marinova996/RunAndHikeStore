namespace RunAndHikeStore.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Size;

    public class SizeService : ISizeService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public SizeService(IRepository _repo)
        {
            this.repo = _repo;
        }

        /// <summary>
        /// Add new size.
        /// </summary>
        /// <param name="model">Size model.</param>
        /// <returns></returns>
        public async Task Add(AddSizeViewModel model)
        {
            var size = new Size()
            {
                Name = model.Name,
                ProductTypeId = model.ProductTypeId,
            };

            await this.repo.AddAsync(size);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Delete size.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            var size = await this.repo.All<Size>()
                                       .Where(s => s.IsDeleted == false)
                                       .FirstOrDefaultAsync();

            if (size != null)
            {
                size.IsDeleted = true;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit size.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(SizeViewModel model)
        {
            var size = await this.repo.All<Size>()
                                       .Include(s => s.ProductType)
                                       .Where(s => s.IsDeleted == false)
                                       .Where(s => s.Id == model.Id)
                                       .Where(s => s.ProductType.IsDeleted == false)
                                       .FirstOrDefaultAsync();

            if (size != null)
            {
                size.Name = model.Name;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <returns></returns>
        public async Task<AllSizesViewModel> GetAllAsync(string searchTerm, int currentPage = 1, int sizesPerPage = 6)
        {
            var sizesQuery = this.repo.AsNoTracking<Size>()
                                  .Where(s => s.IsDeleted == false)
                                  .Include(s => s.ProductType)
                                  .Where(s => s.ProductType.IsDeleted == false)
                                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sizesQuery = sizesQuery.Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    s.ProductType.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var sizes = await sizesQuery
                            .Skip((currentPage - 1) * sizesPerPage)
                            .Take(sizesPerPage)
                            .Select(s => new SizeViewModel()
                            {
                                Id = s.Id,
                                Name = s.Name,
                                ProductTypeId = s.ProductTypeId,
                                ProductType = s.ProductType.Name,
                            }).OrderBy(s => s.ProductType)
                              .ThenBy(s => s.Name)
                              .ToListAsync();

            var totalRecords = sizesQuery.Count();

            return new AllSizesViewModel()
            {
                Sizes = sizes,
                TotalRecordsCount = totalRecords,
            };
        }

        /// <summary>
        /// Get all sizes.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Size>> GetAllSizes()
        {
            return await this.repo.All<Size>().ToListAsync();
        }

        /// <summary>
        /// Get size by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SizeViewModel> GetByIdAsync(string id)
        {
            return await this.repo.AsNoTracking<Size>()
                                  .Where(s => s.IsDeleted == false)
                                  .Where(s => s.Id == id)
                                  .Include(s => s.ProductType)
                                  .Where(s => s.ProductType.IsDeleted == false)
                                  .Select(s => new SizeViewModel()
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      ProductTypeId = s.ProductTypeId,
                                      ProductType = s.ProductType.Name,
                                  }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Size View model for Edit by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SizeViewModel> GetViewModelForEditByIdAsync(string id)
        {
            return await this.repo.All<Size>()
                                  .Where(s => s.IsDeleted == false)
                                  .Where(s => s.Id == id)
                                  .Include(s => s.ProductType)
                                  .Where(s => s.ProductType.IsDeleted == false)
                                  .Select(s => new SizeViewModel()
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      ProductTypeId = s.ProductTypeId,
                                      ProductType = s.ProductType.Name,
                                  }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Check if size exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string id)
        {
            return await repo.All<Size>()
                            .AnyAsync(o => o.Id == id);
        }
    }
}
