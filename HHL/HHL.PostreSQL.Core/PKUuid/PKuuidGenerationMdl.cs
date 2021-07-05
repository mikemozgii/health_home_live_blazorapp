using HHL.PostreSQL.Core.Enums;

namespace HHL.PostreSQL.Core.Uuid
{
    public class PKuuidGenerationMdl
    {
        public UuidType? UuidType { get; set; }
        public SequentialUuidType? SequentialUuidType { get; set; }
        public bool? RemoveHyphens { get; set; }
        public bool? Uppercase { get; set; }
        public bool? IncludeBraces { get; set; }
        public int? Quantity { get; set; }
    }
}
