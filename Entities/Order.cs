using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order:BaseEntity
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Customeremail { get; set; }
        public string CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? discount { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime PlacedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
