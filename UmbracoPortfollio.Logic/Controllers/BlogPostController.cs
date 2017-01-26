﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoPortfollio.Logic.Models;
using UmbracoPortfollio.Logic.Models.Custom;
using UmbracoPortfollio.Logic.Models.ViewModels;

namespace UmbracoPortfollio.Logic.Controllers
{
    public class BlogPostController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var blogPostModel = new BlogPostViewModel(model.Content);                       
            return CurrentTemplate(blogPostModel);
        }
    }
}