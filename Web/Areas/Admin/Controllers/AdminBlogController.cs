using DataAccess;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class AdminBlogController : Controller
    {

        private readonly JewelleryDbContext _context;
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHost;
        private readonly PictureManager _pictureManager;
        private readonly BlogManager _blogManager;
        private readonly BlogCategoryManager _blogCategoryManager;
        public AdminBlogController(JewelleryDbContext context,
            ProductManager productManager, CategoryManager categoryManager
            , PictureManager pictureManager, BlogManager blogManager, IWebHostEnvironment webHost, BlogCategoryManager blogCategoryManager)
        {
            _context = context;
            _productManager = productManager;
            _categoryManager = categoryManager;
            _pictureManager = pictureManager;
            _blogManager = blogManager;
            _webHost = webHost;
            _blogCategoryManager = blogCategoryManager;
        }


        public IActionResult Index()
        {
            return View(_blogManager.GetAll());
        }

        public IActionResult Create()
        {
            ViewBag.categoryList=_blogCategoryManager.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,BlogPhoto,PublishDate,BlogCategoryId,Id")] Blog blog, IFormFile BlogPhoto)
        {

            if (ModelState.IsValid)
            {
                if(BlogPhoto != null)
                {
                    string fileName = Guid.NewGuid() + BlogPhoto.FileName;
                    string rootFile = Path.Combine(_webHost.WebRootPath, "uploads");
                    string mainFile = Path.Combine(rootFile, fileName);
                    using FileStream stream = new(mainFile, FileMode.Create);
                    BlogPhoto.CopyTo(stream);
                    blog.BlogPhoto = "/uploads/" + fileName;
                    blog.PublishDate = DateTime.Now;
                  
                }
                _blogManager.Add(blog);
                return RedirectToAction(nameof(Index));

            }
            ViewBag.categoryList = _blogCategoryManager.GetAll();


            return View(blog);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _blogManager.GetbyId(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["BlogCategoryId"] = new SelectList(_blogManager.GetAll(), "Id", "Name");
            return View(blog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,Description,BlogPhoto,PublishDate,BlogCategoryId,Id")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogManager.Update(blog);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blogManager.BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public IActionResult Delete(int? id)
        {
            ViewBag.CategoryList = _categoryManager.GetAll();

            if (id == null) return NotFound();
            var selectedProduct = _productManager.GetbyId(id);
            if (selectedProduct == null) return NotFound();

            return View(selectedProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            ViewBag.CategoryList = _categoryManager.GetAll();

            var selectedProduct = _productManager.GetbyId(id);
            if (selectedProduct == null) return NotFound();
            _productManager.Delete(selectedProduct);
            return RedirectToAction("Index");
        }

    }
}