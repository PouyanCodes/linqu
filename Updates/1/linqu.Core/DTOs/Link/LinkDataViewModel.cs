

using System;

namespace linqu.Core.DTOs.Link
{
    public class LinkDataViewModel
    {
        public int LinkId { get; set; }
        public int LinkOwnerId { get; set; }
        public string LinkTitle { get; set; }
        public string LinkAddress { get; set; }
        public string Description { get; set; }
        public string ShortKey { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
