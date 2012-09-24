using System;
using System.ComponentModel;

namespace DynamicLoopIoC.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescriptionAttributeValue(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }

}