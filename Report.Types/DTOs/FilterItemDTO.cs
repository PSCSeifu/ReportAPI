using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Types.DTOs
{
   public class FilterItemDTO
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; } = 0;
        public string Operation { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public string Value { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
