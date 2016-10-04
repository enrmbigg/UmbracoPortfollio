using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace UrlPickerExtensions
{
    /// <summary>
    /// Summary description for UrlPicker
    /// </summary>
    public static class UrlPickerExtensions
    {	
        public static LinkInfo GetSingleUrlFromUrlPicker(UrlPicker link)
        {
            LinkInfo linkInfo = null;
            if (link != null && link.Url != null)
            {
                if (link.Type == UrlPicker.UrlPickerTypes.Media)
                { 
                    linkInfo = new LinkInfo();
                    linkInfo.LinkType = UrlPicker.UrlPickerTypes.Media;

                    linkInfo.LinkUmbracoNode = link.TypeData.Media;

                    linkInfo.LinkURL = link.Url;

                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"blank\"";
                        linkInfo.LinkIcon = "<i class=\"fa fa-external-link after\"></i>";
                    }

                    if (link.Meta.Title == String.Empty)
                    {
                        linkInfo.LinkCaption = link.TypeData.Media.Name;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }
                }
                else if (link.Type == UrlPicker.UrlPickerTypes.Content)
                {
                    linkInfo = new LinkInfo();

                    linkInfo.LinkUmbracoNode = link.TypeData.Content;
                    linkInfo.LinkType = UrlPicker.UrlPickerTypes.Content;

                    linkInfo.LinkURL = link.Url;

                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"blank\"";
                        linkInfo.LinkIcon = "<i class=\"fa fa-external-link after\"></i>";
                    }

                    if (link.Meta.Title == String.Empty)
                    {
                        linkInfo.LinkCaption = link.TypeData.Content.Name;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }

                    //Document types ending _AN should be linked to anchor position on page.
                    if (link.TypeData.Content.DocumentTypeAlias.IndexOf("_AN", StringComparison.Ordinal) != -1)
                    {
                        var pageComponentsNode = link.TypeData.Content.Parent;
                        var parentNode = pageComponentsNode.Parent;
                        string anchor = "#pos_" + link.TypeData.Content.Id.ToString();
                        linkInfo.LinkURL = parentNode.Url + anchor;
                    }


                }
                else if (link.Type == UrlPicker.UrlPickerTypes.Url)
                {
                    linkInfo = new LinkInfo
                    {
                        LinkUmbracoNode = null,
                        LinkType = UrlPicker.UrlPickerTypes.Url,
                        LinkURL = link.Url
                    };



                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"blank\"";
                        linkInfo.LinkIcon = "<i class=\"fa fa-external-link after\"></i>";
                    }

                    if (link.Meta.Title == string.Empty)
                    {
                        linkInfo.LinkCaption = link.Url;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }
                }
            }

            return linkInfo;
        }

        public static string GetSingleUrlFromJObject(JObject link, out string linkTitle, out string linkTarget, out string linkIcon, out IPublishedContent node)
        {
            string linkUrl = String.Empty;
            linkTitle = String.Empty;
            linkTarget = String.Empty;
            linkIcon = String.Empty;
            node = null;

            if (link.Value<bool>("isInternal"))
            {
                var umbracoHelper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);

                node = umbracoHelper.TypedContent(link.Value<int>("internal"));

                if (node != null)
                {
                    linkUrl = node.Url;

                    if (link.Value<bool>("newWindow"))
                    {
                        linkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkTarget = "target=\"blank\"";
                        linkIcon = "<i class=\"fa fa-external-link after\"></i>";
                    }

                    //Document types ending _AN should be linked to anchor position on page.
                    if (node.DocumentTypeAlias.IndexOf("_AN", StringComparison.Ordinal) != -1)
                    {
                        var pageComponentsNode = node.Parent;
                        var parentNode = pageComponentsNode.Parent;
                        string anchor = "#pos_" + node.Id.ToString();
                        linkUrl = parentNode.Url + anchor;
                    }
                }
            }
            else
            {
                linkUrl = link.Value<string>("link");

                if (link.Value<bool>("newWindow"))
                {
                    linkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                    linkTarget = "target=\"blank\"";
                    linkIcon = "<i class=\"fa fa-external-link after\"></i>";
                }
            }

            return linkUrl;
        }
    }

    public class UrlPicker
    {
        public UrlPicker.UrlPickerTypes Type { get; set; }

        public Meta Meta { get; set; }

        public TypeData TypeData { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public enum UrlPickerTypes
        {
            Url,
            Content,
            Media,
        }
    }
  
    public class TypeData
    {
        public string Url { get; set; }

        public int? ContentId { get; set; }

        public IPublishedContent Content { get; set; }

        public int? MediaId { get; set; }

        public IPublishedContent Media { get; set; }
    }
  
    public class Meta
    {
        public string Title { get; set; }

        public bool NewWindow { get; set; }
    }
  
    public class LinkInfo
    {
        public string LinkCaption { get; set; }

        public string LinkTitle { get; set; }

        public string LinkURL { get; set; }

        public string LinkTarget { get; set; }

        public string LinkIcon { get; set; }

        public UrlPicker.UrlPickerTypes LinkType { get; set; }

        public IPublishedContent LinkUmbracoNode { get; set; }

        public LinkInfo()
        {
            this.LinkCaption = string.Empty;
            this.LinkTitle = string.Empty;
            this.LinkURL = string.Empty;
            this.LinkTarget = string.Empty;
            this.LinkIcon = string.Empty;
            this.LinkUmbracoNode = null;
        }
    }
  
    [PropertyValueType(typeof (UrlPicker))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class UrlPickerValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("UrlPicker");
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
                return new UrlPicker();
            string input = source.ToString();
            if (!DetectIsJson(input))
                return input;
            try
            {
                UrlPicker urlPicker = JsonConvert.DeserializeObject<UrlPicker>(input);
                UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                if (urlPicker.TypeData.ContentId.HasValue)
                    urlPicker.TypeData.Content = umbracoHelper.TypedContent(urlPicker.TypeData.ContentId);
                if (urlPicker.TypeData.MediaId.HasValue)
                    urlPicker.TypeData.Media = umbracoHelper.TypedMedia(urlPicker.TypeData.MediaId);
                switch (urlPicker.Type)
                {
                    case UrlPicker.UrlPickerTypes.Content:
                        if (urlPicker.TypeData.Content != null)
                        {
                            urlPicker.Url = urlPicker.TypeData.Content.Url;
                            urlPicker.Name = string.IsNullOrWhiteSpace(urlPicker.Meta.Title) ? urlPicker.TypeData.Content.Name : urlPicker.Meta.Title;
                            break;
                        }
                        break;
                    case UrlPicker.UrlPickerTypes.Media:
                        if (urlPicker.TypeData.Media != null)
                        {
                            urlPicker.Url = urlPicker.TypeData.Media.Url;
                            urlPicker.Name = string.IsNullOrWhiteSpace(urlPicker.Meta.Title) ? urlPicker.TypeData.Media.Name : urlPicker.Meta.Title;
                            break;
                        }
                        break;
                    default:
                        urlPicker.Url = urlPicker.TypeData.Url;
                        urlPicker.Name = string.IsNullOrWhiteSpace(urlPicker.Meta.Title) ? urlPicker.TypeData.Url : urlPicker.Meta.Title;
                        break;
                }
                return (object) urlPicker;
            }
            catch (Exception ex)
            {
                LogHelper.Error<UrlPickerValueConverter>(ex.Message, ex);
                return (object) new UrlPicker();
            }
        }

        private static bool DetectIsJson(string input)
        {
            input = input.Trim();
            if (input.StartsWith("{") && input.EndsWith("}"))
                return true;
            if (input.StartsWith("["))
                return input.EndsWith("]");
            return false;
        }
    }
}