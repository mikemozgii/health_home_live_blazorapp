
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.Core.DataAccess.Entities;
using HHL.Core.Handlers;
using HHL.Core.Interfaces;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using HHL.PostreSQL.Core.Uuid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{

    public class ClientRegistrySvc: HHLDataAccessSvc
    {
        AccAuthSvc _AccAuthSvc { get; set; }
        public ClientRegistrySvc(IHHLQueryExecutionSvc queryExecutionSvc, AccAuthSvc accAuthSvc) : base(queryExecutionSvc)
        {
            _AccAuthSvc = accAuthSvc;
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {

            var response = await _AccAuthSvc.Register(model);

            if (response.Success)
            {
                var qbh = new QueryBuilderHandler();

                var pkuuidSvc = new PKuuidSvc();


                var emailId = pkuuidSvc.GenereateHHLGuid();
                var query1 = qbh.INSERT(EntityAcceesNameHdr.Emails, Pairing.Of(nameof(e_Email.Id), emailId), Pairing.Of(nameof(e_Email.Name), model.Email));

                var clientId = pkuuidSvc.GenereateHHLGuid();
                var query2 = qbh.INSERT(EntityAcceesNameHdr.Clients, Pairing.Of(nameof(e_Client.Id), clientId), Pairing.Of(nameof(e_Client.AccountId), response.AccountId), Pairing.Of(nameof(e_Client.PrimaryEmailId), emailId));

                var query_req = new QueryRequest($"{query1} {query2}");
                var query_response = await _HHLQueryExecutionSvc.ExecuteQueryAsync(query_req);

                return response;
            }





            return response;
        }
    }


}
