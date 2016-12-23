using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoPortfollio.App_Code
{
    public class Image
    {
        public Image(IPublishedContent image)
        {
            if (image != null)
            {
                Name = image.Name;
                Url = image.Url;
                AltText = image.GetPropertyValue<string>("altText", Name);
                Node = image;
                Width = image.GetPropertyValue<int>("umbracoWidth");
                Height = image.GetPropertyValue<int>("umbracoHeight");
            }
        }
        public string Name { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
        public IPublishedContent Node { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}