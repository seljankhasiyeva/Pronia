using System.ComponentModel.DataAnnotations;
using Pronia1.Models.Base;

namespace Pronia1.Models
{
    public class Category:BaseEntity
    {
        [MaxLength(30, ErrorMessage ="Name cannot contain more than 30 symbols")]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
