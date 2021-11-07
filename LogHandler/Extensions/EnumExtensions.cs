using System;
using System.ComponentModel;
using System.Linq;

namespace LoggerHandler.Constants.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetEnumAttribute<TAttribute>(this Enum enumVal) where TAttribute : Attribute
        {
            var memberInfo = enumVal.GetType().GetMember(enumVal.ToString());
            return memberInfo[0].GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault();
        }
        public static string GetDescription(this Enum enumValue) => enumValue.GetEnumAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString();

        public static string GetEnumDescription<T>(this int value) where T : struct, IConvertible =>
            typeof(T).IsEnum && Enum.IsDefined(typeof(T), value) ? ((Enum)Enum.ToObject(typeof(T), value)).GetDescription() : string.Empty;
    }
}
