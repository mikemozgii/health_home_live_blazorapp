using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Views
{
    [PgsDataTable(EntityAcceesNameHdr.EditClientInfoes)]
    public class v_EditClientInfo : HHLBaseRepository<v_EditClientInfo>
    {


        [PgsPK]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string OrganizationName { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public Guid? PrimaryLanguageId { get; set; }
        public Guid? SecondaryLanguageId { get; set; }


        public Guid? PrimaryPhoneId { get; set; }
        public Guid? PrimaryPhoneCountryCodeId { get; set; }
        public string PrimaryPhoneNumber { get; set; }

        public Guid? PrimaryEmailId { get; set; }
        public string PrimaryEmailName { get; set; }
    }
}