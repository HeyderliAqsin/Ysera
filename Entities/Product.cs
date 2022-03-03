using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product:BaseEntity
    {
        //[Required(ErrorMessage="Mehsul adi bos ola bilmez")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public decimal? Discount { get; set; }
        public bool IsComplect { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? CoverPhotoId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSlider { get; set; }
        public bool IsWeek { get; set; }
        public bool IsMonth { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        [Display(Name ="Product Photo")]
        public virtual List<ProductPicture>? ProductPictures { get; set; }
    }
}
