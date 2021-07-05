using System.Threading.Tasks;
using HHL.Auth.Core.Models;
using HHL.Core.DataAccess;
using HHL.Core.Models;
using HHL.Core.Services;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;

namespace HHL.Core.Interfaces
{
    public interface IHHLQueryExecutionSvc : IQueryExecutionSvc
    {
        HHLAuthSessionSvc _AuthSessionSvc { get; set; }
        HHLAccountSession _AccountSession { get; }

        QueryResponseGeneric<T> GetByAccountIdFromSession<T>() where T : HHLBaseRepository<T>, new();
        Task<QueryResponseGeneric<T>> GetByAccountIdFromSessionAsync<T>() where T : HHLBaseRepository<T>, new();

    }
}