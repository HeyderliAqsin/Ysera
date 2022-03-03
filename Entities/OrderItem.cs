using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{

    public class OrderItem:BaseEntity
    {
        [NotMapped]
        public decimal OrderPrice { get; set; }
        public ushort Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
