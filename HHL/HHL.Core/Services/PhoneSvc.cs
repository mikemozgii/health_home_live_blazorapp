using AutoMapper;
using HHL.Auth.Core.Models;
using HHL.Common;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class PhoneSvc : HHLDataAccessSvc
    {

        public PhoneSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            
        }

        public async Task<bool> Upsert(Client_EditContactInfoFormModel model)
        {
            QueryResponseGeneric<e_Phone> resp;
            if (model.PrimaryPhoneId != null)
            {
                resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Phone>(model.PrimaryPhoneId,
    nameof(e_Phone.Number).Pair(model.PrimaryPhoneNumber),
    nameof(e_Phone.CountryCodeId).Pair(model.PrimaryPhoneCountryCodeId)
    );
            }
            else
            {
                var e = new e_Phone() { Number = model.PrimaryPhoneNumber, CountryCodeId = model.PrimaryPhoneCountryCodeId };
                resp = await _HHLQueryExecutionSvc.INSERTAsync(e);
                if (resp.Success)
                {
                    var client_repose = await _HHLQueryExecutionSvc.UPDATEAsync<e_Client>(_HHLQueryExecutionSvc._AccountSession.ClientInfo.ClientId, nameof(e_Client.PrimaryPhoneId).Pair(resp.FirstOrDefault.Id));
                }
                
            }



            return resp.Success;
        }
        
    }
}
