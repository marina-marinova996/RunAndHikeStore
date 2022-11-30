using RunAndHikeStore.Web.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface ICustomerService
    {
        Task<EditBillingDetailsViewModel> GetCustomerBillingDetailsByUserId(string userId);

        Task<AddressViewModel> GetCustomerDeliveryAddressByUserId(string userId);
    }
}
