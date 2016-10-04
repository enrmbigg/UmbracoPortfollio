//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.4.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace Umbraco.Web.PublishedContentModels
{
	/// <summary>Email Template</summary>
	[PublishedContentModel("emailTemplate")]
	public partial class EmailTemplate : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "emailTemplate";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public EmailTemplate(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<EmailTemplate, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Email Body
		///</summary>
		[ImplementPropertyType("emailBody")]
		public string EmailBody
		{
			get { return this.GetPropertyValue<string>("emailBody"); }
		}

		///<summary>
		/// Email From
		///</summary>
		[ImplementPropertyType("emailFrom")]
		public string EmailFrom
		{
			get { return this.GetPropertyValue<string>("emailFrom"); }
		}

		///<summary>
		/// Email Subject
		///</summary>
		[ImplementPropertyType("emailSubject")]
		public string EmailSubject
		{
			get { return this.GetPropertyValue<string>("emailSubject"); }
		}

		///<summary>
		/// Email To
		///</summary>
		[ImplementPropertyType("emailTo")]
		public string EmailTo
		{
			get { return this.GetPropertyValue<string>("emailTo"); }
		}
	}
}
