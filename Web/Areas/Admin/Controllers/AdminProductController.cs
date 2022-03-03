using DataAccess;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]

    public class AdminProductController : Controller
    {
        private readonly JewelleryDbContext _context;
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PictureManager _pictureManager;    

        public AdminProductController(JewelleryDbContext context, ProductManager productManager, IWebHostEnvironment webHostEnvironment, PictureManager pictureManager, CategoryManager categoryManager)
        {
            _context = context;
            _productManager = productManager;
            _webHostEnvironment = webHostEnvironment;
            _categoryManager = categoryManager;
            _pictureManager = pictureManager;

        }
        public IActionResult Index()
        {
            return View(_productManager.GetAll());
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryManager.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product,IFormFile[] PictureUrlss,string? removePictureIds,int? thumbnailPictureId)
        {
            ViewBag.CategoryList=_categoryManager.GetAll();
            if(ModelState.IsValid)
            {
                product.ProductPictures = new List<ProductPicture>();

                foreach (var PhotoUrl in PictureUrlss)
                {   
                    string fileName = Guid.NewGuid() + PhotoUrl.FileName;
                    var rootFile = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var photoLink = Path.Combine(rootFile, fileName);
                    using FileStream fileStream = new(photoLink, FileMode.Create);
                    PhotoUrl.CopyTo(fileStream);

                    Picture newPicture = new() { Url = "/uploads/" + fileName };
                    _pictureManager.AddPicture(newPicture);

                    product.ProductPictures.Add(
                        new ProductPicture() 
                        { PictureId=newPicture.Id,ProductId=product.Id});

                    //int picFirstId = product.ProductPictures.First().PictureId;
                        product.CoverPhotoId=product.ProductPictures != null ?
                        product.ProductPictures[thumbnailPictureId ?? 0].PictureId : null;

                    product.PublishDate = DateTime.Now;
                    _productManager.Add(product);
                    return RedirectToAction(nameof(Index));
                   
                 }
                product.CoverPhotoId = product.ProductPictures != null ? product.ProductPictures.First().PictureId : null;

                _productManager.Add(product);
                return RedirectToAction("Index");
                
            }
            return View(product);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var selectedProduct = _productManager.GetbyId(id);
            if (selectedProduct == null) NotFound();
            ViewBag.CategoryList = _categoryManager.GetAll();
            return View(selectedProduct);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product, IFormFile[] PictureUrlss,string oldPicture, string removePictureIds, int? thumbnailPictureId)
        {

            if (id != product.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {

                    List<int> removePicIds = removePictureIds.Split("-")
                        .Select(c => int.Parse(c)).ToList();

                    _pictureManager.RemovePicture(removePicIds);
                    List<int> oldPictureIds = oldPicture.Split("-")
                       .Select(c => int.Parse(c))
                       .Where(c => !removePicIds.Contains(c))
                       .ToList();

                    product.ProductPictures = new List<ProductPicture>();
                    product.ProductPictures = product.ProductPictures.Where(c => !removePicIds.Contains(c.PictureId)).ToList();

                    var oldPicturewithoutRemove = _pictureManager.GetProductIds(oldPictureIds);
                    product.ProductPictures = oldPicturewithoutRemove.Count>0? oldPicturewithoutRemove: new List<ProductPicture>();


                    foreach (var PhotoUrl in PictureUrlss)
                    {
                        string fileName = Guid.NewGuid() + PhotoUrl.FileName;
                        var rootFile = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        var photoLink = Path.Combine(rootFile, fileName);
                        using FileStream fileStream = new(photoLink, FileMode.Create);
                        PhotoUrl.CopyTo(fileStream);

                        Picture newPicture = new() { Url = "/uploads/" + fileName };
                        _pictureManager.AddPicture(newPicture);

                        product.ProductPictures.Add(
                            new ProductPicture()
                            { PictureId = newPicture.Id, ProductId = product.Id });

                        
                    }
                    int picFirstId = product.ProductPictures.First().PictureId;

                    product.CoverPhotoId = product.ProductPictures != null ?
                    product.ProductPictures[
                      thumbnailPictureId.HasValue ?
                      thumbnailPictureId.Value :
                      picFirstId].PictureId : null;

                    product.ModifiedOn = DateTime.Now;
                    _productManager.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productManager.ProductExists(product.Id))
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
            ViewBag.CategoryList = _categoryManager.GetAll();
            return View(product);
        
        }


        public IActionResult Details(int? id)
        {
            //ViewBag.CategoryList = _categoryManager.GetAll();

            if (id == null) return NotFound();
            var selecetedProduct = _productManager.GetbyId(id.Value);
            if (selecetedProduct == null) return NotFound();
            return View(selecetedProduct);

        }
        public IActionResult Delete(int? id)
        {

            if (id == null) return NotFound();
            var selectedProduct=_productManager.GetbyId(id);
            if(selectedProduct==null) return NotFound();
           
            return View(selectedProduct);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            var selectedProduct = _productManager.GetbyId(id); 
            if (selectedProduct == null) return NotFound();
            _productManager.Delete(selectedProduct);
            return RedirectToAction("Index");
        }
       
    }
}
