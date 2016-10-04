using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace UmbracoPortfollio.App_Code
{
    public class CommentController : SurfaceController
        {

            /// <summary>
            /// Renders the Contact Form
            /// @Html.Action("RenderContactForm","ContactSurface")
            /// </summary>
            /// <returns></returns>
            public ActionResult RenderCommentForm()
            {
                return PartialView("CommentForm", new CommentModel());
            }

            /// <summary>
            /// Sends the Contact Form            
            /// </summary>
            /// <returns></returns>
            [HttpPost]
            public ActionResult SendCommentForm(CommentModel model)
            {
                if (!ModelState.IsValid)
                return CurrentUmbracoPage();
                model.Date = DateTime.Now;
                // store data
                LogCommentForm(CurrentPage.Id, model);                
                return RedirectToCurrentUmbracoPage();
            }
            
            private void LogCommentForm(int nodeId, CommentModel model)
            {
                try
                {                    
                    var db = ApplicationContext.DatabaseContext.Database;
                    var helper = GlobalHelpers.GetDatabaseSchemeInstance();

                    if (helper.TableExist("Comments"))
                    {
                        db.Insert(model);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error<ContactController>("There was an error logging the contact form data.", ex);
                }
            }
        }
    }    
