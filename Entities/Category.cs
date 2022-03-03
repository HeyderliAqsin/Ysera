using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(150)]
        [Display(Name ="Category Name")]
         public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Category Icon")]
        public string IconUrl { get; set; }
    }

}
