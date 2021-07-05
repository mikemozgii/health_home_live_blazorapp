using HHL.Auth.Core.Handlers;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesNameHdr.ContractorPlans)]
    public class e_ContractorPlan : HHLBaseRepository<e_ContractorPlan>
    {


        [PgsPK]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceTypeCount { get; set; }
        public int ServiceCategoryCount { get; set; }
        public decimal PriceMontly { get; set; }
        public decimal PriceMonthlyDiscounted  { get; set; }

        public int LocationLimitCount { get; set; }
        public int TaskLimitCount { get; set; }
        public decimal BudgetLimitation { get; set; }
        public decimal Commission { get; set; }

    }
}
