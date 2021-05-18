using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Pluralsight.Todo.Extensions
{

    // https://devsitesindex20190127.azurewebsites.net/CodeReferences/Details?id=2202138

    public static class EnumExtensions
    {

        public static string getName2(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string getDescription2(this Enum value)
        {

            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.getName2());

            DescriptionAttribute descAttr =
               fi.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            if (descAttr != null) return descAttr.Description;
            return value.getName2();

        }
    }

}