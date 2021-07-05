using HHL.Common;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core.Models
{
    public class UpsertQuery<T> : BaseQueryGeneric<T> where T : class
    {
        private ReturnDataSettings ReturnDataSettings { get; set; }
        private string[] ConflictColumnNames { get; set; }
        private object Value { get; set; }


        public UpsertQuery(object _value, string[] _conflictColumnNames, ReturnDataSettings _ReturnDataSettings = null) 
        {

            ConflictColumnNames = _conflictColumnNames;
            ReturnDataSettings = _ReturnDataSettings;
            ExpectResults = ReturnDataSettings != null;
            Value =  _value;
            Init();
        }

        public void Init()
        {

            if (ExpectResults)
            {
                if (ReturnDataSettings.ColumnNames.IsNullOrEmpty())
                {
                    Request = new QueryRequest(new QueryBuilderHandler().UPSERT_RETURN_ALL(GetTableNameFromAttribute(), Value, ConflictColumnNames));
                  
                }
                else
                {
                    Request = new QueryRequest(new QueryBuilderHandler().UPSERT_RETURN(GetTableNameFromAttribute(), Value, ConflictColumnNames, ReturnDataSettings.ColumnNames));
                }
            }
            else
            {
                Request = new QueryRequest(new QueryBuilderHandler().UPSERT(GetTableNameFromAttribute(), Value, ConflictColumnNames));
            }

        }

    }
}
