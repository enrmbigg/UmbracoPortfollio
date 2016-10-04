using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    public class UmbracoRegularExpression : RegularExpressionAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;
        private readonly string _defaultText;


        public UmbracoRegularExpression(string errorMessageDictionaryKey, string defaultText, string pattern)
            : base(pattern)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey, _defaultText);

            var error = UmbracoValidationHelper.FormatErrorMessage(metadata.DisplayName, _errorMessageDictionaryKey, _defaultText);
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = error,
                ValidationType = "regex"
            };

            rule.ValidationParameters.Add("pattern", Pattern);

            yield return rule;
        }
    }
}