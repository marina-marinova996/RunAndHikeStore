using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get Customer Billing Details.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<EditBillingDetailsViewModel> GetCustomerBillingDetailsByUserId(string userId);

        /// <summary>
        /// Get Customer Delivery Address.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<EditAddressViewModel> GetCustomerDeliveryAddressByUserId(string userId);

        /// <summary>
        /// Add Billing Details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddBillingDetails(BillingDetailsFormViewModel model, string userId);

        /// <summary>
        /// Edit Billing Details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditBillingDetails(EditBillingDetailsViewModel model);

        /// <summary>
        /// Add Delivery Address.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddDeliveryAddress(AddressViewModel model, string userId);

        /// <summary>
        /// Edit Delivery Address.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditDeliveryAddress(EditAddressViewModel model);

        /// <summary>
        /// Check for billing details.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsCustomerHavingBillingDetails(string userId);
    }
}
