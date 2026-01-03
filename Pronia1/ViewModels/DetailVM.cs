using Pronia1.Models;
namespace Pronia1.ViewModels
{
    public class DetailVM
    {
        public Models.Product Product { get; set; }
        public List<Models.Product> RelatedProducts { get; set; }
    }
}
