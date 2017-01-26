using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoPortfollio.Logic.Models.ViewModels
{
    public partial class BlogPostViewModel : MasterViewModel
    {
        public BlogPostViewModel(IPublishedContent content) : base(content) { }
        public DateTime ArticlePublishedDate { get { return Content.GetPropertyValue<DateTime>("createTime", Content.CreateDate); } }
        public string[] BlogTags { get { return Content.GetPropertyValue<string>("tag").Split(','); } }
        public string Description { get { return Content.GetPropertyValue<string>("pageIntro"); } }
        public string BodyContent { get { return Content.GetPropertyValue<string>("bodyContent");} }
        public string PageTitle { get { return Content.GetPropertyValue<string>("pageTitle"); } }
        public DateTime PostCreation { get { return Content.GetPropertyValue<DateTime>("createTime"); } }
        public HtmlString Markdown { get { return Content.GetPropertyValue<HtmlString>("markdown"); } }
    }
}