using AutoMapper;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Models;
using HHL.Core.Models.Address;
using HHL.Core.Models.Client;
using HHL.Core.Models.Contractor;
using HHL.PostreSQL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core
{

    public class DomainProfile : Profile
    {
        public DomainProfile()
        {

            CreateMap<v_EditContractorInfo, EditContractorInfoModel>();
            CreateMap<v_EditClientInfo, Client_EditPersonaInfoFormModel>();
            CreateMap<e_Client, Client_EditPersonaInfoFormModel>().ReverseMap();
         
            CreateMap<v_EditClientInfo, Client_EditContactInfoFormModel>();
            CreateMap<e_Client, Client_EditSettingsFormModel>().ReverseMap();
            CreateMap<e_Address, AddEditAddressFormModel>().ReverseMap();
            CreateMap<e_Address, AddEditContractorLocationFormModel>().ReverseMap();
            
            CreateMap<e_Contractor, EditContractorLegalInfoFormModel>().ReverseMap();
            CreateMap<e_Contractor, EditContractorProfileFormModel>().ReverseMap();
            CreateMap<v_EditContractorInfo, EditContractorLegalInfoFormModel>().ReverseMap();
            
                //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber == null ? string.Empty :  ((int)src.PhoneNumber).ToString("D9")))
                //.ReverseMap()
                //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.PhoneNumber) ? (int?)null : int.Parse(src.PhoneNumber)));
            CreateMap<e_Language, LanguageSelectModel>();
            CreateMap<e_City, CitySelectModel>();
            CreateMap<e_Priority, PrioritySelectModel>();
            CreateMap<e_Industry, IndustrySelectModel>();
            CreateMap<e_CompanySize, CompanySizeSelectModel>();
            CreateMap<e_CompanyType, CompanyTypeSelectModel>();
            CreateMap<e_HomeTaskCategory, ServiceSelectModel>();
            

            CreateMap<e_TimeZone, TimeZoneSelectModel>();
            CreateMap<e_Country, CountryCodeSelectModel>();
            CreateMap<e_Country, CountrySelectModel>();           
            CreateMap<e_Phone, EditPhoneNumberModel>();
            //CreateMap<EditPhoneNumberModel, e_Phone>().ForMember(q=>q.EntityConnStr, c =>c.Ignore()).ForMember(q => q.EntityAccessName, c => c.Ignore()).ForMember(q => q.EntityPrimaryKeyName, c => c.Ignore())
            //    .ForMember(q => q.EntityPrimaryKeyValue, c => c.Ignore()).ForMember(q => q.Name, c => c.Ignore());
            
            CreateMap<EditPhoneNumberModel, e_Phone>();
            CreateMap<EditAddressModel, e_Address>();
            CreateMap<RegionSelectModel, e_Region>();


        }
        
    }
}
