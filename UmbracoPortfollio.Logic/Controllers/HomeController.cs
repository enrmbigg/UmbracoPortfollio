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
    public class HomeController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var homeModel = new HomeViewModel(model.Content);                       
            return CurrentTemplate(homeModel);
        }
    }
}