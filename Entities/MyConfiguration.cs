namespace Entities
{
    public class MyConfiguration : BaseEntity
    {
        public string Logo { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook  { get; set; }
        public string? Twitter { get; set; }
        public string DescriptionFooter { get; set; }
        public string TopGreeting { get; set; }
    }
}
