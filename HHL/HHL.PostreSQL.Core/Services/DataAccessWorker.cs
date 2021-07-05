using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core.Services
{
    public class DataAccessWorker : IDataAccessWorker
    {
        public IQueryExecutionSvc _QueryExecutionSvc { get; set; }

        public DataAccessWorker(IQueryExecutionSvc queryExecutionSvc)
        {
            _QueryExecutionSvc = queryExecutionSvc;
        }
    }
}
