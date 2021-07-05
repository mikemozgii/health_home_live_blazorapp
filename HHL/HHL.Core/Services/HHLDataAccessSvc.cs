using HHL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Services
{
    public class HHLDataAccessSvc : IHHLDataAccess
    {
        public IHHLQueryExecutionSvc _HHLQueryExecutionSvc { get; set; }

        public HHLDataAccessSvc(IHHLQueryExecutionSvc queryExecutionSvc)
        {
            _HHLQueryExecutionSvc = queryExecutionSvc;
        }
    }
}
