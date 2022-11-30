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

        public async Task<EditBillingDetailsViewModel> GetCustomerBillingDetailsByUserId(string userId)
        {
            var billingDetails = await this.repo.AsNoTracking<ApplicationUser>()
                                                .Where(u => u.IsDeleted == false)
                                                .Where(u => u.Id == userId)
                                                .Include(u => u.BillingDetails.Where(b => b.IsDeleted == false))
                                                .FirstOrDefaultAsync();

            return billingDetails.BillingDetails.Select(b => new EditBillingDetailsViewModel
            {
                Id = b.Id,
                FirstName = b.FirstName,
                LastName = b.LastName,
                PhoneNumber = b.PhoneNumber,
                StreetAddress = b.StreetAddress,
                City = b.City,
                Country = b.Country,
                PostalCode = b.PostalCode
            }).FirstOrDefault();
        }

        public async Task<AddressViewModel> GetCustomerDeliveryAddressByUserId(string userId)
        {


            var user = await this.repo.AsNoTracking<ApplicationUser>()
                                  .Where(u => u.IsDeleted == false)
                                  .Where(u => u.Id == userId)
                                  .Include(u => u.Addresses.Where(a => a.AddressType == Data.Models.Enums.AddressType.Delivery))
                                  .FirstOrDefaultAsync();

            var address = user.Addresses.FirstOrDefault();

            return user.Addresses.Where(a => a.IsDeleted == false).Select(a => new AddressViewModel
            {
                Id = a.Id,
                StreetAddress = a.StreetAddress,
                City = a.City,
                Country = a.Country,
                PostalCode = a.PostalCode,
            }).FirstOrDefault();
        }
    }
}
