using HHL.Common;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core.Models
{
    public class InsertQuery<T> : BaseQueryGeneric<T> where T : class
    {
        private ReturnDataSettings ReturnDataSettings { get; set; }
        private IEnumerable<object> Values { get; set; }


        public InsertQuery(IEnumerable<object> _values, ReturnDataSettings _ReturnDataSettings = null) 
        {
            Values = _values;
            ReturnDataSettings = _ReturnDataSettings;
            ExpectResults = ReturnDataSettings != null;
            Init();
        }

        public InsertQuery(object _value, ReturnDataSettings _ReturnDataSettings = null) 
        {
            ReturnDataSettings = _ReturnDataSettings;
            Values = new object[] { _value };
            ExpectResults = ReturnDataSettings != null;
            Init();
        }

        public void Init()
        {
            if (ExpectResults)
            {
                if (ReturnDataSettings.ColumnNames.IsNullOrEmpty())
                {
                    Request = new QueryRequest(new QueryBuilderHandler().INSERT_RETURN_ALL(GetTableNameFromAttribute(), Values));
                }
                else
                {
                    Request = new QueryRequest(new QueryBuilderHandler().INSERT_RETURN(GetTableNameFromAttribute(), Values, ReturnDataSettings.ColumnNames));
                }
            }
            else
            {
                Request = new QueryRequest(new QueryBuilderHandler().INSERT(GetTableNameFromAttribute(), Values));
            }
        }

    }
}
