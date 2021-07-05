
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Helpers;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.Core.Services;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    //TODO: This class is really need?
    public  class InstantDatahandler
    {
        public IEnumerable<e_Country> All_Countries;
        public IEnumerable<e_Region> All_Regions;
        public  IEnumerable<e_Language> All_Languages;
        public IEnumerable<e_HomeProjectType> All_HomeProjectTypes;
        public  IEnumerable<e_HomeTaskType> All_HomeTaskTypes;

        public   IEnumerable<e_HomeBuldingType> All_HomeBuldingTypes;
        public IEnumerable<e_HomeTaskServiceType> All_HomeTaskServiceTypes;
        public  IEnumerable<e_HomeTaskCategory> All_HomeTaskCategories;
        public   IEnumerable<v_VmHomeTaskCategory> All_VmHomeTaskCategories;
        public IEnumerable<e_ContractorPlan> All_ContractorPlans;
        public IEnumerable<e_ClientPlan> All_ClientPlans;
        public  IEnumerable<e_FileType> All_FileTypes;
        public  IEnumerable<e_FieldNames> All_FieldNames;
        public  IEnumerable<AreaKmSelectModel> All_AreaKms;
        public IEnumerable<e_TimeZone> All_TimeZones;
        public IEnumerable<e_City> All_Cities;
        public IEnumerable<e_Priority> All_Priorities;
        public IEnumerable<e_Industry> All_Industries;
        public IEnumerable<e_CompanySize> All_CompanySizes;
        public IEnumerable<e_CompanyType> All_CompanyTypes;
        public IEnumerable<BuldingTypeSelectModel> All_BuldingTypes;
        public IEnumerable<ProductTypeSelectModel> All_ProductTypes;

        

        public Dictionary<string, string> All_EmailTemplates;

        //public InstantDatahandler(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        //{
        //}


        public InstantDatahandler(IServiceProvider serviceProvider)
        {
            var country_CA = new Guid("3e4fd539-577f-421e-4582-05a536b75939");
            var country_US = new Guid("3e4fd539-1f83-45d9-96fb-0cad0ec9462d");
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var queryExecutionSvc = (HHLQueryExecutionSvc)serviceScope.ServiceProvider.GetService(typeof(IHHLQueryExecutionSvc));
                All_Countries = queryExecutionSvc.SELECTALL<e_Country>().Results.Where(q => q.Code == "CA" || q.Code == "US");
                All_Regions = queryExecutionSvc.SELECT<e_Region>(QueryFilter.Or(QueryFilter.Equal(nameof(e_Region.CountryId), country_CA), QueryFilter.Equal(nameof(e_Region.CountryId), country_US))).Results.Where(q=>q.Code != "CA.12" && q.Code != "CA.13" && q.Code != "CA.14");
                All_Languages = queryExecutionSvc.SELECTALL<e_Language>().Results;
                All_TimeZones = queryExecutionSvc.SELECTALL<e_TimeZone>().Results;
                All_HomeProjectTypes = queryExecutionSvc.SELECTALL<e_HomeProjectType>().Results;
                All_HomeTaskTypes = queryExecutionSvc.SELECTALL<e_HomeTaskType>().Results;
                All_HomeBuldingTypes = queryExecutionSvc.SELECTALL<e_HomeBuldingType>().Results;
                All_HomeTaskServiceTypes = queryExecutionSvc.SELECTALL<e_HomeTaskServiceType>().Results;
                All_HomeTaskCategories = queryExecutionSvc.SELECTALL<e_HomeTaskCategory>().Results;
                All_VmHomeTaskCategories = queryExecutionSvc.SELECTALL<v_VmHomeTaskCategory>().Results;
                All_ContractorPlans = queryExecutionSvc.SELECTALL<e_ContractorPlan>().Results;
                All_FileTypes = queryExecutionSvc.SELECTALL<e_FileType>().Results;
                All_FieldNames = queryExecutionSvc.SELECTALL<e_FieldNames>().Results;
                All_ClientPlans = queryExecutionSvc.SELECTALL<e_ClientPlan>().Results;
                All_Priorities = queryExecutionSvc.SELECTALL<e_Priority>().Results;
                All_Cities = queryExecutionSvc.SELECT<e_City>(QueryFilter.Or(QueryFilter.Equal(nameof(e_City.CountryId), country_CA), QueryFilter.Equal(nameof(e_City.CountryId), country_US))).Results;

                All_Industries = queryExecutionSvc.SELECTALL<e_Industry>().Results;
                All_CompanySizes = queryExecutionSvc.SELECTALL<e_CompanySize>().Results;
                All_CompanyTypes = queryExecutionSvc.SELECTALL<e_CompanyType>().Results;
            }

            All_ProductTypes = new List<ProductTypeSelectModel>() {

                new ProductTypeSelectModel(1,"Home"),
                new ProductTypeSelectModel(2, "Health"),
                new ProductTypeSelectModel(3, "Life")
            };

            All_BuldingTypes = new List<BuldingTypeSelectModel>() {

                new BuldingTypeSelectModel(1,"Residential"),
                new BuldingTypeSelectModel(2, "Commercial")
            };


            All_AreaKms = new List<AreaKmSelectModel>()
         {
            new AreaKmSelectModel(5, "5 km"),
            new AreaKmSelectModel(10, "15 km"),
            new AreaKmSelectModel(20, "20 km"),
            new AreaKmSelectModel(50, "50 km"),
            new AreaKmSelectModel(100, "100 km"),
            new AreaKmSelectModel(150, "150 km"),
            new AreaKmSelectModel(200, "200 km"),
        };

            LoadEmailTemplates();
        }

        public void Init()
        {
            //var TaskList = new List<Task>();
            //TaskList.Add(new Task(() => { All_Countries = new e_Country().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_Regions = new e_Region().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_Languages = new e_Language().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_HomeProjectTypes = new e_HomeProjectType().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_HomeTaskTypes = new e_HomeTaskType().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_HomeBuldingTypes = new e_HomeBuldingType().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_HomeTaskServiceTypes = new e_HomeTaskServiceType().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_HomeTaskCategories = new e_HomeTaskCategory().SELECTALL().Results; }));
            //TaskList.Add(new Task(() => { All_VmHomeTaskCategories = new v_VmHomeTaskCategory().SELECTALL().Results; }));

            //Task.WaitAll(TaskList.ToArray());




        }

        public void LoadEmailTemplates()
        {

            var emailNames = new string[] { "HelloWorldTemplate" };
            All_EmailTemplates = new Dictionary<string, string>();

            var _assembly = typeof(DomainProfile).Assembly;
            string assemblyName = _assembly.GetName().Name;
            foreach (var emailName in emailNames)
            {
                var template = EmbeddedResourceHelper.GetResourceAsString(_assembly, $"{assemblyName}.EmailTemplates.{emailName}.cshtml");
                All_EmailTemplates.Add(emailName, template);
            }

        }

    }
}
