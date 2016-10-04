using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    public class UmbracoRange : RangeAttribute, IClientValidatable
    {
        /// <summary>
        /// 
        /// </summary>
        private string _errorMessageDictionaryKey { get; set; }
        private readonly string _defaultText;


        public UmbracoRange(string errorMessageDictionaryKey, string defaultText, Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }

        public UmbracoRange(string errorMessageDictionaryKey, string defaultText,double minimum, double maximum): base(minimum, maximum)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }

        public UmbracoRange(string errorMessageDictionaryKey, string defaultText, int minimum, int maximum)
            : base(minimum, maximum)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey, _defaultText);

            var error = UmbracoValidationHelper.FormatErrorMessage(metadata.DisplayName, _errorMessageDictionaryKey, _defaultText);
            var rule    = new ModelClientValidationRangeRule(error, Minimum, Maximum);

            yield return rule;
        }
    }
}
