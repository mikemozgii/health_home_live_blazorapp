using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core.Services
{
   public interface IDataAccessWorker
    {
        IQueryExecutionSvc _QueryExecutionSvc { get; set; }
    }
}
