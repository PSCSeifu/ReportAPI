using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Types.DTOs
{
    public class SortItemDTO
    {

        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; } = 0;
        public string OrderBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
