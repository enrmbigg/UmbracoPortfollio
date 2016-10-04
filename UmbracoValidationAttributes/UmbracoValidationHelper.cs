using System;
using umbraco;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace UmbracoValidationAttributes
{
    public static class UmbracoValidationHelper
    {
        public static UmbracoHelper UmbracoHelper { get; private set; }
        private static readonly ILocalizationService Service = ApplicationContext.Current.Services.LocalizationService;
        static UmbracoValidationHelper()
        {
            //Ensure we have a context
            if (UmbracoContext.Current == null)
            {
                throw new Exception("We have no Umbraco context, are you sure you are running this in Umbraco?");
            }

            //Setup Umbraco Helper for our inheriting classes to use as needed
            UmbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public static string GetDictionaryItem( string errorMessageDictionaryKey, string defaultText)
        {
            //Get dictionary value for thge required error message
            var error = UmbracoHelper.GetDictionaryValue(errorMessageDictionaryKey);


            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(error))
            {
                LogHelper.Warn<DictionaryItem>(string.Format("The dictionary key '{0}' for the required error message is empty or does not exist", errorMessageDictionaryKey));
                return defaultText; //AddDictionaryItemIfNotExist(controllerName, errorMessageDictionaryKey, defaultText);
            }
            return error;
        }

        public static string FormatErrorMessage(string name, string errorMessageDictionaryKey, string defaultText)
        {
            //Get dictionary value for thge required error message
            var error = UmbracoHelper.GetDictionaryValue(errorMessageDictionaryKey);


            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(error))
            {
                LogHelper.Warn<DictionaryItem>(string.Format("The dictionary key '{0}' for the required error message is empty or does not exist", errorMessageDictionaryKey));
                return defaultText; //AddDictionaryItemIfNotExist(controllerName, errorMessageDictionaryKey, defaultText);
            }

            // String replacment the token wiht our localised propertyname
            // The {{Field}} field is required
            error = error.Replace("{{Field}}", name);

            //Return the value
            return error;
        }

        public static string FormatRangeErrorMessage(string name, string errorMessageDictionaryKey, string defaultText, object min, object max)
        {
            //Get dictionary value for thge required error message
            var error = UmbracoHelper.GetDictionaryValue(errorMessageDictionaryKey);


            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(error))
            {
                LogHelper.Warn<DictionaryItem>(string.Format("The dictionary key '{0}' for the required error message is empty or does not exist", errorMessageDictionaryKey));
                return defaultText; //AddDictionaryItemIfNotExist(controllerName, errorMessageDictionaryKey, defaultText);
            }

            //Convert object to int's
            var minVal = Convert.ToInt32(min);
            var maxVal = Convert.ToInt32(max);

            // String replacment the token wiht our localised propertyname
            // The field {{Field}} must be between {0} and {1}
            error = error.Replace("{{Field}}", name);
            error = string.Format(error, minVal, maxVal);

            //Return the value
            return error;
        }

        public static string FormatCompareErrorMessage(string name, string errorMessageDictionaryKey, string defaultText, string otherProperty)
        {
            //Get dictionary value for thge required error message
            var error = UmbracoHelper.GetDictionaryValue(errorMessageDictionaryKey);

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(error))
            {
                LogHelper.Warn<DictionaryItem>(string.Format("The dictionary key '{0}' for the required error message is empty or does not exist", errorMessageDictionaryKey));
                return defaultText; //AddDictionaryItemIfNotExist(controllerName, errorMessageDictionaryKey, defaultText);
            }

            //TODO - Somehow figure out
            //Get other property display name, but from UmbracoDisplay as getting C# property name
            

            // String replacment the token with our localised propertyname
            //'{{Field}}' and '{0}' do not match.
            error = error.Replace("{{Field}}", name);
            error = string.Format(error, otherProperty);

            //Return the value
            return error;
        }

        public static string FormatLengthErrorMessage(string name, string errorMessageDictionaryKey, string defaultText, int maxLength, int minLength)
        {
            //Get dictionary value for thge required error message
            var error = UmbracoHelper.GetDictionaryValue(errorMessageDictionaryKey);

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(error))
            {
                LogHelper.Warn<DictionaryItem>(string.Format("The dictionary key '{0}' for the required error message is empty or does not exist", errorMessageDictionaryKey));
                return defaultText; //AddDictionaryItemIfNotExist(controllerName, errorMessageDictionaryKey, defaultText);
            }

            // it's ok to pass in the minLength even for the error message without a {2} param since String.Format will just
            // ignore extra arguments

            // String replacment the token wiht our localised propertyname
            // The field {{Field}} must be less than {0} (MaxLength)
            // The field {{Field}} must be less than {0} (MaxLength) & greater than {1} (MinLength)
            error = error.Replace("{{Field}}", name);
            error = string.Format(error, maxLength, minLength);

            //Return the value
            return error;
        }

        
        
        private static string AddDictionaryItemIfNotExist(string uniqueKey, string key, string defaultText)
        {
            var comboKey = string.Format("{0}.{1}", uniqueKey, key);
            defaultText = !string.IsNullOrWhiteSpace(defaultText) ? defaultText : string.Format("[{0}]", key);
            if (!Service.DictionaryItemExists(uniqueKey))
            {
                var newItem = new DictionaryItem(uniqueKey);
                Service.Save(newItem);
            }
            var guidKey = Service.GetDictionaryItemByKey(uniqueKey).Key;
            var dictionary = Service.CreateDictionaryItemWithIdentity(comboKey, guidKey, defaultText);
            var dictionaryId = dictionary.Id;
            var dictionaryItem = Service.GetDictionaryItemById(dictionaryId);

            foreach (var item in dictionaryItem.Translations)
            {
                var value = item.Language.IsoCode.Substring(0, 2).ToLowerInvariant() == "en" ? defaultText : string.Format("[{0}]", key);
                Service.AddOrUpdateDictionaryValue(dictionaryItem, item.Language, value);
            }
            Service.Save(dictionaryItem);

            return UmbracoHelper.GetDictionaryValue(comboKey);
        }
        
    }
}
