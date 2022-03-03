using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ProductPicture : BaseEntity
    {
        public int ProductId { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }


    }

}
