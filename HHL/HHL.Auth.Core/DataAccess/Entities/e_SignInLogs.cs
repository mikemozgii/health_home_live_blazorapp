using HHL.Auth.Core.DataAccess.Views;
using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesName.SignInLogs)]
    public class e_SignInLogs : AuthBaseRepository<e_SignInLogs>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Date { get; set; }
        public bool IsSuccess { get; set; }
        public Guid? AccountId { get; set; }
        public string AccountName { get; set; }

        public QueryRequest INSERT(string userAngent, string ipAddress, bool isSuccess, string accountName, Guid? accountId)
        {
            var dt = DateTime.UtcNow;
            UserAgent = userAngent;
            IpAddress = ipAddress;
            IsSuccess = isSuccess;
            Date = dt;
            AccountId = accountId;
            AccountName = accountName;

            return INSERT();

            //await this.INSERTAsync(e);

            //var qbh = new QueryBuilderHandler();


            //if (account != null)
            //{
            //    string query = "";
            //    if (isSuccess)
            //    {
            //        query = qbh.UPDATE(EntityAcceesName.Accounts, new string[] {
            //    nameof(e_Account.AccessSuccessCount),
            //    nameof(e_Account.AccessSuccessDate),nameof(e_Account.Token)},
            //        new object[] { ++account.AccountAccessSuccessCount, dt, token });
            //    }
            //    else
            //    {
            //        query = qbh.UPDATE(EntityAcceesName.Accounts, new string[] {
            //    nameof(e_Account.AccessFailedCount),
            //    nameof(e_Account.AccessFailedDate)},
            //        new object[] { ++account.AccountAccessFailedCount, dt });

            //    }


            //    QueryExecutionSvc.Sql = query;
            //    var query_response = await QueryExecutionSvc.ExecuteQueryAsync();
            //    return query_response.Success;
            //}



            //return true;


        }
    }
}
