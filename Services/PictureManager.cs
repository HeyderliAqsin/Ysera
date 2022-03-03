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
    public class PictureManager
    {
        private readonly JewelleryDbContext _context;

        public PictureManager(JewelleryDbContext context)
        {
            _context = context;
        }

        public void AddPicture(Picture picture)
        {
            _context.Add(picture);
            _context.SaveChanges();
        }
        public void RemovePicture(List<int> PicIds)
        {
            var oldPictures= GetProductIds(PicIds);
            _context.ProductPictures.RemoveRange(oldPictures);
            _context.SaveChanges();
        }
        public List<ProductPicture> GetProductIds(List<int> picIds)
        {
            return _context.ProductPictures.Include(c=>c.Picture).Where(c=>picIds.Contains(c.PictureId)).ToList();
        }
    }
}
