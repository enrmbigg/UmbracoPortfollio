using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using UmbracoValidationAttributes;

namespace UmbracoPortfollio.App_Code
{
    [TableName("Comments")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class CommentModel
    {
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("NodeId")]
        public DateTime NodeId { get; set; }

        [Column("Name")]
        [UmbracoRequired("CommentForm.NameRequired", "Please enter your Name")]
        [UmbracoDisplayName("CommentForm.Name", "*Name")]
        public string Name { get; set; }

        [Column("Email")]
        [UmbracoRequired("CommentForm.EmailRequired", "Please enter your Email")]
        [UmbracoDisplayName("CommentForm.Email", "*Email")]
        [UmbracoEmail(ErrorMessageDictionaryKey = "CommentForm.InvalidEmail", DefaultText = "Invalid Email")]
        [UmbracoDataType(DataType.EmailAddress, "CommentForm.EmailAddressError", "EmailAddress incorrect")]
        public string Email { get; set; }

        [Column("Comment")]
        [UmbracoRequired("CommentForm.CommentRequired", "Please enter your Comment")]
        [UmbracoDisplayName("CommentForm.Comment", "Comment")]
        [UmbracoDataType(DataType.MultilineText, "CommentForm.CommentError", "Comment Needed")]
        public string Comment { get; set; }
    }
    
}
