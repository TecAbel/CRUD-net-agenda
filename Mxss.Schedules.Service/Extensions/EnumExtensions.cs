using System;
using System.ComponentModel;
using System.Reflection;

namespace Mxss.Schedules.Service
{
    public static class EnumExtensions
    {
        public static int Value(this System.Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string Description(this System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T) System.Enum.Parse(typeof(T), value, true);
        } 

        public static T ToEnum<T>(this int value)
        {
            return System.Enum.GetName(typeof(T), value).ToEnum<T>();
        } 
    }
}