using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoRequired : RequiredAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;
        private readonly string _defaultText;


        public UmbracoRequired(string errorMessageDictionaryKey, string defaultText)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem( _errorMessageDictionaryKey, _defaultText);

            var error = UmbracoValidationHelper.FormatErrorMessage(metadata.DisplayName, _errorMessageDictionaryKey, _defaultText); ;
            var rule    = new ModelClientValidationRequiredRule(error);

            yield return rule;
        }
    }
}
