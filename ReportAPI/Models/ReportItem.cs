namespace TemplateAPI.Models
{
    public class ReportItem
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Table { get; set; }
        public string Type { get; set; }
        public bool IsNullable { get; set; }
        public string Name { get; set; }
        public bool IsDisplayed { get; set; }
    }
}