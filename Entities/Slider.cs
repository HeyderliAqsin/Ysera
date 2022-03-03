using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Slider:BaseEntity
    {
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Header { get; set; }
        public decimal? Price { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(700)]
        public string PhotoUrl { get; set; }

    }
}
