namespace TemplateAPI.Models
{
    public class SortItem
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; } = 0;
        public string OrderBy { get; set; }
    }
}