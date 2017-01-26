using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoPortfollio.Logic.Models;
using UmbracoPortfollio.Logic.Models.Custom;

namespace UmbracoPortfollio.Logic.Controllers
{
    public class SitemapController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var SitemapModel = new SitemapViewModel(model.Content);                       
            return CurrentTemplate(SitemapModel);
        }
    }
}