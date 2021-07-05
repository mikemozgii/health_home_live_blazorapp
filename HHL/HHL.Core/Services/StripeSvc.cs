using HHL.Core.DataAccess.Entities;
using HHL.Core.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class StripeSvc
    {


        public async Task<Customer> InsertCustomer(Guid customerId)
        {
            var options = new CustomerCreateOptions
            {
                Name = customerId.ToString()
            };

            var customer = await new CustomerService().CreateAsync(options);

            return customer;
        }


        public async Task<PaymentMethod> InsertPaymentMethod(string stripe_customerId, AddPaymentMethodFormModel model)
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                BillingDetails = new BillingDetailsOptions {

                    Name = model.CardholderName,
                    Address = new AddressOptions {
                        Line1 =model.AddressLine1,
                        Line2 = model.AddressLine2,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        State = model.Region,
                        Country = model.Country
                    }

                },
                Card = new PaymentMethodCardCreateOptions
                {
                    Number = model.CardNumber.ToString(),
                    ExpMonth = model.ExpiryMonth,
                    ExpYear = model.ExpiryYear,
                    Cvc = model.Cvc.ToString(),
                },
            };

            var paymentMethod =  await new PaymentMethodService().CreateAsync(options);

            var attachOptions = new PaymentMethodAttachOptions
            {
                CustomerId = stripe_customerId,
            };
            var attachedPaymentMethod = await new PaymentMethodService().AttachAsync(paymentMethod.Id, attachOptions);

            return attachedPaymentMethod;
        }
        public async Task<bool> DeletePaymentMethod(PaymentMethod model)
        {
            var options = new PaymentMethodDetachOptions()
            {

            };
            var paymentMethod = await new PaymentMethodService().DetachAsync(model.Id, options);

            return paymentMethod != null;
        }
        public async Task<IEnumerable<PaymentMethod>> SelectPaymentMethods(string stripe_customerId)
        {
            var service = new PaymentMethodService();
            var options = new PaymentMethodListOptions
            {
                CustomerId = stripe_customerId,
                Type = "card"
            };
            var paymentmethods = await service.ListAsync(options);

            return paymentmethods;
        }







    }
}
