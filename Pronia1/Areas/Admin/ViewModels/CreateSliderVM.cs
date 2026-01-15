namespace Pronia1.Areas.Admin.ViewModels
{
    public class CreateSliderVM
    {
        public string Title { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
    }
}
