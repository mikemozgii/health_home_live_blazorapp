using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.Core.DataAccess;
using HHL.Core.DataAccess.Entities;
using HHL.Core.Handlers;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using HHL.PostreSQL.Core.Uuid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class ContractorApplicationSvc : HHLDataAccessSvc
    {
        public InstantDatahandler InstantDatahandler { get; set; }
        public ContractorApplicationSvc(IHHLQueryExecutionSvc queryExecutionSvc, InstantDatahandler _InstantDatahandler) : base(queryExecutionSvc)
        {
            InstantDatahandler = _InstantDatahandler;
        }

        public async Task<bool> UpsertNewContractorApplication(ContractorApplicationModel model)
        {
            if (model.Id != null)
            {
                return await UpdateNewContractorApplication(model);
            }

            return await AddNewContractorApplication(model);

        }


        public async Task<bool> AddNewContractorApplication(ContractorApplicationModel model)
        {
            var dtn = DateTime.UtcNow;
            var emailEntity = new e_Email()
            {
                Name = model.PrimaryEmailName
            };

            var responseAddEmail = await _HHLQueryExecutionSvc.INSERTAsync(emailEntity);
            if (!responseAddEmail.Success) return false;

            var emailId = responseAddEmail.Results.First().Id;

            var phoneEntity = new e_Phone()
            {
                CountryCodeId = model.PhoneCountryCodeId,
                Number = model.PhoneNumber
            };

            var responseAddPhone =  await _HHLQueryExecutionSvc.INSERTAsync(phoneEntity);
            if (!responseAddPhone.Success) return false;

            var phoneId = responseAddPhone.Results.First().Id;


            var addressEntity = new e_Address()
            {
                Line1 = model.Line1,
                Line2 = model.Line2,
                PostalCode = model.PostalCode,
                CountryId = model.CountryId,
                RegionId = model.RegionId,
                CityId = model.CityId,
                FieldNameId = 6
            };

            var responseAddAddress = await _HHLQueryExecutionSvc.INSERTAsync(addressEntity);
            if (!responseAddPhone.Success) return false;

            var addressId = responseAddAddress.Results.First().Id;


            var contractorEntity = new e_Contractor()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                OrganizationName = model.OrganizationName,
                OrganizationIdentity = model.OrganizationIdentity,
                OrganizationTaxNumber = model.OrganizationTaxNumber,
                DateOfBirth = model.DateOfBirth,
                PrimaryEmailId = emailId,
                PrimaryPhoneId = phoneId,
                PrimaryAddressId = addressId,
                ApplicationStep = 1,
                ContractorPlanId = 1,
                ContractorStatusId = 1,
                AccountId = _HHLQueryExecutionSvc._AccountSession.AccountId

            };

            var response = await _HHLQueryExecutionSvc.INSERTAsync(contractorEntity);
            if (!response.Success) return false;

            if (response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;

        }
        public async Task<bool> UpdateNewContractorApplication(ContractorApplicationModel model)
        {
            

            var respContractor = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(model.Id.Value, nameof(e_Contractor.FirstName).Pair(model.FirstName),
     nameof(e_Contractor.LastName).Pair(model.LastName),
      nameof(e_Contractor.OrganizationName).Pair(model.OrganizationName),
       nameof(e_Contractor.OrganizationIdentity).Pair(model.OrganizationIdentity),
       nameof(e_Contractor.OrganizationTaxNumber).Pair(model.OrganizationTaxNumber),
       nameof(e_Contractor.DateOfBirth).Pair(model.DateOfBirth),
       nameof(e_Contractor.ApplicationStep).Pair(1));

           var respPhone = await _HHLQueryExecutionSvc.UPDATEAsync<e_Phone>(model.PrimaryPhoneId.Value,
    nameof(e_Phone.Number).Pair(model.PhoneNumber),
    nameof(e_Phone.CountryCodeId).Pair(model.PhoneCountryCodeId));

            var respAddress = await _HHLQueryExecutionSvc.UPDATEAsync<e_Address>(model.PrimaryAddressId.Value, nameof(e_Address.Line1).Pair(model.Line1),
nameof(e_Address.Line2).Pair(model.Line2),
nameof(e_Address.CountryId).Pair(model.CountryId),
nameof(e_Address.RegionId).Pair(model.RegionId),
nameof(e_Address.CityId).Pair(model.CityId),
nameof(e_Address.PostalCode).Pair(model.PostalCode));


            return respAddress.Success;

        }

        public async Task<bool> AddContractorIdentityFile(AddFileModel model)
        {

            var fileId = await new FileSvc(_HHLQueryExecutionSvc, InstantDatahandler).AddFile_ReturnFileId(model);
            if (fileId == null) return false;

            var response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep),2), Pairing.Of(nameof(e_Contractor.IdentityFileId), fileId));

            if (response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;

        }

        public async Task<bool> AddContractorWorkorizationFile(AddFileModel model)
        {
            var fileId = await new FileSvc(_HHLQueryExecutionSvc, InstantDatahandler).AddFile_ReturnFileId(model);

            if (fileId == null) return false;

            var response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), 3), Pairing.Of(nameof(e_Contractor.WorkAuthorizationFileId), fileId));
            if (response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;

        }

        public async Task<bool> AddContractorPhotoFile(AddFileModel model)
        {

            var fileId = await new FileSvc(_HHLQueryExecutionSvc,InstantDatahandler).AddFile_ReturnFileId(model);

            if (fileId == null) return false;

             var response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), 4), Pairing.Of(nameof(e_Contractor.PhotoFileId), fileId));
            if (response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;

        }

  

        public async Task<bool> UpdateContractorPlanApplication(int planId)
        {
            var dtn = DateTime.UtcNow;
            var entity = new e_Contractor()
            {
                Id = _HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId,
            };


            var response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), 5), Pairing.Of(nameof(e_Contractor.ContractorPlanId), planId));
            if (response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;
        }

        public async Task<bool> AddContractorLocations()
        {
            var query_response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), 6));
            if (query_response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }

            

            return false;
        }

        public async Task<bool> AddContractorServices()
        {
            var query_response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), 7));
            if (query_response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }



            return false;
        }

        public async Task<bool> UpdateApplicationStep(int applicationStep)
        {
            var query_response = await _HHLQueryExecutionSvc.UPDATEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId, Pairing.Of(nameof(e_Contractor.ApplicationStep), applicationStep));
            if (query_response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }
            return false;
        }
        public async Task<bool> AddContractorSchedule(AddSheduleFormModel AddSheduleFormModel)
        {
            var dtn = DateTime.UtcNow;
            var qbh = new QueryBuilderHandler();
            var pkuuidSvc = new PKuuidSvc();
            var query = "";

            foreach (var day in AddSheduleFormModel.Days)
            {
                foreach (var t in day.AddSheduleDayModels_Include)
                {
                    if (t.TimeStart != null && t.TimeEnd != null) {
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


            query += qbh.UPDATE(EntityAcceesNameHdr.Contractors, new QueryFilter(QueryFilter.Equal(nameof(e_Contractor.Id), _HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId)),
                Pairing.Of(nameof(e_Contractor.ApplicationStep),-1),
                Pairing.Of(nameof(e_Contractor.ContractorStatusId),(int)ContractorStatus.ApplicationCompleted));

            var query_response = await _HHLQueryExecutionSvc.ExecuteQueryAsync(new QueryRequest(query));

            if (query_response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }

 
            return false;
        }

        public async Task<bool> DeleteContractor()
        {

            var query_response = await _HHLQueryExecutionSvc.DELETEAsync<e_Contractor>(_HHLQueryExecutionSvc._AccountSession.ContractorInfo.ContractorId);

            if (query_response.Success)
            {
                return await new SessionSvc(_HHLQueryExecutionSvc).UpdateContractorSession(_HHLQueryExecutionSvc._AccountSession);
            }


            return false;
        }


        //public string GenerateContracorModifyQuery(Guid contractorId, Guid session_accountId)
        //{
        //    return new QueryBuilderHandler().UPDATE(EntityAcceesNameHdr.Contractors, new string[] {
        //        nameof(e_Contractor.DateModified),
        //        nameof(e_Contractor.ModifiedBy) },
        //         new object[] { DateTime.UtcNow, session_accountId }, new QueryFilter(new QueryFilter(nameof(e_Contractor.Id), QueryOperators.EQ, contractorId)));
        //}

        //public async Task<bool> UpdateContractorModifyData(Guid contractorId, Guid session_accountId)
        //{
        //    var query_response = await new QueryExecutionSvc().ExecuteQueryAsync(GenerateContracorModifyQuery(contractorId, session_accountId), HHLConfigHdr.ConnStr);
        //    return query_response.Success;
        //}


    }
}
