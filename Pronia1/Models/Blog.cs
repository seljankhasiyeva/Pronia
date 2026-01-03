using Pronia1.Models.Base;

namespace Pronia1.Models
{
    public class Blog : BaseEntity
    {
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
