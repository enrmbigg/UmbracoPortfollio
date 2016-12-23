using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
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
        public static string DaysAgo(DateTime date)
        {
            DateTime startDate = date;

            // 2.
            // Get the current DateTime.
            DateTime now = DateTime.Now;

            // 3.
            // Get the TimeSpan of the difference.
            TimeSpan elapsed = now.Subtract(startDate);

            // 4.
            // Get number of days ago.
            double daysAgo = elapsed.TotalDays;
            if (daysAgo >= 1)
            {
                return string.Format("{0} days ago", daysAgo.ToString("0"));
            }
            else
            {
                return "Today";
            }
        }

        public static string GravatarImageUrl(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email.ToLower()));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var hash = sBuilder.ToString();  // Return the hexadecimal string. 
        
            return string.Format("https://secure.gravatar.com/avatar/{0}?", hash);
        }
        public static Image ReturnImage(this IPublishedContent node,string propertyName)
        {
            UmbracoHelper Umbraco = new UmbracoHelper(UmbracoContext.Current);
            var image = Umbraco.TypedMedia(node.GetPropertyValue(propertyName));
            return new Image(image);
        }
    }
}