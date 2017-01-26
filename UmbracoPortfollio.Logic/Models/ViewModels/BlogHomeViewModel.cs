using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using UmbracoPortfollio.Logic.Models.ViewModels;

namespace UmbracoPortfollio.Logic.Models.ViewModels
{
    public partial class BlogHomeViewModel :  MasterViewModel
    {
        public BlogHomeViewModel(IPublishedContent content) : base(content) { }
        public IEnumerable<BlogPostViewModel> BlogPosts { get { return Umbraco.TypedContentAtXPath("//blogPost").OrderByDescending(x => x.GetPropertyValue<DateTime>("createTime")).Select(x => new BlogPostViewModel(x)); } }          
        public IEnumerable<TagModel> Tags { get {  return Umbraco.TagQuery.GetAllContentTags().OrderByDescending(x => x.NodeCount); } }
        public IEnumerable<BlogPostViewModel> PagedBlogPosts(int page, string query, int pageSize = 10)
        {
            var posts = query == null ?
                BlogPosts
                : Umbraco.TagQuery.GetContentByTag(query).OrderByDescending(x => x.GetPropertyValue<DateTime>("createTime")).Select(x => new BlogPostViewModel(x));
            var resultsToSkip = page > 0 ? (page - 1) * pageSize : 0;
            var resultSet = posts.Skip(resultsToSkip)
                               .Take(pageSize);
            return resultSet;
        }
    }
}