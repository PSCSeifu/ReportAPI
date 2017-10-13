using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Types.DTOs
{
   public class ReportItemDTO
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Table { get; set; }
        public string Type { get; set; }
        public bool IsNullable { get; set; }
        public string Name { get; set; }
        public bool IsDisplayed { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
