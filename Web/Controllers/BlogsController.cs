#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services;
using Web.ViewModels;
using DataAccess;

namespace Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly JewelleryDbContext _context;
        private readonly BlogManager _blogManager;

        public BlogsController(BlogManager blogManager, JewelleryDbContext context)
        {
            _blogManager = blogManager;
            _context = context;
        }



        // GET: Blogs
        public  IActionResult Index()
        {
            var blogList = _blogManager.GetAll();
      
            return View(blogList);
        }
        public List<Blog> FindBlogbyId(int? categoryid, int? blogID)
        {
            var blogList = _context.Blogs.Where(x => x.Id != blogID && x.BlogCategoryId == categoryid).ToList();
            return blogList;
        }
        // GET: Blogs/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogID = _blogManager.GetbyId(id);


            if (blogID == null)
            {
                return NotFound();
            }
            BlogVM blogvm = new()
            {
                BlogSingle = blogID,
                SameBlogs=FindBlogbyId(blogID.BlogCategoryId,id)
            };

            return View(blogvm);
        }

      

        // GET: Blogs/Create
        public IActionResult Create()
        {
            ViewData["BlogCategoryId"] = new SelectList(_blogManager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Blogs/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Name,Description,BlogPhoto,PublishDate,BlogCategoryId,Id")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogManager.Add(blog);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogCategoryId"] = new SelectList(_blogManager.GetAll(), "Id", "Name",blog.BlogCategoryId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog =_blogManager.GetbyId(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["BlogCategoryId"] = new SelectList(_blogManager.GetAll(),"Id", "Name");
            return View(blog);
        }

        // POST: Blogs/Edit/5
 
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

        // GET: Blogs/Delete/5
        public IActionResult Delete(int? id)
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

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = _blogManager.GetbyId(id);
            blog.IsDeleted = true;
            return RedirectToAction(nameof(Index));
        }

    
    }
}
