using System.ComponentModel.DataAnnotations;
using Pronia1.Models.Base;

namespace Pronia1.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name cannot contain more than 30 symbols")]
        public string Name { get; set; } = null!;

        public List<Product>? Products { get; set; }
    }
}
