using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoDataType : DataTypeAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;
        private readonly string _defaultText;


        public UmbracoDataType(DataType type,string errorMessageDictionaryKey, string defaultText)
            : base(type)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
            _defaultText = defaultText;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey, _defaultText);

            // Kodus to "Chad" http://stackoverflow.com/a/9914117
            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = this.DataType.ToString().ToLower()
            };
        }
    }
}