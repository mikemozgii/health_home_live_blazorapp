using AutoMapper;
using HHL.Auth.Core.Models;
using HHL.Common;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.Core.Models.Address;
using HHL.Core.Models.Client;
using HHL.PostreSQL.Core.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class ClientSvc : HHLDataAccessSvc
    {
        public Guid ClientId { get; set; }
        public ClientSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            ClientId = _HHLQueryExecutionSvc._AccountSession.ClientInfo.ClientId;
        }

        public async Task<bool> Update(Client_EditPersonaInfoFormModel model)
        {


            var resp =  await _HHLQueryExecutionSvc.UPDATEAsync<e_Client>(ClientId, 
                nameof(e_Client.FirstName).Pair(model.FirstName),
                nameof(e_Client.LastName).Pair(model.LastName),
                nameof(e_Client.OrganizationName).Pair(model.OrganizationName),
                nameof(e_Client.DateOfBirth).Pair(model.DateOfBirth),
                nameof(e_Client.PrimaryLanguageId).Pair(model.PrimaryLanguageId),
                nameof(e_Client.SecondaryLanguageId).Pair(model.SecondaryLanguageId)
                );

            return resp.Success;
        }

        public async Task<bool> Update(e_ClientPlan plan)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Client>(ClientId,nameof(e_Client.ClientPlanId).Pair(plan.Id));

            return resp.Success;
        }

        public async Task<bool> Update(Client_EditSettingsFormModel model)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Client>(ClientId,
                nameof(e_Client.TimeZoneId).Pair(model.TimeZoneId),
                 nameof(e_Client.DefaultCountryId).Pair(model.DefaultCountryId),
                  nameof(e_Client.DefaultRegionId).Pair(model.DefaultRegionId),
                   nameof(e_Client.DefaultCityId).Pair(model.DefaultCityId)
                );

            return resp.Success;
        }
        
        public async Task<e_Client> SelectCurrent()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyIdAsync<e_Client>(ClientId)).FirstOrDefault;
        }

        public async Task<v_EditClientInfo> SelectCurrent_EditClientInfo()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyIdAsync<v_EditClientInfo>(ClientId)).FirstOrDefault;
        }

        public async Task<bool> MakeClientAddressPrimary(Guid adressId)
        {
            if (await new AddressSvc(_HHLQueryExecutionSvc).UpdateTypeByEntityId(ClientId, null))
            {
               return await new AddressSvc(_HHLQueryExecutionSvc).UpdateType(adressId, 1);
            }
           
            return false;
        }

        public async Task<IEnumerable<v_Address>> SelectClientAddresses()
        {
            return (await new AddressSvc(_HHLQueryExecutionSvc).SelectViewByEntityId(ClientId)).Results;
        }

        public async Task<v_Address> SelectCurrentPrimaryAddress()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_Address>(nameof(v_Address.EntityId).Pair(ClientId), nameof(v_Address.TypeId).Pair(1))).FirstOrDefault;
        }


        public async Task<string> SelectCurrerntClientStripeCustomerId()
        {
            var client =  await SelectCurrent();
            if (string.IsNullOrWhiteSpace(client.StripeCustomerId))
            {
               var stripeCustomer =  await new StripeSvc().InsertCustomer(client.Id);
               var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Client>(ClientId, nameof(e_Client.StripeCustomerId).Pair(stripeCustomer.Id));
                if (resp.Success)
                {
                    return stripeCustomer.Id;
                }

            }

            return client.StripeCustomerId;
        }

        public async Task<IEnumerable<PaymentMethod>> SelectCurrentPaymentMethods()
        {
            var stripe_customerId = await SelectCurrerntClientStripeCustomerId();
            return await new StripeSvc().SelectPaymentMethods(stripe_customerId);
        }

        public async Task<PaymentMethod> InsertPaymentMethod(AddPaymentMethodFormModel model)
        {
            var stripe_customerId = await SelectCurrerntClientStripeCustomerId();
            return await new StripeSvc().InsertPaymentMethod(stripe_customerId, model);
        }

        
    }
}
