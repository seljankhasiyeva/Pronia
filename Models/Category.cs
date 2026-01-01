using System.ComponentModel.DataAnnotations;
using Pronia.Models.Base;

namespace Pronia.Models
{
    public class Category:BaseEntity
    {
        [MaxLength(30, ErrorMessage = "adi 30-dan cox yazmaq olmaz")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
