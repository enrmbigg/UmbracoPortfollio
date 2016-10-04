using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoPortfollio.App_Code
{
    public static class HtmlHelpers
    {
        public static string CalculateAge(DateTime BirthDate)
        {
            int YearsPassed = DateTime.Now.Year - BirthDate.Year;
            // Are we before the birth date this year? If so subtract one year from the mix
            if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
            {
                YearsPassed--;
            }
            if (YearsPassed > 0)
            {
                return string.Format("{0} year old", YearsPassed);
            }
            else
            {
                BirthDate.AddYears(1);
                var MonthsPassed = GetMonthsBetween(DateTime.Now, BirthDate);
                return string.Format("{0} months old", MonthsPassed);
            }
        }
        public static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }
        public static string SafeEncodeUrlSegments(this string urlPath)
        {
            if (urlPath.InvariantStartsWith("http://") || urlPath.InvariantStartsWith("https://"))
            {
                if (Uri.IsWellFormedUriString(urlPath, UriKind.Absolute))
                {
                    Uri url;
                    if (Uri.TryCreate(urlPath, UriKind.Absolute, out url))
                    {
                        var pathToEncode = url.AbsolutePath;
                        return url.GetLeftPart(UriPartial.Authority).EnsureEndsWith('/') + EncodePath(pathToEncode) + url.Query;
                    }
                }
            }

            return EncodePath(urlPath);

        }
        private static string EncodePath(string urlPath)
        {
            return string.Join("/",
                urlPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => HttpUtility.UrlEncode(x).Replace("+", "%20"))
                    .WhereNotNull()
                //we are not supporting dots in our URLs it's just too difficult to
                // support across the board with all the different config options
                    .Select(x => x.Replace('.', '-')));
        }

        public static bool IsAmp(this IPublishedContent content)
        {
            if(content.HasValue("isAmpPage") || content.DocumentTypeAlias == "blogPost")
            {
                return true;
            }
            return false;
        }
        public static string AmpUrl(this IPublishedContent content)
        {
            if(content.DocumentTypeAlias == "blogPost")
            {
                var url = content.UrlWithDomain();
                var ampUrl = url.Replace("blog/", "blog/amp/");
                return ampUrl;
            }
            else
            {
                return "";
            }
        }

        public static IPublishedContent GetHeaderImage(this IPublishedContent content)
        {
            var umbraco = new UmbracoHelper(UmbracoContext.Current);
            var header = content.GetPropertyValue<IPublishedContent>("header");
            if (header != null && header.HasValue("image"))
            {
                var imageId = header.GetPropertyValue("image");
                var image = umbraco.TypedMedia(imageId);
                return image;

            }
            else if(content.HasValue("pageImage"))
            {
                var imageId = content.GetPropertyValue("pageImage");
                var image = umbraco.TypedMedia(imageId);
                return image;
            }
            else
            {
                return null;
            }
        }        
    }
}