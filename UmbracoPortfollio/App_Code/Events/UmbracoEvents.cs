using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using UmbracoPortfollio.App_Code;
using UmbracoPortfollio.App_Code.Helpers;

namespace UmbracoPortfollio.App_Code
{
    public class UmbracoEvents : ApplicationEventHandler
    {

        private static object _lockObj = new object();
        private static bool _ran = false;
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += ContentServicePublished;
            ContentService.Created += ContentServiceCreate;
            //Umbraco App has started
            if (!_ran)
            {
                lock (_lockObj)
                {
                    if (!_ran)
                    {
                        var helper = GlobalHelpers.GetDatabaseSchemeInstance();
                        //Check if the DB table does NOT exist
                        if (!helper.TableExist("ContactFormLogs"))
                        {
                            //Create DB table - and set overwrite to false
                            helper.CreateTable<ContactForm>(false);
                        }
                        if (!helper.TableExist("Comments"))
                        {
                            //Create DB table - and set overwrite to false
                            helper.CreateTable<CommentModel>(false);
                        }
                        _ran = true;
                    }
                }
            }
        }

        private static void MapRoutes()
        {
            ////Create our custom MVC route for our member profile pages
            //RouteTable.Routes.MapRoute(
            //    "memberProfileRoute",
            //    "user/{profileURLtoCheck}",
            //    new
            //    {
            //        controller = "ViewProfile",
            //        action = "Index"
            //    });
        }

        public void ContentServicePublished(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {
            var memberService = ApplicationContext.Current.Services.MemberService;
            foreach (var node in args.PublishedEntities)
            {
                if (node.Id <= 0) //new record
                {
                    var members = memberService.GetAllMembers();
                    foreach (var member in members)
                    {
                        GlobalHelpers.SendGridEmailMessage("no-reply@biggsy150.co.uk", member.Email, "Blog Post Published", string.Format("A new Blog Post was created called {0}, fancy having a look at <a href='http://www.biggsy150.co.uk/blog/' >{1}</a>.", node.Name, node.CreateDate));
                    }                    
                }
            }            
        }
        //public void PublishTweetBlog(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        //{
        //    foreach (var node in args.PublishedEntities)
        //    {
                
        //        if (node) //new record
        //        {
        //            SocialMediaHelpers.PostTweet()
        //        }
        //    }
        //}
        public void ContentServiceCreate(IContentService sender, NewEventArgs<IContent> args)
        {
            var memberService = ApplicationContext.Current.Services.MemberService;
            if(args.Alias == "blogPost")
            {
                
                if(args.Entity.GetValue("createTime") == null)
                {
                    var datetime = DateTime.Now;
                    args.Entity.SetValue("createTime",datetime);                    
                }
            }
        }
    }
}
