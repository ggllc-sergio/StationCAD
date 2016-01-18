using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StationCAD.Web.Helpers
{
    public static class EnumUtils
    {

        public static IEnumerable<SelectListItem> GetItems(this Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            { throw new ArgumentException("Not an Enum Type."); }

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) =>
                    new SelectListItem
                    {
                        Text = GetName(enumType, name),
                        Value = value.ToString(),
                        Selected = value == selectedValue
                    }
                );
            return items;

        }

        private static string GetName(Type enumType, string name)
        {
            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .Where(x => x.Name != null)
                .FirstOrDefault();
            return attribute != null ? attribute.GetName() : name;
        }
    }
}