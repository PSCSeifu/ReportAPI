using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.Data.Entites
{
    [Table("Template")]
    public  class TemplateEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OutputFormat { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
