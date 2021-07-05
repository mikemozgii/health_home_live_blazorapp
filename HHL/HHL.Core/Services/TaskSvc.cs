using HHL.Common;
using HHL.Core.DataAccess;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class TaskSvc : HHLDataAccessSvc
    {
        public Guid ClientId { get; set; }
        public TaskSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            ClientId = _HHLQueryExecutionSvc._AccountSession.ClientInfo.ClientId;
        }

        //public async Task<bool> DeleteTask(Guid taskId)
        //{
        //    var taskView = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.TaskId), taskId))).FirstOrDefault;         
        //    var deleteTaskResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeTask>(taskId, nameof(e_HomeTask.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));

        //    if (deleteTaskResponse.Success)
        //    {
        //        var tasksResponse = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.ProjectId), taskView.ProjectId)));
        //        var anyOtherOpenTask = tasksResponse.Results.FirstOrDefault(q => q.TaskStatusId != (int)HomeTaskStatus.Deleted);

        //        if (anyOtherOpenTask == null)
        //        {
        //            var projectUpdateResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeProject>(taskView.ProjectId, nameof(e_HomeProject.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));
        //            return projectUpdateResponse.Success;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }




        //    return false;
        //}


        public async Task<bool> DeleteTask(Guid taskId)
        {
            //var taskView = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.TaskId), taskId))).FirstOrDefault;
            var deleteTaskResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeTask>(taskId, nameof(e_HomeTask.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));

            return deleteTaskResponse.Success;
            //if (deleteTaskResponse.Success)
            //{
            //var tasksResponse = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.ProjectId), taskView.ProjectId)));
            //var anyOtherOpenTask = tasksResponse.Results.FirstOrDefault(q => q.TaskStatusId != (int)HomeTaskStatus.Deleted);

            //if (anyOtherOpenTask == null)
            //{
            //    var projectUpdateResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeProject>(taskView.ProjectId, nameof(e_HomeProject.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));
            //    return projectUpdateResponse.Success;
            //}
            //else
            //{
            //    return true;
            //}
            //}




            //return false;
        }

        public async Task<bool> UpdateNewTask(AddNewHomeTaskModel model)
        {
            var responseUpdateTask = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeTask>(model.TaskId.Value,
                Pairing.Of(nameof(e_HomeTask.Description),model.Description),
                Pairing.Of(nameof(e_HomeTask.Name),model.Name),
                Pairing.Of(nameof(e_HomeTask.Brand),model.Brand),
                Pairing.Of(nameof(e_HomeTask.Model),model.Model),
                Pairing.Of(nameof(e_HomeTask.PriceBase),model.PriceBase),
                Pairing.Of(nameof(e_HomeTask.PriceMultiplier),model.PriceMultiplier),
                Pairing.Of(nameof(e_HomeTask.EstimatedHours),model.EstimatedHours),
                Pairing.Of(nameof(e_HomeTask.PriceTotal),model.PriceTotal),
                Pairing.Of(nameof(e_HomeTask.PriceBaseFinal),model.PriceBaseFinal),
                Pairing.Of(nameof(e_HomeTask.ContactEmail),model.ContactEmail),
                Pairing.Of(nameof(e_HomeTask.ContactPhoneNumber),model.ContactPhoneNumber),
                Pairing.Of(nameof(e_HomeTask.UseClientPrimaryContactInfo), model.UseClientPrimaryContactInfo),
                Pairing.Of(nameof(e_HomeTask.ContactCountryCodeId), model.ContactCountryCodeId),
                Pairing.Of(nameof(e_HomeTask.PriorityId), model.PriorityId),
                Pairing.Of(nameof(e_HomeTask.DateStart), model.DateStart));
    
            return responseUpdateTask.Success;

        }

        public async Task<Guid?> AddOrUpdateNewTask(AddNewHomeTaskModel model)
        {

            if (model.TaskId.HasValue && await UpdateNewTask(model))
            {
                return model.ProjectId;
            }

                

            //var existingOpenTasksResponse = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.ProjectStatusId), 1)));
            Guid projectId;


            var homeTaskCategoryResponse = await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<e_HomeTaskCategory>(Pairing.Of(nameof(e_HomeTaskCategory.Id), model.HomeTaskCategoryId));

            //if (homeTaskCategoryResponse.Success)
            //{

            //}

            //if (existingOpenTasksResponse.Success && existingOpenTasksResponse.HasResults)
            //{
            //    projectId = existingOpenTasksResponse.FirstOrDefault.ProjectId;
            //}
            //else
            //{

            if (!model.ProjectId.HasValue)
            {
                var projectEntity = new e_HomeProject()
                {
                    Name = "Home Service",
                    ClientId = model.ClientId,
                    HomeTaskStatusId = (int)HomeTaskStatus.New,
                    PriorityId = model.PriorityId,
                    HomeBuldingTypeId = homeTaskCategoryResponse.FirstOrDefault.HomeBuldingTypeId
                };

                var responseAddProject = await _HHLQueryExecutionSvc.INSERTAsync(projectEntity);
                if (!responseAddProject.Success) return null;

                projectId = responseAddProject.Results.First().Id;
            }
            else
            {
                projectId = model.ProjectId.Value;
            }


            //}


            var addressId = await new AddressSvc(_HHLQueryExecutionSvc).CopyAddressForTask((Guid)model.AddressId);

            var taskEntity = new e_HomeTask()
            {
                Name = model.Name,
                HomeProjectId = projectId,
                AddressId = addressId,
                Description = model.Description,
                HomeTaskCategoryId = model.HomeTaskCategoryId,
                Brand = model.Brand,
                Model = model.Model,
                PriceBase = model.PriceBase,
                PriceMultiplier = model.PriceMultiplier,
                EstimatedHours = model.EstimatedHours,
                PriceTotal = model.PriceTotal,
                PriceBaseFinal = model.PriceBaseFinal,
                ContactEmail = model.ContactEmail,
                ContactPhoneNumber = model.ContactPhoneNumber,
                HomeTaskStatusId = (int)HomeTaskStatus.New,
                DateStart = model.DateStart,
                ClientId = model.ClientId,
                ContactCountryCodeId = model.ContactCountryCodeId,
                SelectedAddressId = (Guid)model.AddressId,
                UseClientPrimaryContactInfo = model.UseClientPrimaryContactInfo,
                PriorityId = model.PriorityId
            };

            var responseAddTask = await _HHLQueryExecutionSvc.INSERTAsync(taskEntity);
            if (!responseAddTask.Success) return null;

            return projectId;

        }

        public async Task<QueryResponseGeneric<v_VmHomeTask>> SELECTActiveByProjectId(Guid projectId)
        {
            var qf = new QueryFilter(QueryFilter.Equal(nameof(v_VmHomeTask.TaskHomeProjectId), projectId), QueryFilter.NotEqual(nameof(v_VmHomeTask.TaskStatusId), (int)HomeTaskStatus.Deleted), QueryFilter.Equal(nameof(v_VmHomeTask.TaskClientId), ClientId));
            return await _HHLQueryExecutionSvc.SELECTAsync<v_VmHomeTask>(qf);
        }

    }
}