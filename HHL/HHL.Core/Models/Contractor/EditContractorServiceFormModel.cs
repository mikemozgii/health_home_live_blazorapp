using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models.Contractor
{
    public class EditContractorServiceFormModel
    {
        public Guid? Id { get; set; }

        public int? ProductTypeId { get; set; }
        public int? ServiceId { get; set; }
        public int? BuldingTypeId { get; set;}
        public bool IsCustomPrice { get; set; }
        public decimal? Price { get; set; }

    }
}
