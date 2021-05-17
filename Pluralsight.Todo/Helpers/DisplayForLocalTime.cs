using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

// Add namespace where helpers to be used
// @using Pluralsight.Todo.Helpers


namespace Pluralsight.Todo.Helpers
{
    // 05/16/2021 11:16 am - SSN - [20210516-1011] - [003] - M03-02 - Introducing Azure table storage in a .NET application

    public static class HtmlHelpersCustom
    {
        public static string DisplayForLocalTime(this HtmlHelper helper, DateTimeOffset? _dateTime)
        {
            if (!_dateTime.HasValue) return "";

            return String.Format("*!*{0:MM/dd/yyyy hh:mm tt}", TimeZoneInfo.ConvertTime(_dateTime.Value, TimeZoneInfo.Local));
        }

         

        // http://blog.lekevin.com/technology/c-sharp/tips-extends-labelfor-or-displaynamefor-in-razor-mvc5-to-display-custom-label/
        //        public static MvcHtmlString DisplayNameFor_SSN<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression)
        public static MvcHtmlString DisplayNameFor_SSN<TModel>(IEnumerable<TModel> html, string fieldName) // where TModel : class
        {
            // var metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            // string displayName = metadata.DisplayName ?? metadata.PropertyName ?? ExpressionHelper.GetExpressionText(expression).Split('.').Last();
            // string displayName = metadata.DisplayName ?? metadata.PropertyName ?? ExpressionHelper.GetExpressionText(expression).Split('.').Last();
            string displayName = GetDisplayName(html.GetType(), fieldName); // replace with custom label
            return new MvcHtmlString(HttpUtility.HtmlEncode(displayName));

        }

        public static string GetDisplayName(Type t, string propName)
        {
            string displayName = propName;

            Type[] modelTypes = t.GenericTypeArguments;
            Type modelType = modelTypes[modelTypes.Length - 1];

            PropertyInfo propInfo = modelType.GetProperty(propName);

            object[] attributes = propInfo.GetCustomAttributes(true);

            foreach (object attr in attributes)
            {
                string attributeName = attr.GetType().Name;

                if (attributeName == "DisplayNameAttribute")
                {
                    DisplayNameAttribute dna = attr as DisplayNameAttribute;
                    displayName = dna.DisplayName;
                }
            }
            

            return displayName;

        }

    }
}