using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.Core.DataAccess;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Handlers;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.Core.Models.Contractor;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using HHL.PostreSQL.Core.Uuid;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class ContractorSvc : HHLDataAccessSvc
    {
        public Guid ContractorId { get; set; }
        public InstantDatahandler InstantDatahandler { get; set; }
        public ContractorSvc(IHHLQueryExecutionSvc queryExecutionSvc, InstantDatahandler _InstantDatahandler) : base(queryExecutionSvc)
        {
            InstantDatahandler = _InstantDatahandler;

            if (_HHLQueryExecutionSvc._AccountSession.ContractorInfo != null)
            {
                ContractorId = _HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId;
            }
            
        }


        public async Task<IEnumerable<v_Address>> SelectContracotrLocations()
        {
            return (await new AddressSvc(_HHLQueryExecutionSvc).SelectViewByEntityId(ContractorId)).Results;
        }

        public async Task<IEnumerable<e_ContractorSchedule>> SelectContractorWeeklySchedule()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<e_ContractorSchedule>(nameof(e_ContractorSchedule.ContractorId).Pair(ContractorId))).Results;
        }


        public async Task<IEnumerable<e_ContractorExcludeSchedule>> SelectContractorExcludeSchedule()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<e_ContractorExcludeSchedule>(nameof(e_ContractorExcludeSchedule.ContractorId).Pair(ContractorId))).Results;
        }

        public async Task<e_Contractor> SelectCurrent()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyIdAsync<e_Contractor>(ContractorId)).FirstOrDefault;
        }

        public async Task<v_EditContractorInfo> SelectCurrent_EditView()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyIdAsync<v_EditContractorInfo>(ContractorId)).FirstOrDefault;
        }

        public async Task<bool> Update(EditContractorProfileFormModel model)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(ContractorId,
                nameof(e_Contractor.WebSite).Pair(model.WebSite),
                 nameof(e_Contractor.IndustryId).Pair(model.IndustryId),
                  nameof(e_Contractor.CompanyTypeId).Pair(model.CompanyTypeId),
                   nameof(e_Contractor.CompanySizeId).Pair(model.CompanySizeId),
                   nameof(e_Contractor.YearFounded).Pair(model.YearFounded),
                   nameof(e_Contractor.Linkedin).Pair(model.Linkedin),
                   nameof(e_Contractor.Facebook).Pair(model.Facebook),
                   nameof(e_Contractor.Instagram).Pair(model.Instagram),
                   nameof(e_Contractor.Twitter).Pair(model.Twitter),
                   nameof(e_Contractor.OrganizationAbout).Pair(model.OrganizationAbout),
                   nameof(e_Contractor.OrganizationTagline).Pair(model.OrganizationTagline),
                   nameof(e_Contractor.Skype).Pair(model.Skype),
                   nameof(e_Contractor.ShareProfileWithPublic).Pair(model.ShareProfileWithPublic)
                );

            return resp.Success;
        }

        public async Task<bool> Update(e_ContractorPlan plan)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(ContractorId, nameof(e_Contractor.ContractorPlanId).Pair(plan.Id));

            return resp.Success;
        }

        public async Task<bool> EditContractorSchedule(AddSheduleFormModel AddSheduleFormModel)
        {
            var qbh = new QueryBuilderHandler();
            var pkuuidSvc = new PKuuidSvc();
            var query = "";


            var delete_response1 = await _HHLQueryExecutionSvc.DELETEAsync<e_ContractorSchedule>(new QueryFilter(QueryFilter.Equal(nameof(e_ContractorSchedule.ContractorId),ContractorId)));
            var delete_response2 = await _HHLQueryExecutionSvc.DELETEAsync<e_ContractorExcludeSchedule>(new QueryFilter(QueryFilter.Equal(nameof(e_ContractorExcludeSchedule.ContractorId), ContractorId)));

            if (delete_response1.Success && delete_response2.Success)
            {
                foreach (var day in AddSheduleFormModel.Days)
                {
                    foreach (var t in day.AddSheduleDayModels_Include)
                    {
                        if (t.TimeStart != null && t.TimeEnd != null)
                        {
                            query += qbh.INSERT(EntityAcceesNameHdr.ContractorSchedules,
    Pairing.Of(nameof(e_ContractorSchedule.Id), pkuuidSvc.GenereateHHLGuid()),
    Pairing.Of(nameof(e_ContractorSchedule.ContractorId), _HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId),
    Pairing.Of(nameof(e_ContractorSchedule.TimeStart), t.TimeStart),
    Pairing.Of(nameof(e_ContractorSchedule.TimeEnd), t.TimeEnd),
    Pairing.Of(nameof(e_ContractorSchedule.WeekDay), day.Id));
                        }
                    }
                }

                foreach (var day in AddSheduleFormModel.ExclusionDays)
                {
                    query += qbh.INSERT(EntityAcceesNameHdr.ContractorExcludeSchedules,
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.Id), pkuuidSvc.GenereateHHLGuid()),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.ContractorId), _HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.TimeStart), day.TimeStart),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.TimeEnd), day.TimeEnd),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.Name), day.Name),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.Date), day.Date),
                    Pairing.Of(nameof(e_ContractorExcludeSchedule.IsAllDay), day.IsAllDay));
                }


                var query_response = await _HHLQueryExecutionSvc.ExecuteQueryAsync(new QueryRequest(query));

                if (query_response.Success)
                {
                    return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
                }
            }

            return false;
        }


        public async Task<string> SelectCurrerntCurrentStripeCustomerId()
        {
            var client = await SelectCurrent();
            if (string.IsNullOrWhiteSpace(client.StripeCustomerId))
            {
                var stripeCustomer = await new StripeSvc().InsertCustomer(client.Id);
                var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(ContractorId, nameof(e_Contractor.StripeCustomerId).Pair(stripeCustomer.Id));
                if (resp.Success)
                {
                    return stripeCustomer.Id;
                }

            }

            return client.StripeCustomerId;
        }

        public async Task<IEnumerable<PaymentMethod>> SelectCurrentPaymentMethods()
        {
            var stripe_customerId = await SelectCurrerntCurrentStripeCustomerId();
            return await new StripeSvc().SelectPaymentMethods(stripe_customerId);
        }

        public async Task<IEnumerable<e_Contractor_Service>> SelectCurrentContractorServices()
        {
            return (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<e_Contractor_Service>(nameof(e_Contractor_Service.ContractorId).Pair(ContractorId))).Results;
        }

        
        public async Task<PaymentMethod> InsertPaymentMethod(AddPaymentMethodFormModel model)
        {
            var stripe_customerId = await SelectCurrerntCurrentStripeCustomerId();
            return await new StripeSvc().InsertPaymentMethod(stripe_customerId, model);
        }

        public async Task<bool> DeleteContractorService(Guid id)
        {
            return (await _HHLQueryExecutionSvc.DELETEAsync<e_Contractor_Service>(id)).Success;
        }


        public async Task<QueryResponseGeneric<e_Contractor_Service>> Upsert(EditContractorServiceFormModel model)
        {
            return model.Id == null ? await Insert(model) : await Update(model);
        }

        public async Task<QueryResponseGeneric<e_Contractor_Service>> Insert(EditContractorServiceFormModel model)
        {
            var e = new e_Contractor_Service() { ContractorId = ContractorId, ServiceId = model.ServiceId.Value, IsCustomPrice = model.IsCustomPrice, ProductTypeId = model.ProductTypeId.Value };
            if (model.IsCustomPrice)
            {
                e.PricePerHour = model.Price;
            }
            return await _HHLQueryExecutionSvc.INSERTAsync(e);
        }

        public async Task<QueryResponseGeneric<e_Contractor_Service>> Update(EditContractorServiceFormModel model)
        {

            if (!model.IsCustomPrice)
            {
                model.Price = null;
            }

            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor_Service>(model.Id.Value, nameof(e_Contractor_Service.IsCustomPrice).Pair(model.IsCustomPrice), nameof(e_Contractor_Service.PricePerHour).Pair(model.Price));

            return resp;
        }

    }
}
