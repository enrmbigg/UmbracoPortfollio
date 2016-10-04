using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    public class UmbracoStringLength : StringLengthAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;
        private readonly string _defaultText;


        public UmbracoStringLength( string errorMessageDictionaryKey, string defaultText, int maximumLength)
            : base(maximumLength)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem( _errorMessageDictionaryKey, _defaultText);

            var error = UmbracoValidationHelper.FormatErrorMessage(metadata.DisplayName, _errorMessageDictionaryKey, _defaultText); 
            var rule    = new ModelClientValidationStringLengthRule(error, MinimumLength, MaximumLength);

            yield return rule;
        }
    }
}
