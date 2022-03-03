#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Web.Data;
using Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogCategoriesController : Controller
    {
        private readonly BlogCategoryManager _blogCategoryManager;

        public BlogCategoriesController(BlogCategoryManager blogCategoryManager)
        {
            _blogCategoryManager = blogCategoryManager;
        }



        // GET: Admin/BlogCategories
        public IActionResult Index()
        {
            return View( _blogCategoryManager.GetAll());
        }

        // GET: Admin/BlogCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _blogCategoryManager.GetbyId(id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Create
        public IActionResult Create()
        {
            ViewData["BlogCategoryId"] = new SelectList(_blogCategoryManager.GetAll(), "Id", "Name");

            return View();
        }

        // POST: Admin/BlogCategories/Create
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,BlogCategoryIcon,Id,IsDeleted")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                _blogCategoryManager.Add(blogCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _blogCategoryManager.GetbyId(id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,BlogCategoryIcon,Id,IsDeleted")] BlogCategory blogCategory)
        {
            if (id != blogCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogCategoryManager.Update(blogCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blogCategoryManager.BlogCategoriesExists(blogCategory.Id))
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
            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _blogCategoryManager.GetbyId(id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var blogCategory = _blogCategoryManager.GetbyId(id);
            blogCategory.IsDeleted = true;
            return RedirectToAction(nameof(Index));
        }

       
    }
}
