using HHL.Auth.Core.Handlers;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesNameHdr.Clients)]
    public class e_Client : HHLBaseRepository<e_Client>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string OrganizationName { get; set; }
        public Guid PrimaryEmailId { get; set; }
        public Guid? PrimaryAddressId { get; set; }
        public Guid? PrimaryPhoneId { get; set; }
        public Guid? PrimaryLanguageId { get; set; }
        public Guid? SecondaryLanguageId { get; set; }
        public Guid TimeZoneId { get; set; }
        public int ClientPlanId { get; set; }
        public Guid? DefaultCountryId { get; set; }
        public Guid? DefaultRegionId { get; set; }
        public Guid? DefaultCityId { get; set; }

        public string StripeCustomerId { get; set; }
        
    }
}
