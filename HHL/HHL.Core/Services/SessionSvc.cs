using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class SessionSvc : HHLDataAccessSvc
    {
        public SessionSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
        }

        public async Task<bool> UpdateContractorSession(AccountSession accountSession)
        {
            var hhlaccSession = new HHLAccountSession()
            {
                ContractorInfo = await GetContractorSessionInfo(accountSession.AccountId),
                ClientInfo = await GetClientSessionInfo(accountSession.AccountId),
                AccountId = accountSession.AccountId,
                AccountName = accountSession.AccountName,
                EmailName = accountSession.EmailName,
                Token = accountSession.Token
            };
            return await RedisHdr.redisStorage.SetValueAsync($"accountId:{accountSession.AccountId}", JsonConvert.SerializeObject(hhlaccSession));         
        }


        public async Task<ContractorInfo> GetContractorSessionInfo(Guid? accountId = null)
        {
            //var query = new v_ContractorInfo().GetActiveByAccountId(accountId);
            //var contractor = (await  _HHLQueryExecutionSvc.ExecuteResultQueryAsync<v_ContractorInfo>(query)).FirstOrDefault;

            v_ContractorInfo contractor;
            if (accountId == null)
            {
                contractor = (await _HHLQueryExecutionSvc.GetByAccountIdFromSessionAsync<v_ContractorInfo>()).FirstOrDefault;
            }
            else
            {
                contractor = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_ContractorInfo>(Pairing.Of(nameof(v_ContractorInfo.AccountId), accountId))).FirstOrDefault;
            }

             
            if (contractor == null) return null;

            var contractorInfo = new ContractorInfo();
            contractorInfo.ApplicationStep = contractor.ApplicationStep;
            contractorInfo.ContractorId = contractor.Id;
            contractorInfo.ContractorName = $"{contractor.FirstName} {contractor.LastName}";
            contractorInfo.EmailName = contractor.PrimaryEmailName;
            contractorInfo.ContractorStatusId = contractor.ContractorStatusId;
            contractorInfo.OrganizationName = contractor.OrganizationName;
            contractorInfo.ContractorPlanId = contractor.ContractorPlanId ?? -1;
            return contractorInfo;
        }

        public async Task<ClientInfo> GetClientSessionInfo(Guid? accountId = null)
        {

            e_Client client;
            if (accountId == null)
            {
                client = (await _HHLQueryExecutionSvc.GetByAccountIdFromSessionAsync<e_Client>()).FirstOrDefault;
            }
            else
            {
                client = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<e_Client>(Pairing.Of(nameof(e_Client.AccountId), accountId))).FirstOrDefault;
            }

          
            if (client == null) return null;

            var clientInfo = new ClientInfo();
            clientInfo.ClientId = client.Id;
            clientInfo.ClientName = $"{client.FirstName} {client.LastName}";
            //clientInfo.EmailName = client.PrimaryEmailName;
            //clientInfo.ContractorStatusId = contractor.ContractorStatusId;
            return clientInfo;
        }

    }
}
