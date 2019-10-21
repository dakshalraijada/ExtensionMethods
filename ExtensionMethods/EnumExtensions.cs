using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExtensionMethods
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets Display name attribute value of an Enum
        /// Required library: System.ComponentModel.DataAnnotations
        /// [Display(Name="My enum")]
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
