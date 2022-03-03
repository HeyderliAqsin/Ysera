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
    public class CategoryManager
    {
        private readonly JewelleryDbContext _context;

        public CategoryManager(JewelleryDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
        public List<Category> GetAll()
        {
            return _context.Categories.Where(x => !x.IsDeleted).ToList();
        }
        public Category? GetbyId(int? id)
        {
            var selectedCategory=_context.Categories.FirstOrDefault(x=>x.Id==id && !x.IsDeleted);
            if (selectedCategory == null) return null;
            return selectedCategory;
        }
        public void Delete(Category category)
        {
            category.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

       
    }
}
