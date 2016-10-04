using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.IO;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using UmbracoPortfollio.App_Code;
using System.Threading.Tasks;

namespace UmbracoPortfollio.App_Code
{
    public class ContactController :  SurfaceController
        {

            /// <summary>
            /// Renders the Contact Form
            /// @Html.Action("RenderContactForm","ContactSurface")
            /// </summary>
            /// <returns></returns>
            public ActionResult RenderContactForm()
            {
                return PartialView("ContactForm", new ContactModel());
            }

            /// <summary>
            /// Sends the Contact Form            
            /// </summary>
            /// <returns></returns>
            [HttpPost]
            public ActionResult SendContactForm(ContactModel model)
            {
                if (!ModelState.IsValid)
                    return CurrentUmbracoPage();

                var um = new UmbracoHelper(UmbracoContext);
                var email = um.TypedContentAtXPath("//emailTemplate[@nodeName= 'Contact Form']").FirstOrDefault();

                // process the form
                var emailFrom = email.HasValue("emailFrom") ? email.GetPropertyValue("emailFrom").ToString() : "no-reply@biggsy150.co.uk";
                var emailTo = email.GetPropertyValue("emailTo").ToString();
                var emailSubject = email.GetPropertyValue("emailSubject").ToString();
                var emailBody = email.GetPropertyValue("emailBody").ToString();                

                if (!string.IsNullOrEmpty(emailTo) && !string.IsNullOrEmpty(emailSubject))
                {
                    // process the email body text
                    emailBody = ReplaceShortcodes(emailBody, model);
                    // send the email
                    GlobalHelpers.SendGridEmailMessage(emailFrom, emailTo, emailSubject, emailBody);
                    // store data locally
                    LogContactForm(CurrentPage.Id, Request.Form, model);
                }
                return Redirect(CurrentPage.Url + "?=success");
            }

            private string ReplaceShortcodes(string content, ContactModel model)
            {
                content = content.Replace("{Name}", model.Name);                
                content = content.Replace("{Email}", model.Email);
                content = content.Replace("{Comment}", model.Comment);
                return content;
            }

            private void LogContactForm(int nodeId, NameValueCollection form, ContactModel model)
            {
                try
                {                    
                    DateTime date = DateTime.Now;

                    var db = ApplicationContext.DatabaseContext.Database;
                    ContactForm c = new ContactForm
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Date = DateTime.Now,
                        Message = model.Comment
                    };

                    var helper = GlobalHelpers.GetDatabaseSchemeInstance();

                    if (helper.TableExist("ContactFormLogs"))
                    {
                        db.Insert(c);
                    }
                    //var contentService = Services.ContentService;
                    //contentService.

                }
                catch (Exception ex)
                {
                    LogHelper.Error<ContactController>("There was an error logging the contact form data.", ex);
                }
            }
        }
    }