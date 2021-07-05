using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Services
{
    public class AuditSvc
    {
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
