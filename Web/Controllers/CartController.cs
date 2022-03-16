using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductManager _productManager;

        public CartController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
                var productIdList = Request.Cookies["cartItem"];
                List<Product> productList = null;

                CartVM vm = new();
                if (productIdList != null && productIdList != "")
                {
                    List<int> productIds = productIdList.Split('-').Select(x => int.Parse(x)).ToList();
                    productList = _productManager.GetbyIds(productIds.Distinct());
                    vm.ProductIds = productIds;
                    vm.Products = productList;

                }
            return View(vm);
        }
    }
}
