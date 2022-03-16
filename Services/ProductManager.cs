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
    public class ProductManager
    {
        private readonly JewelleryDbContext _context;

        public ProductManager(JewelleryDbContext context)
        {
            _context = context;
        }

        public List<Product> SearchProducts(string q,int? categoryId)
        {
            var productList = _context.Products.Include(x=>x.Category).Include(x=>x.ProductPictures).AsQueryable();
            if(categoryId != null) productList=productList.Where(x=>x.CategoryId==categoryId);
            if (!string.IsNullOrWhiteSpace(q)) 
                productList = productList.Where(x => x.Name.Contains(q) || x.Category.Name.Contains(q));

            return productList.OrderByDescending(x=>x.ModifiedOn).ToList();
        }
        public void Add(Product newProduct)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
        }
        public void Update(Product newProduct)
        {
            _context.Update(newProduct);
            _context.SaveChanges();
        }

        public List<Product>? GetAll()
        {

            return _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => !x.IsDeleted).ToList();
        }
        public List<Product>? NewProducts(int count)
        {

            return _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.ModifiedOn).Take(count).ToList();
        }
        public List<Product>? WeekProducts(int count)
        {

            return _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => !x.IsDeleted && x.IsWeek)
                .OrderByDescending(x => x.ModifiedOn).Take(count).ToList();
        }
        public List<Product>? MonthProducts(int count)
        {

            return _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => !x.IsDeleted && x.IsMonth)
                .OrderByDescending(x => x.ModifiedOn).Take(count).ToList();
        }
        public Product? GetbyId(int? id)
        {
            var selectedProduct = _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return selectedProduct;
        }
        public List<Blog> GetBlogs()
        {
            return _context.Blogs.ToList();
        }
        public List<Product?> GetbyIds(IEnumerable<int> ids)
        {
            var selectedProducts = _context.Products.Include(x=>x.ProductPictures).ThenInclude(x=>x.Picture)
                .Where(p => ids.Contains(p.Id)).ToList();
            return selectedProducts;
        }
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.SaveChanges();
        }

        public bool ProductExists(object id)
        {
            throw new NotImplementedException();
        }
    }
}
