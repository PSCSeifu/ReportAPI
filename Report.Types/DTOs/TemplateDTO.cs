using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Types.DTOs
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OutputFormat { get; set; }
        public List<ReportItemDTO> ReportItems { get; set; }
        public List<SortItemDTO> SortItems { get; set; }
        public List<FilterItemDTO> FilterItems { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
