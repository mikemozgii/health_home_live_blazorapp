using HHL.Common;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Services;
using System.Diagnostics;

namespace HHL.PostreSQL.Core.Models
{
    public class SelectQueryGeneric<T> : BaseQueryGeneric<T> where T : class
    {
        private ReturnDataSettings ReturnDataSettings { get; set; }
        private SelectQueryOptions Options { get; set; }
        private QueryFilter Filter { get; set; }
        public SelectQueryGeneric(QueryFilter filter, ReturnDataSettings _ReturnDataSettings = null, SelectQueryOptions options = null) 
        {
            ReturnDataSettings = _ReturnDataSettings;
            Filter = filter;
            ExpectResults = true;
            Init();
            if (options != null)
            {
                Init(options);
            }
            else
            {
                Init();
            }
        }

        private string GetSql(SelectQueryOptions options = null)
        {
            //var t = new T();
            var typeName = typeof(T).GetType().Name;

            //if (typeName != "Object") return new QueryBuilderHandler().SELECT(GetTableNameFromAttribute(), t, Filter, options: options);

            if (ReturnDataSettings != null)
            {
                if (ReturnDataSettings.ColumnNames.IsNullOrEmpty())
                {
                    return new QueryBuilderHandler().SELECT(GetTableNameFromAttribute(), ReturnDataSettings.ColumnNames, Filter, options: options);
                }
                else
                {
                    return new QueryBuilderHandler().SELECT(GetTableNameFromAttribute(), Filter, options: options);
                }

                
            }
            else
            {
                return new QueryBuilderHandler().SELECT(GetTableNameFromAttribute(), Filter, options: options);
            }


          

            //WTF???
            return null;
        }

        private void Init(SelectQueryOptions options = default(SelectQueryOptions))
        {

            Request = new QueryRequest(GetSql(options));

#if DEBUG
            Debug.WriteLine("SQL:" + Request.Sql);
#endif
        }

    }
}
