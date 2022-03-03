using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SliderManager _sliderManager;
        private readonly ProductManager _productManager;
        private readonly JewelleryDbContext _context;
        public HomeController(ILogger<HomeController> logger, SliderManager sliderManager, ProductManager productManager, JewelleryDbContext context)
        {
            _logger = logger;
            _sliderManager = sliderManager;
            _productManager = productManager;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new()
            {
                SliderList = _sliderManager.GetSliders(),
                NewProducts = _productManager.NewProducts(8),
                Blogs=_productManager.GetBlogs()

            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}