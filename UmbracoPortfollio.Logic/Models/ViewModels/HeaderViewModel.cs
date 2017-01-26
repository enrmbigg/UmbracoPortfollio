using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using UrlPickerExtensions;

namespace UmbracoPortfollio.Logic.Models.ViewModels
{
    public class HeaderViewModel : RenderModel
    {
        public HeaderViewModel(IPublishedContent content) : base(content) { }
        public Image Image { get { return Content.GetHeaderImage(); }  }
        public UrlPicker Link { get { return Content.GetPropertyValue("link",new UrlPicker()); } }
        public string Title { get { return Content.GetPropertyValue<string>("title",""); } }
        public string Description { get { return Content.GetPropertyValue<string>("description",""); } }
        public string Colour { get { return Content.GetPropertyValue<string>("textColour", "00000"); } }
    }
}