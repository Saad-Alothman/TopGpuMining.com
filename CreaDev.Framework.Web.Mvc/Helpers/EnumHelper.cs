using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class EnumHelper<T>
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }
        public static List<SelectListItem> GetSelectListEnumTextAsValue(string selectedValue)
        {
            List<SelectListItem> items = EnumHelper.GetSelectList(typeof(T)).ToList();
            foreach (var selectListItem in items)
            {
                int intValue = 0;
                if (int.TryParse(selectListItem.Value, out intValue))
                {

                    selectListItem.Value = (Enum.GetName(typeof(T), intValue)).ToString();
                }
            }
            if (!string.IsNullOrWhiteSpace(selectedValue))
            {

                var itemToSelect = items.FirstOrDefault(s => s.Value == selectedValue.ToString());
                if (itemToSelect != null)
                    itemToSelect.Selected = true;
            }
            return items;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}