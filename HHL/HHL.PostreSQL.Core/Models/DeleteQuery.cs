using HHL.Common;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HHL.PostreSQL.Core.Models
{
    public class DeleteQuery<T> : BaseQueryGeneric<T> where T: class
    {
        public DeleteQuery(QueryFilter _filter) 
        {
            Request = new QueryRequest(new QueryBuilderHandler().DELETE(GetTableNameFromAttribute(), _filter));
        }
    }
}
