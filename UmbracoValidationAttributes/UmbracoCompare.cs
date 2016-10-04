using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    public class UmbracoCompare : System.Web.Mvc.CompareAttribute, IClientValidatable
    {
        private readonly string _otherProperty;
        private readonly string _errorMessageDictionaryKey;
        private readonly string _defaultText;


        public UmbracoCompare(string errorMessageDictionaryKey, string defaultText, string otherProperty)
            : base(otherProperty)
        {
            _otherProperty = otherProperty;
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey,_defaultText);

            var error = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationEqualToRule(error, _otherProperty);

            yield return rule;
        }
    }
}
