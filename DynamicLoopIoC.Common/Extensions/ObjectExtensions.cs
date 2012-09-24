using System.Reflection;

namespace DynamicLoopIoC.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void Clone<T>(this T toClone, T clone)
        {
            foreach (var property in clone.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (property.CanRead && property.CanWrite)
                    property.SetValue(clone, property.GetValue(toClone, null), null);
        }
    }
}