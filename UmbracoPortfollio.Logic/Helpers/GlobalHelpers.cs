using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using log4net;
using System.Text.RegularExpressions;
using SendGrid;
using System.Web.Configuration;
using System.Security.Cryptography;

namespace UmbracoPortfollio.Logic.Helpers
{
    public static class GlobalHelpers
    {
        public static DatabaseSchemaHelper GetDatabaseSchemeInstance()
        {
            var logger = LoggerResolver.Current.Logger;
            var dbContext = ApplicationContext.Current.DatabaseContext;
            var helper = new DatabaseSchemaHelper(dbContext.Database, logger, dbContext.SqlSyntax);
            return helper;
        }
        public static UmbracoDatabase GetDatabase()
        {
          var db = UmbracoContext.Current.Application.DatabaseContext.Database;
          return db;
        }
        public static void SendEmailMessage(string from, string to, string subject, string body)
        {
            umbraco.library.SendMail(from, to, subject, body, true);
        }
        public static void SendGridEmailMessage(string from, string to, string subject, string body)
        {
            var sendgridUsername = WebConfigurationManager.AppSettings["sendgridUsername"];
            var sendgridPassword = WebConfigurationManager.AppSettings["sendgridPassword"];
            var emailMessage = new SendGridMessage();
            emailMessage.From = new System.Net.Mail.MailAddress(from,"Biggsy150.co.uk");
            emailMessage.AddTo(to);
            emailMessage.Subject = subject;
            emailMessage.Text = body;
            emailMessage.EnableTemplateEngine("468f9aba-a5aa-4596-9c05-2fd94f670cea");

            var credentials = new System.Net.NetworkCredential(sendgridUsername, sendgridPassword);
            var transportWeb = new Web(credentials);                
            transportWeb.DeliverAsync(emailMessage);
        }
        public static string ReplaceQueryStringItem(this string url, string key, string value)
        {
            string str;
            bool flag = url.Contains("?");
            if (flag)
            {
                flag = url.Contains(string.Concat(key, "="));
                str = (flag ? Regex.Replace(url, string.Concat("([?&]", key, ")=[^?&]+"), string.Concat("$1=", value)) : string.Format("{0}&{1}={2}", url, key, value));
            }
            else
            {
                str = string.Format("{0}?{1}={2}", url, key, value);
            }
            return str;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}