using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Interfaces
{
   public interface IAuthDataAccessWorker
    {
        IAuthQueryExecutionSvc _AuthQueryExecutionSvc { get; set; }
    }
}
