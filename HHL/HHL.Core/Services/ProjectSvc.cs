using HHL.Common;
using HHL.Core.DataAccess;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using HHL.PostreSQL.Core.Handlers;
using HHL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Services;
using HHL.Auth.Core.Handlers;
using System.Linq;
using HHL.Core.Interfaces;
using HHL.Core.Models;

namespace HHL.Core.Services
{
    public class ProjectSvc : HHLDataAccessSvc
    {

        public Guid ClientId { get; set; }
        public ProjectSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            ClientId = _HHLQueryExecutionSvc._AccountSession.ClientInfo.ClientId;
        }

        public async Task<bool> CheckOutProjectTask(Guid projectId)
        {
            var projectView =  await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(Pairing.Of(nameof(v_VmHomeProject.ProjectId), projectId));
            var taskView = await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeTask>(Pairing.Of(nameof(v_VmHomeTask.TaskHomeProjectId), projectId));
            if (!projectView.HasResults) return false;

            var rs = taskView.Results.Where(q => q.TaskStatusId == (int)HomeTaskStatus.New);

            var qbh = new QueryBuilderHandler();
            var dtn = DateTime.UtcNow;

            var query = "";

            query += new e_HomeProject().UPDATE(projectId,nameof(e_HomeProject.HomeTaskStatusId).Pair((int)HomeTaskStatus.WaitingforConfirmation)).Sql;
            //query += qbh.UPDATE(EntityAcceesNameHdr.HomeProjects, QueryFilter.Equal(nameof(e_HomeProject.Id), projectId), );

            var tasks = rs;

            foreach (var t in tasks)
            {
                //var r = qbh.UPDATE(EntityAcceesNameHdr.HomeTasks, QueryFilter.Equal(nameof(e_HomeTask.Id), t.TaskId), Pairing.Of(nameof(e_HomeTask.HomeTaskStatusId), (int)HomeTaskStatus.Paid));

                var r = new e_HomeTask().UPDATE(t.TaskId, nameof(e_HomeProject.HomeTaskStatusId).Pair((int)HomeTaskStatus.WaitingforConfirmation)).Sql;
                query = query + r;
            }


            var query_response = await _HHLQueryExecutionSvc.ExecuteQueryAsync(new QueryRequest(query));


            return query_response.Success;

        }


        public async Task<bool> Update(AddUpdateProjectFormModel model)
        {
            var responseUpdateTask = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeProject>(model.Id.Value,
                Pairing.Of(nameof(e_HomeProject.PriorityId), model.PriorityId),
                Pairing.Of(nameof(e_HomeProject.Name), model.Name));
            return responseUpdateTask.Success;

        }

        public async Task<Guid> Add(AddUpdateProjectFormModel model)
        {

            var e = new e_HomeProject()
            {
                Name = model.Name,
                PriorityId = model.PriorityId,
                HomeTaskStatusId = (int)HomeTaskStatus.New,
                ClientId = _HHLQueryExecutionSvc._AccountSession.ClientInfo.ClientId,
                HomeBuldingTypeId = model.HomeBuldingTypeId
            };
            var responseUpdateTask = await _HHLQueryExecutionSvc.INSERTAsync(e);
            return responseUpdateTask.FirstOrDefault.Id;

        }


        public async Task<bool> DeleteProject(Guid projectId)
        {
            var taskView = (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeTask>(Pairing.Of(nameof(v_VmHomeTask.TaskHomeProjectId), projectId))).Results;

            //if any task is new than delete all seem to be new project where all task can be deleted
            if (!taskView.Where(q => q.TaskStatusId == (int)HomeTaskStatus.New).IsNullOrEmpty())
            {
                var deleteTasksResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeTask>(new QueryFilter(QueryFilter.Equal(nameof(e_HomeTask.HomeProjectId), projectId)), nameof(e_HomeTask.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));

                if (deleteTasksResponse.Success)
                {
                    var projectdeleteResponse = await _HHLQueryExecutionSvc.UPDATEAsync<e_HomeProject>(projectId, nameof(e_HomeProject.HomeTaskStatusId).Pair((int)HomeTaskStatus.Deleted));
                    return projectdeleteResponse.Success;
                }
            }
          
            return false;
        }

        public async Task<v_VmHomeProject> SELECTByProjectId(Guid projectId)
        {
            return (await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_VmHomeProject>(
                nameof(v_VmHomeProject.ProjectClientId).Pair(ClientId),
                nameof(v_VmHomeProject.ProjectId).Pair(projectId),
                nameof(v_VmHomeProject.ProjectStatusId).Pair((int)HomeTaskStatus.New)
                )).FirstOrDefault;
        }

        public async Task<QueryResponseGeneric<v_VmHomeProject>> SelectCurrentVmHomeProjects()
        {
            var qf = new QueryFilter(QueryFilter.Equal(nameof(v_VmHomeProject.ProjectClientId), ClientId), QueryFilter.NotEqual(nameof(v_VmHomeProject.ProjectStatusId), (int)HomeTaskStatus.Deleted));
            return await _HHLQueryExecutionSvc.SELECTAsync<v_VmHomeProject>(qf);
        }
    }
}
