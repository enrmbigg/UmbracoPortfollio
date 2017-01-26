using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skybrud.Social;
using Skybrud.Social.Umbraco.Facebook.PropertyEditors.OAuth;
using Skybrud.Social.Facebook;
using Skybrud.Social.Facebook.Objects.Posts;
using Skybrud.Social.Facebook.Objects.Users;
using Skybrud.Social.Facebook.Options.Posts;
using Skybrud.Social.Facebook.Responses.Posts;
using Skybrud.Social.Umbraco.Facebook;
using Skybrud.Social.Umbraco.Facebook.PropertyEditors.OAuth;
using Skybrud.Social.Twitter;
using Skybrud.Social.Twitter.Objects;
using Skybrud.Social.Umbraco.Twitter.PropertyEditors.OAuth;
using Skybrud.Social.Umbraco.Instagram;
using Skybrud.Social.Umbraco.Instagram.PropertyEditors.OAuth;
using Skybrud.Social.Instagram;
using Skybrud.Social.Instagram.Responses;
using Skybrud.Social.Instagram.Objects;
using UmbracoPortfollio.Logic.Models;
using Skybrud.Social.Twitter.Responses;

namespace UmbracoPortfollio.Logic.Helpers
{
    public class SocialMediaHelpers
    {
        public static IEnumerable<FacebookPost> GetLastestFacebookPosts(object OAuthValue)
        {
            // Get the OAuth information stored in the property
            FacebookOAuthData facebook = OAuthValue as FacebookOAuthData;

            // Check whether the OAuth data is valid
            if (facebook != null && facebook.IsValid)
            {

                // Gets an instance of FacebookService based on the OAuth data
                FacebookService service = facebook.GetService();

                // Get information about the authenticated user
                FacebookUser me = service.Users.GetUser("me").Body;

                // Gets the most recent posts of the authenticated user (me)
                FacebookPostsResponse response = service.Posts.GetPosts(new FacebookGetPostsOptions
                {
                    Identifier = "me",
                    Fields = "id,name,message,story,created_time,picture,link,description,status_type,type,likes",
                    Limit = 25
                });

                return response.Body.Data.ToList();
            }
            return null;
        }

        public static IEnumerable<TwitterStatusMessage> GetLastestTwitterPosts(object OAuthValue)
        {
            // Check whether the OAuth data is valid
            TwitterOAuthData twitter = OAuthValue as TwitterOAuthData;

            // Check whether the OAuth data is valid
            if (twitter != null && twitter.IsValid) {

                // Gets an instance of TwitterService based on the OAuth data
                TwitterService service = twitter.GetService();

                // Get information about the authenticated user
                TwitterUser user = service.Users.GetUser(twitter.Id).Body;

                // Get recent status messages (tweets) from the authenticated user
                return service.Statuses.GetUserTimeline(user.Id).Body;

            }
            return null;
        }

        public static IEnumerable<InstagramMedia> GetLastestInstagramPosts(object OAuthValue)
        {
            // Check whether the OAuth data is valid
            InstagramOAuthData instagram = OAuthValue as InstagramOAuthData;

            // Check whether the OAuth data is valid
            if (instagram != null && instagram.IsValid)
            {

                // Gets an instance of TwitterService based on the OAuth data
                InstagramService service = instagram.GetService();
                var response = service.Users.GetRecentMedia();
                // Get recent status messages (tweets) from the authenticated user
                return response.Body.Data;
            }
            return null;
        }

        public static SocialImage ReturnSocialImage(InstagramMedia item)
        {
            SocialImage s = new SocialImage();
            s.Image = item.Images.StandardResolution.Url;
            s.Date = item.Created;
            s.Caption = item.CaptionText;
            s.SocialLink = item.Link;
            return s;
        }
        public static IEnumerable<SocialImage> ReturnSocialImage(IEnumerable<InstagramMedia> items)
        {
            List<SocialImage> list = new List<SocialImage>();
            foreach(var item in items)
            {
                list.Add(ReturnSocialImage(item));
            }

            return list;
        }

        public static SocialImage ReturnSocialImage(FacebookPost item)
        {
            SocialImage s = new SocialImage();
            s.Image = item.Picture;
            s.Date = item.CreatedTime;
            s.Caption = item.Description;
            s.SocialLink = item.Link;
            return s;
        }
        public static IEnumerable<SocialImage> ReturnSocialImage(IEnumerable<FacebookPost> items)
        {
            List<SocialImage> list = new List<SocialImage>();
            foreach (var item in items)
            {
                list.Add(ReturnSocialImage(item));
            }

            return list;
        }

        public static SocialImage ReturnSocialImage(TwitterStatusMessage item)
        {
            SocialImage s = new SocialImage();
            var imageMedia = item.Entities.Media != null? item.Entities.Media.FirstOrDefault(): null;
            var urlMedia = item.Entities.Urls != null ? item.Entities.Urls.FirstOrDefault() : null;
            var image = imageMedia != null ? imageMedia.MediaUrl: "";
            var url = imageMedia != null ? imageMedia.Url : "";
            var urlToRemove = url.Split(' ').Last();
            s.Image = image;
            s.Date = item.CreatedAt;
            s.Caption = !string.IsNullOrWhiteSpace(item.Text) && !string.IsNullOrWhiteSpace(urlToRemove) ? item.Text.Replace(urlToRemove, string.Empty) : "";
            s.SocialLink = url;
            return s;
        }
        public static IEnumerable<SocialImage> ReturnSocialImage(IEnumerable<TwitterStatusMessage> items)
        {
            List<SocialImage> list = new List<SocialImage>();
            foreach (var item in items)
            {
                list.Add(ReturnSocialImage(item));
            }

            return list;
        }

        public static IEnumerable<SocialImage> ReturnSocialImage(IEnumerable<TwitterStatusMessage> twitterItems, IEnumerable<FacebookPost> facebookItems, IEnumerable<InstagramMedia> instagramItems)
        {
            List<SocialImage> list = new List<SocialImage>();
            list.AddRange(ReturnSocialImage(twitterItems));
            list.AddRange(ReturnSocialImage(facebookItems));
            list.AddRange(ReturnSocialImage(instagramItems));            
            return list;
        }

        public static IEnumerable<SocialImage> ReturnSocialImage(IEnumerable<TwitterStatusMessage> twitterItems, IEnumerable<InstagramMedia> instagramItems)
        {
            List<SocialImage> list = new List<SocialImage>();
            list.AddRange(ReturnSocialImage(twitterItems));
            list.AddRange(ReturnSocialImage(instagramItems));
            return list;
        }
        public static TwitterStatusMessageResponse PostTweet(object OAuthValue,string tweet)
        {
            TwitterOAuthData twitter = OAuthValue as TwitterOAuthData;
            // Check whether the OAuth data is valid
            if (twitter != null && twitter.IsValid)
            {
                // Gets an instance of TwitterService based on the OAuth data
                TwitterService service = twitter.GetService();
                // Post tweet
                TwitterStatusMessageResponse response = service.Statuses.PostStatusMessage(tweet);
                // Return response if successful
                return response;
            }
            return null;
        }
    }
}