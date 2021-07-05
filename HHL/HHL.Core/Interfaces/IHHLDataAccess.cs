using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Interfaces
{
   public interface IHHLDataAccess
    {
        IHHLQueryExecutionSvc _HHLQueryExecutionSvc { get; set; }
    }
}
