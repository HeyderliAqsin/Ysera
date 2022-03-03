using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SliderManager
    {
        private readonly JewelleryDbContext _context;
        private readonly ProductManager _productManager;

        public SliderManager(JewelleryDbContext context, ProductManager productManager)
        {
            _context = context;
            _productManager = productManager;
        }
        public List<Product>? GetSliders()
        {
            return _context.Products.Include(x=>x.ProductPictures).ThenInclude(x=>x.Picture).Where( x => x.IsSlider != null && !x.IsDeleted).OrderByDescending(x=>x.ModifiedOn).Take(4).ToList();
        }
    }
}
