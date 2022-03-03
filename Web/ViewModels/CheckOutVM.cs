using Entities;

namespace Web.ViewModels
{
    public class CheckOutVM
    {
        public List<int> ProductIds { get; set; }
        public List<Product> Products { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerFirstname { get; set; }
        public string? CustomerLastname { get; set; }
        public string? Customeremail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
