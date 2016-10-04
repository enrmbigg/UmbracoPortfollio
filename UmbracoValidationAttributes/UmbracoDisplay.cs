using System;
using System.ComponentModel;

namespace UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UmbracoDisplayName : DisplayNameAttribute
    {
        private readonly string _dictionaryKey;
        private readonly string _defaultText;

        // This is a positional argument
        public UmbracoDisplayName(string dictionaryKey,  string defaultText)
        {
            _dictionaryKey = dictionaryKey;
            _defaultText = defaultText;
        }

        public override string DisplayName
        {
            get
            {
                return UmbracoValidationHelper.GetDictionaryItem(_dictionaryKey, _defaultText);
            }
        }
    }
}
