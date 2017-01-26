using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace UmbracoPortfollio.Logic.Models.ViewModels
{
    public partial class SitemapViewModel :  MasterViewModel
    {
        public SitemapViewModel(IPublishedContent content) : base(content) { }  
        //Go to The home node and get all decendants with 
        public MasterViewModel Home { get { return new MasterViewModel(Content.AncestorOrSelf(1)); } }    
    }
}