using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.DataAccess
{
    public enum HomeTaskStatus
    {
        New =1,
        Paid =2,
        Deleted = 3,
        WaitingforConfirmation = 4,
        InProgress = 5,
        InReview = 6,
        Completed = 7
    }
}
