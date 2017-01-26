using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using UmbracoPortfollio.Logic.Models;

namespace UmbracoPortfollio.Logic.Helpers
{
    public static class BlogHelper
    {
        public static IHtmlString TagCloud(this HtmlHelper html, IEnumerable<TagModel> model, int maxWeight, int maxResults)
        {
            var tagsAndWeight = model.Select(x => new { tag = x.Text, weight = Math.Ceiling(GetTagWeight(x.NodeCount, maxWeight)) })
                .OrderByDescending(x => x.weight)
                .ToList();

            tagsAndWeight.Shuffle();                
            tagsAndWeight = tagsAndWeight.Take(maxResults).ToList();

            var ul = new TagBuilder("ul");
            ul.AddCssClass("tag-cloud");
            foreach (var tag in tagsAndWeight)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("tag-cloud-" + tag.weight);
                var a = new TagBuilder("a");
                a.MergeAttribute("title", tag.tag);
                a.MergeAttribute("href", "/blog/?q="+tag.tag);
                a.SetInnerText(tag.tag);
                li.InnerHtml = a.ToString();
                ul.InnerHtml += li.ToString();
            }
            return MvcHtmlString.Create(ul.ToString());
        }
        public static IHtmlString TagCloud(this HtmlHelper html, int maxWeight, int maxResults, string tagGroup = null)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            var model = helper.TagQuery.GetAllContentTags(tagGroup).ToArray();
            if (!model.Any())
            {
                return null;
            }
            var tagsAndWeight = model.Select(x => new { tag = x.Text, weight = Math.Ceiling(GetTagWeight(x.NodeCount, maxWeight)) })
                .OrderByDescending(x => x.weight)
                .Take(maxResults);

            var ul = new TagBuilder("ul");
            ul.AddCssClass("tag-cloud");
            foreach (var tag in tagsAndWeight)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("tag-cloud-" + tag.weight);
                var a = new TagBuilder("a");
                a.MergeAttribute("title", tag.tag);
                a.MergeAttribute("href", "/blog/?q=" + tag.tag);
                a.SetInnerText(tag.tag);
                li.InnerHtml = a.ToString();
                ul.InnerHtml += li.ToString();
            }
            return MvcHtmlString.Create(ul.ToString());
        }
        
        public static IEnumerable<TagModel> GetAllContentTags(this UmbracoHelper helper, string tagGroup = null)
        {
            var tags = helper.TagQuery.GetAllContentTags(tagGroup).ToArray();
            if (!tags.Any())
            {
                return Enumerable.Empty<TagModel>();
            }
            return tags;
        }

        private static double GetTagWeight(int postCount, int maxWeight)
        {
            return Convert.ToDouble(Math.Ceiling((double)postCount * maxWeight));
        }
    }
}