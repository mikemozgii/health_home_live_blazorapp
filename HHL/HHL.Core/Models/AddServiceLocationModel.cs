using AutoMapper;
using HHL.Core.DataAccess.Entities;
using HHL.Core.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HHL.Core.Models
{
    public class AddServiceLocationModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid? CountryId { get; set; }

        [Required]
        public Guid? RegionId { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int AreaKm { get; set; }
    }
}
