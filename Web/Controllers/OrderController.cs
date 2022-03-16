using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly UserManager<YseraUser> _userManager;
        private readonly OrderManager _orderManager;

        public OrderController(ProductManager productManager, UserManager<YseraUser> userManager, OrderManager orderManager)
        {
            _productManager = productManager;
            _userManager = userManager;
            _orderManager = orderManager;   
        }

        public async Task<IActionResult> Checkout()
        {
            var productIdList = Request.Cookies["cartItem"];
            List<Product> productList = null;

            CheckOutVM vm = new();
            if (productIdList != null && productIdList != "")
            {
                List<int> productIds = productIdList.Split('-').Select(x => int.Parse(x)).ToList();
                productList = _productManager.GetbyIds(productIds.Distinct());
                vm.ProductIds = productIds;
                vm.Products = productList;
                    var selectedUser = await _userManager.GetUserAsync(User);
                    if (selectedUser != null)
                    {
                        vm.CustomerId = selectedUser.Id;
                        vm.CustomerPhone = selectedUser.PhoneNumber;
                        vm.Customeremail = selectedUser.Email;
                        vm.CustomerAddress = selectedUser.Address;
                        vm.CustomerFirstname = selectedUser.Firstname;
                        vm.CustomerLastname = selectedUser.Lastname;

                    }
       
                return View(vm);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(CheckOutVM checkOut)
        {
            Order newOrder = new();
            var productIdList = Request.Cookies["cartItem"];
            List<Product> productList = null;
            if (productIdList != null && productIdList != "")
            {

                List<int> productIds = productIdList.Split('-').Select(x => int.Parse(x)).ToList();
                productList = _productManager.GetbyIds(productIds.Distinct());
                var selectedUser = await _userManager.GetUserAsync(User);

                newOrder.CustomerPhone = selectedUser.PhoneNumber;
                newOrder.Customeremail = selectedUser.Email;
                newOrder.CustomerId = selectedUser.Id;
                newOrder.OrderCode = Guid.NewGuid().ToString();
                newOrder.PlacedOn = DateTime.Now;
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(productList.Select(c => new OrderItem()
                {
                    ProductId = c.Id,
                    Quantity = (ushort)productIds.Where(p => p == c.Id).Count(),
                    Id=newOrder.Id,
                    OrderPrice=c.Price,
                   
                }));
                newOrder.TotalAmount = newOrder.OrderItems.Select(c => c.Quantity * c.OrderPrice).Sum();


            }
            _orderManager.Add(newOrder);
            Response.Cookies.Delete("cartItem");
              return RedirectToAction("Index","Home");
        }
    }
}
