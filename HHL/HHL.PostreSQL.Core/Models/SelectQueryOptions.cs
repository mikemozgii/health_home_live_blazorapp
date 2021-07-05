using System.Collections.Generic;

namespace HHL.PostreSQL.Core.Models
{

    /// <summary>
    /// Select query options.
    /// </summary>
    public sealed class SelectQueryOptions
    {

        public long? Skip { get; set; }

        public long? Take { get; set; }

        public IEnumerable<SelectOrderField> OrderFields { get; set; }

    }
}
