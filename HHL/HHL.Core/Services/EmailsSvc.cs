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
    public class EmailsSvc : HHLDataAccessSvc
    {

        public EmailsSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            
        }

        public async Task<bool> Update(Client_EditContactInfoFormModel model)
        {
          var  resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Email>(model.PrimaryEmailId, nameof(e_Email.Name).Pair(model.PrimaryEmailName));
          return resp.Success;
        }
        
    }
}
