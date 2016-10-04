using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace UmbracoPortfollio.App_Code.Controllers
{
    public class XmlSitemapSurfaceController : SurfaceController
    {
        public ActionResult Index()
        {
            Response.ContentType = "text/xml";
            return PartialView("XmlSitemap", CurrentPage);
        }
    }
}