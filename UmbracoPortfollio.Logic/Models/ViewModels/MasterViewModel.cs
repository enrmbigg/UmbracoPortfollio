using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using UmbracoPortfollio.Logic.Models.ViewModels;

namespace UmbracoPortfollio.Logic.Models.ViewModels
{
    public class MasterViewModel : RenderModel
    {
        internal UmbracoHelper Umbraco = new UmbracoHelper(UmbracoContext.Current);

        public MasterViewModel(IPublishedContent content) : base(content) { }
        public string PageTitle { get { return Content.GetPropertyValue<string>("pagetitle"); } }
        public string PageIntro { get { return Content.GetPropertyValue<string>("pageIntro"); } }
        public Image PageImage { get { return new Image(Umbraco.TypedMedia(Content.GetPropertyValue<string>("pageImage"))); } }
        public HeaderViewModel Header { get { return new HeaderViewModel(Content.GetPropertyValue<IPublishedContent>("header")); } }
        public HtmlString BodyContent { get { return Content.GetPropertyValue<HtmlString>("bodyContent"); } }
        public string MetaTitle { get { return Content.GetPropertyValue<string>("metaTitle"); } }
        public string[] MetaKeywords { get { return Content.GetPropertyValue<string>("metaKeywords").Split(); } }
        public string MetaDescription { get { return Content.GetPropertyValue<string>("metaDescription"); } }
        public string MetaRobots { get { return Content.GetPropertyValue<string>("metaRobots"); } }
        public string SocialTitle { get { return Content.GetPropertyValue<string>("openGraphTitle"); } }
        public string SocialDescription { get { return Content.GetPropertyValue<string>("openGraphDescription"); } }
        public Image SocialImage { get { return new Image(Umbraco.TypedMedia(Content.GetPropertyValue<string>("openGraphDefaultImage"))); } }
        public bool HideFromSitemap { get { return Content.GetPropertyValue<bool>("hideFromSitemap"); } }
    }
}