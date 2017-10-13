namespace TemplateAPI.Models
{
    public class FilterItem
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; } = 0;
        public string Operation { get; set; }
        public string LowerLimit { get; set; }
        public string HigherLimit { get; set; }
        public string Value { get; set; }
    }
}