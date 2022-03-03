using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderManager
    {
        private readonly JewelleryDbContext _context;

        public OrderManager(JewelleryDbContext context)
        {
            _context = context;
        }

        public List<Order> GetOrder()
        {
            return _context.Orders.ToList();
        }
        public void Add(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
        }
    }
}
