using System;
using System.Collections.Generic;
using Pronia.Models;
namespace Pronia.ViewModels

{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
