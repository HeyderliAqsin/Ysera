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
    public class BlogManager
    {
        private readonly JewelleryDbContext _context;

        public BlogManager(JewelleryDbContext context)
        {
            _context = context;
        }
        public void Add(Blog newBlog)
        {
            _context.Add(newBlog);
            _context.SaveChanges();
        }
        public void Update(Blog newBlog)
        {
            _context.Update(newBlog);
            _context.SaveChanges();
        }
        public List<Blog>? GetAll()
        {

            return _context.Blogs.Include(x => x.BlogCategory).ToList();
        }
        public Blog? GetbyId(int? id)
        {
            var selectedBlog = _context.Blogs.Include(x => x.BlogCategory).FirstOrDefault(x => x.Id == id );
            return selectedBlog;
        }
        public void Delete(Blog blog)
        {
            blog.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
