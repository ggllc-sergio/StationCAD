using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StationCAD.Model.Helpers
{
    public static class EnumUtils
    {

        public static string GetName(this Type enumType)
        {
            var attribute = enumType
                .GetField("Name")
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .Where(x => x.Name != null)
                .FirstOrDefault();
            return attribute != null ? attribute.GetName() : enumType.Name;
        }
    }
}


