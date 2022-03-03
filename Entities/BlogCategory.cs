using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BlogCategory:BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public string BlogCategoryIcon { get; set; }


    }
}
