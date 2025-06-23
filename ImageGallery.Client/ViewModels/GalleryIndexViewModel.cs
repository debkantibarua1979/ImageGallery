using ImageGallery.IDP.Client.Controllers;
using System.Collections.Generic;
using ImageGallery.IDP.Model;

namespace ImageGallery.IDP.Client.ViewModels
{
    public class GalleryIndexViewModel
    {
        public IEnumerable<Image> Images { get; private set; }
            = new List<Image>();

        public GalleryIndexViewModel(IEnumerable<Image> images)
        {
           Images = images;
        }
    }
}
