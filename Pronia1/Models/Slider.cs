using System.ComponentModel.DataAnnotations.Schema;
using Pronia1.Models.Base;

namespace Pronia1.Models
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
