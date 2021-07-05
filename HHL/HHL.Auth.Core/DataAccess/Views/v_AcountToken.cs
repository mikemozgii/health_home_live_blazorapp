using HHL.Auth.Core.DataAccess.Entities;
using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.DataAccess.Workers
{
    [PgsDataTable(EntityAcceesName.Accounts)]
    public class v_AcountToken : AuthBaseRepository<v_AcountToken>
    {
        [PgsPK]
        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}
