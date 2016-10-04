using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmbracoValidationAttributes;

namespace UmbracoPortfollio.App_Code
{
    public class ContactModel
    {
        [UmbracoRequired("ContactForm.NameRequired", "Please enter your Name")]
        [UmbracoDisplayName("ContactForm.Name", "*Name")]
        public string Name { get; set; }

        [UmbracoDataType(DataType.EmailAddress, "ContactForm.EmailAddressError", "EmailAddress incorrect")]
        [UmbracoRequired("ContactForm.EmailAddressRequired", "Please enter your EmailAddress")]
        [UmbracoEmail(ErrorMessageDictionaryKey = "ContactForm.InvalidEmail", DefaultText = "Invalid Email")]
        [UmbracoDisplayName("ContactForm.Email", "*Email")]
        public string Email { get; set; }

        [UmbracoRequired("ContactForm.CommentRequired", "Please enter your Comment")]
        [UmbracoDataType(DataType.MultilineText, "ContactForm.CommentError", "Comment Needed")]
        [UmbracoDisplayName("ContactForm.Comment", "*Comment")]
        public string Comment { get; set; }

    }
}