using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.Customer;
using System.Linq;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public CustomerService(
            IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add billing details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddBillingDetails(BillingDetailsFormViewModel model, string userId)
        {
            var billingDetails = new BillingDetails()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
                CustomerId = userId,
            };

            await this.repo.AddAsync(billingDetails);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Add Delivery Address.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddDeliveryAddress(AddressViewModel model, string userId)
        {
            var address = new Address()
            {
                StreetAddress = model.StreetAddress,
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                CustomerId = userId,
            };

            await this.repo.AddAsync(address);
            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Billing Details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditBillingDetails(EditBillingDetailsViewModel model)
        {
            var billingDetails = await this.repo.All<BillingDetails>()
                                                .Where(b => b.IsDeleted == false)
                                                .Where(b => b.Id == model.Id)
                                                .FirstOrDefaultAsync();

            billingDetails.FirstName = model.FirstName;
            billingDetails.LastName = model.LastName;
            billingDetails.StreetAddress = model.StreetAddress;
            billingDetails.City = model.City;
            billingDetails.Country = model.Country;
            billingDetails.PostalCode = model.PostalCode;
            billingDetails.PhoneNumber = model.PhoneNumber;

            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Delivery Address.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditDeliveryAddress(EditAddressViewModel model)
        {
            var address = await this.repo.All<Address>().Where(a => a.IsDeleted == false).Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            address.StreetAddress = model.StreetAddress;
            address.City = model.City;
            address.Country = model.Country;
            address.PostalCode = model.PostalCode;

            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Get Customer Billing Details by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<EditBillingDetailsViewModel> GetCustomerBillingDetailsByUserId(string userId)
        {
             return await this.repo.AsNoTracking<ApplicationUser>()
                                                .Where(u => u.IsDeleted == false)
                                                .Where(u => u.Id == userId)
                                                .Include(u => u.BillingDetails)
                                                .Select(u => new EditBillingDetailsViewModel
                                                {
                                                    Id = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.Id)
                                                       .FirstOrDefault(),
                                                    FirstName = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.FirstName)
                                                       .FirstOrDefault(),
                                                    LastName = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.LastName)
                                                       .FirstOrDefault(),
                                                    StreetAddress = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.StreetAddress)
                                                       .FirstOrDefault(),
                                                    City = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.City)
                                                       .FirstOrDefault(),
                                                    Country = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.Country)
                                                       .FirstOrDefault(),
                                                    PostalCode = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.PostalCode)
                                                       .FirstOrDefault(),
                                                    PhoneNumber = u.BillingDetails.Where(b => b.IsDeleted == false)
                                                       .Select(b => b.PhoneNumber)
                                                       .FirstOrDefault(),
                                                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Customer Delivery Address by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<EditAddressViewModel> GetCustomerDeliveryAddressByUserId(string userId)
        {
            var user = await this.repo.AsNoTracking<ApplicationUser>()
                                  .Where(u => u.IsDeleted == false)
                                  .Where(u => u.Id == userId)
                                  .Include(u => u.DeliveryAddresses)
                                  .FirstOrDefaultAsync();

            var address = user.DeliveryAddresses.FirstOrDefault();

            return user.DeliveryAddresses.Where(a => a.IsDeleted == false).Select(a => new EditAddressViewModel
            {
                Id = a.Id,
                StreetAddress = a.StreetAddress,
                City = a.City,
                Country = a.Country,
                PostalCode = a.PostalCode,
            }).FirstOrDefault();
        }

        /// <summary>
        /// Check for billing details.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsCustomerHavingBillingDetails(string userId)
        {
            var user = await this.repo.AsNoTracking<ApplicationUser>()
                                      .Where(u => u.IsDeleted == false)
                                      .Where(u => u.Id == userId)
                                      .Include(u => u.BillingDetails.Where(b => b.IsDeleted == false))
                                      .FirstOrDefaultAsync();

            return user.BillingDetails.Any();
        }
    }
}
