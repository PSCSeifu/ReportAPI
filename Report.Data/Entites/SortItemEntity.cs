using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.Data.Entites
{
    [Table("SortItem")]
    public class SortItemEntity
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
