using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core.Models
{
    public class ReturnDataSettings
    {
        public string[] ColumnNames { get; set; }

        public ReturnDataSettings(params string[] columnNames)
        {
            ColumnNames = columnNames;
        }
    }
}
