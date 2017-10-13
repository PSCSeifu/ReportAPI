using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPI.Models;

namespace ReportAPI.Models
{
    public class TemplateCreateOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OutputFormat { get; set; }
        public List<ReportItem> ReportItems { get; set; }
        public List<SortItem> SortItems { get; set; }
        public List<FilterItem> FilterItems { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
