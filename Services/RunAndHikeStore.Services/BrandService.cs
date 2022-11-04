namespace RunAndHikeStore.Services
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Brand;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BrandService : IBrandService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public BrandService(IRepository _repo)
        {
            this.repo = _repo;
        }

        /// <summary>
        /// Add new brand.
        /// </summary>
        /// <param name="model">Brand model.</param>
        /// <returns></returns>
        public async Task Add(AddBrandViewModel model)
        {
            var brand = new Brand()
            {
                Name = model.Name,
            };

            await this.repo.AddAsync(brand);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Delete brand.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            var brand = await this.repo.All<Brand>()
                                       .Where(b => b.IsDeleted == false)
                                       .FirstOrDefaultAsync();

            if (brand != null)
            {
                brand.IsDeleted = true;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit brand.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(EditBrandViewModel model)
        {
            var brand = await this.repo.All<Brand>()
                                       .Where(b => b.IsDeleted == false)
                                       .Where(b => b.Id == model.Id)
                                       .FirstOrDefaultAsync();

            if (brand != null)
            {
                brand.Name = model.Name;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BrandViewModel>> GetAllAsync()
        {
            return await this.repo.AsNoTracking<Brand>()
                                  .Where(b => b.IsDeleted == false)
                                  .Select(b => new BrandViewModel()
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                  }).ToListAsync();
        }

        /// <summary>
        /// Get brand by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BrandViewModel> GetByIdAsync(string id)
        {
            return await this.repo.AsNoTracking<Brand>()
                                  .Where(b => b.IsDeleted == false)
                                  .Where(b => b.Id == id)
                                  .Select(b => new BrandViewModel()
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                  }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Brand View model for Edit by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditBrandViewModel> GetViewModelForEditByIdAsync(string id)
        {
            return await this.repo.All<Brand>()
                                  .Where(b => b.IsDeleted == false)
                                  .Where(b => b.Id == id)
                                  .Select(b => new EditBrandViewModel()
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                  }).FirstOrDefaultAsync();
        }
    }
}
