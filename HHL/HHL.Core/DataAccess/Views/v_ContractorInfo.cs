using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Views
{
    [PgsDataTable(EntityAcceesNameHdr.ContractorInfoes)]
    public class v_ContractorInfo : HHLBaseRepository<v_ContractorInfo>
    {


        [PgsPK]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIdentity { get; set; }

        public string SIN { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? ApplicationStep { get; set; }
        public int ContractorStatusId { get; set; }
        public int? ContractorPlanId { get; set; }

        
        public Guid? PrimaryLanguageId { get; set; }
        public Guid? SecondaryLanguageId { get; set; }

        public Guid? PrimaryPhoneId { get; set; }
        public Guid? SecondaryPhoneId { get; set; }

        public Guid? PrimaryEmailId { get; set; }

        public string PrimaryEmailName { get; set; }

        public Guid? SecondaryEmailId { get; set; }

        public string SecondaryEmailName { get; set; }

        public Guid? BillingAddressId { get; set; }

        public bool HasLocation { get; set; }

        public Guid? LocationAddressId { get; set; }

    }
}