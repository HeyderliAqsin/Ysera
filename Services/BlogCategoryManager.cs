using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlogCategoryManager
    {
        private readonly JewelleryDbContext _context;

        public BlogCategoryManager(JewelleryDbContext context)
        {
            _context = context;
        }
        public void Add(BlogCategory newblogCategory)
        {
            _context.Add(newblogCategory);
            _context.SaveChanges();
        }
        public void Update(BlogCategory newblogCategory)
        {
            _context.Update(newblogCategory);
            _context.SaveChanges();
        }
        public List<BlogCategory> GetAll()
        {
            return _context.BlogCategories.Where(x => !x.IsDeleted).ToList();
        }
        public BlogCategory? GetbyId(int? id)
        {
            var selectedBlogCategory = _context.BlogCategories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (selectedBlogCategory == null) return null;
            return selectedBlogCategory;
        }
        public void Delete(BlogCategory blogCategory)
        {

            blogCategory.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool BlogCategoriesExists(int id)
        {
            return _context.BlogCategories.Any(e => e.Id == id);
        }

    }
}
