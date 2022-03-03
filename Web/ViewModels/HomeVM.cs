using Entities;

namespace Web.ViewModels
{
    public class HomeVM
    {
        public List<Product>? SliderList { get; set; }
        public List<Category>? CategoryList { get; set; }
        public List<Product>? NewProducts { get; set; }
        public List<Blog>? Blogs { get; set; }

    }
}
